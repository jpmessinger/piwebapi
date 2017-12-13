using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace PIWebAPI_Demo
{
    public partial class frmAttributeData : Form
    {
        private PIWebAPIClient client;
        private string baseUrl;
        private string serverWebId;
        private string assetDbWebId;

        public frmAttributeData()
        {
            InitializeComponent();
            baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            client = new PIWebAPIClient(ConfigurationManager.AppSettings["userid"], ConfigurationManager.AppSettings["password"]);
            LoadAssetServers();
            LoadUI();
            LoadComboBoxes();
        }

        private async void LoadAssetServers()
        {
            cboServers.Items.Clear();
            string url = string.Format(@"{0}/assetservers", baseUrl);
            try
            {
                JObject serverList = await client.GetAsync(url);

                for (int i = 0; i < serverList["Items"].Count(); i++)
                {
                    cboServers.Items.Add(serverList["Items"][i]["Name"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadUI()
        {
            cboDatabase.Items.Clear();
            cboAttrCategory.Items.Clear();
            cboTemplate.Items.Clear();

            lvResults.Columns.Clear();
            lvResults.Columns.Add("Name", -2, HorizontalAlignment.Left);
            lvResults.Columns.Add("Path", -2, HorizontalAlignment.Left);

            cboStreamType.Items.Clear();
            cboStreamSetType.Items.Clear();

            lvData.Columns.Clear();
            lvData.Columns.Add("Timestamp", -2, HorizontalAlignment.Left);
            lvData.Columns.Add("Value", -2, HorizontalAlignment.Left);
            lvData.Columns.Add("UoM", -2, HorizontalAlignment.Left);
        }

        private void LoadComboBoxes()
        {
            cboStreamType.Items.Add("Interpolated");
            cboStreamType.Items.Add("Plot");
            cboStreamType.Items.Add("Recorded");
            cboStreamType.Items.Add("Summary");

            cboStreamSetType.Items.Add("");
            cboStreamSetType.Items.Add("Interpolated");
            cboStreamSetType.Items.Add("Plot");
            cboStreamSetType.Items.Add("Recorded");
            cboStreamSetType.Items.Add("Summaries");
        }

        private async void cboServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            string url = string.Format(@"{0}/assetservers?path=\\{1}", baseUrl, cboServers.SelectedItem);
            JObject jobj = await client.GetAsync(url);

            // Get the WebID of the selected server
            serverWebId = jobj["WebId"].ToString();

            // Load the asset databases
            cboDatabase.Items.Clear();
            url = string.Format(@"{0}/assetservers/{1}/assetdatabases", baseUrl, serverWebId);
            jobj = await client.GetAsync(url);
            for (int i = 0; i < jobj["Items"].Count(); i++)
            {
                cboDatabase.Items.Add(jobj["Items"][i]["Name"].ToString());
            }
        }

        private async void cboDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboAttrCategory.Items.Clear();
            cboTemplate.Items.Clear();
            lvResults.Items.Clear();

            // Get the WebID of the selected asset database
            string url = string.Format(@"{0}/assetdatabases?path=\\{1}\{2}", baseUrl, cboServers.SelectedItem, cboDatabase.SelectedItem);
            JObject jobj = await client.GetAsync(url);
            assetDbWebId = jobj["WebId"].ToString();

            // Load the attribute categories
            url = string.Format(@"{0}/assetdatabases/{1}/attributecategories", baseUrl, assetDbWebId);

            jobj = await client.GetAsync(url);
            for (int i = 0; i < jobj["Items"].Count(); i++)
            {
                cboAttrCategory.Items.Add(jobj["Items"][i]["Name"].ToString());
            }

            // Load the element templates
            url = string.Format(@"{0}/assetdatabases/{1}/elementtemplates", baseUrl, assetDbWebId);

            jobj = await client.GetAsync(url);
            for (int i = 0; i < jobj["Items"].Count(); i++)
            {
                cboTemplate.Items.Add(jobj["Items"][i]["Name"].ToString());
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (cboServers.SelectedItem == null)
            {
                MessageBox.Show("Must select a server first!");
                return;
            }

            lvResults.Items.Clear();
            lvResults.BeginUpdate();

            // search for Elements based on user selected criteria
            var url = new StringBuilder();
            url.AppendFormat(@"{0}/assetdatabases/{1}/elements?", baseUrl, assetDbWebId);

            if ((cboTemplate.SelectedItem != null) && (!string.IsNullOrWhiteSpace(cboTemplate.SelectedItem.ToString())))
            {
                url.AppendFormat("templatename={0}&", cboTemplate.SelectedItem.ToString());
            }

            url.AppendFormat("searchFullHierarchy={0}", chkHierarchy.Checked.ToString());

            JObject jobj = await client.GetAsync(url.ToString());

            var elementWebIds = new List<string>();

            for (int i = 0; i < jobj["Items"].Count(); i++)
            {
                elementWebIds.Add(jobj["Items"][i]["WebId"].ToString());
            }

            // Now get the Attributes for the returned Elements
            var attrWebIds = new List<string>();

            foreach (var item in elementWebIds)
            {
                url.Clear();
                url.AppendFormat(@"{0}/elements/{1}/attributes", baseUrl, item);

                if (!string.IsNullOrWhiteSpace(txtNameFilter.Text))
                {
                    url.AppendFormat("?nameFilter={0}", txtNameFilter.Text);
                }

                jobj = await client.GetAsync(url.ToString());
                for (int i = 0; i < jobj["Items"].Count(); i++)
                {
                    lvResults.Items.Add(new AFAttributeListViewItem(jobj["Items"][i]["Name"].ToString(),
                                        jobj["Items"][i]["Path"].ToString(),
                                        jobj["Items"][i]["WebId"].ToString()));
                }
            }

            lvResults.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lvResults.EndUpdate();
        }

        private async void btnData_Click(object sender, EventArgs e)
        {
            lvData.Items.Clear();

            if (lvResults.SelectedItems == null)
            {
                MessageBox.Show("Must select an attribute for data retrieval!");
                return;
            }

            if (cboStreamType.SelectedItem == null)
            {
                MessageBox.Show("Must select a stream type for data retrieval!");
                return;
            }

            var url = new StringBuilder();
            url.AppendFormat(@"{0}/streams/{1}/{2}", baseUrl, lvResults.SelectedItems[0].SubItems[2].Text, cboStreamType.SelectedItem.ToString());

            JObject jobj = await client.GetAsync(url.ToString());

            lvData.BeginUpdate();
            for (int i = 0; i < jobj["Items"].Count(); i++)
            {
                DataItemListViewItem lvItem;
                if (cboStreamType.SelectedItem.ToString().ToUpper().Equals("SUMMARY"))
                {
                    var timestamp = Convert.ToDateTime(jobj["Items"][i]["Value"]["Timestamp"].ToString()).ToLocalTime();
                    lvItem = new DataItemListViewItem(timestamp.ToString(),
                                        jobj["Items"][i]["Value"]["Value"].ToString(),
                                        jobj["Items"][i]["Value"]["UnitsAbbreviation"].ToString());
                }
                else
                {
                    var timestamp = Convert.ToDateTime(jobj["Items"][i]["Timestamp"].ToString()).ToLocalTime();
                    lvItem = new DataItemListViewItem(timestamp.ToString(),
                                        jobj["Items"][i]["Value"].ToString(),
                                        jobj["Items"][i]["UnitsAbbreviation"].ToString());
                }

                lvData.Items.Add(lvItem);
            }

            lvData.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lvData.EndUpdate();
        }

        private async void btnBulkData_Click(object sender, EventArgs e)
        {
            lvData.Items.Clear();

            if (lvResults.SelectedItems == null)
            {
                MessageBox.Show("Must select an attribute for data retrieval!");
                return;
            }

            if (cboStreamSetType.SelectedItem == null)
            {
                MessageBox.Show("Must select a stream type for data retrieval!");
                return;
            }

            var attrWebIds = new List<string>();
            foreach (AFAttributeListViewItem item in lvResults.SelectedItems)
            {
                attrWebIds.Add(item.SubItems[2].Text);
            }

            var url = new StringBuilder();
            url.AppendFormat(@"{0}/streams/{1}/{2}", baseUrl, lvResults.SelectedItems[0].SubItems[2].Text, cboStreamType.SelectedItem.ToString());

            JObject jobj = await client.GetAsync(url.ToString());

            for (int i = 0; i < jobj["Items"].Count(); i++)
            {
                DataItemListViewItem lvItem;
                if (cboStreamType.SelectedItem.ToString().ToUpper().Equals("SUMMARY"))
                {
                    var timestamp = Convert.ToDateTime(jobj["Items"][i]["Value"]["Timestamp"].ToString()).ToLocalTime();
                    lvItem = new DataItemListViewItem(timestamp.ToString(),
                                        jobj["Items"][i]["Value"]["Value"].ToString(),
                                        jobj["Items"][i]["Value"]["UnitsAbbreviation"].ToString());
                }
                else
                {
                    var timestamp = Convert.ToDateTime(jobj["Items"][i]["Timestamp"].ToString()).ToLocalTime();
                    lvItem = new DataItemListViewItem(timestamp.ToString(),
                                        jobj["Items"][i]["Value"].ToString(),
                                        jobj["Items"][i]["UnitsAbbreviation"].ToString());
                }

                lvData.Items.Add(lvItem);
            }
        }

        private void btnRealtimeData_Click(object sender, EventArgs e)
        {

        }
    }
}
