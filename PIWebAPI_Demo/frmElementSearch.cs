using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace PIWebAPI_Demo
{
    public partial class frmElementSearch : Form
    {
        private PIWebAPIClient client;
        private string baseUrl;
        private string serverWebId;
        private string assetDbWebId;

        public frmElementSearch()
        {
            InitializeComponent();
            baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            client = new PIWebAPIClient(ConfigurationManager.AppSettings["userid"], ConfigurationManager.AppSettings["password"]);
            LoadAssetServers();
            LoadUI();
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
            cboCategory.Items.Clear();
            cboTemplate.Items.Clear();

            lvResults.Columns.Clear();
            lvResults.Columns.Add("Name", -2, HorizontalAlignment.Left);
            lvResults.Columns.Add("Description", -2, HorizontalAlignment.Left);
            lvResults.Columns.Add("Path", -2, HorizontalAlignment.Left);
            lvResults.Columns.Add("Categories", -2, HorizontalAlignment.Left);
        }

        private async void cboServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadUI();

            string url = string.Format(@"{0}/assetservers?path=\\{1}", baseUrl, cboServers.SelectedItem);
            JObject jobj = await client.GetAsync(url);

            // Get the WebID of the selected server
            serverWebId = jobj["WebId"].ToString();

            // Load the asset databases
            url = string.Format(@"{0}/assetservers/{1}/assetdatabases", baseUrl, serverWebId);
            jobj = await client.GetAsync(url);
            for (int i = 0; i < jobj["Items"].Count(); i++)
            {
                cboDatabase.Items.Add(jobj["Items"][i]["Name"].ToString());
            }
        }

        private async void cboDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCategory.Items.Clear();
            cboTemplate.Items.Clear();
            lvResults.Items.Clear();

            // Get the WebID of the selected asset database
            string url = string.Format(@"{0}/assetdatabases?path=\\{1}\{2}", baseUrl, cboServers.SelectedItem, cboDatabase.SelectedItem);
            JObject jobj = await client.GetAsync(url);
            assetDbWebId = jobj["WebId"].ToString();

            // Load the element categories
            url = string.Format(@"{0}/assetdatabases/{1}/elementcategories", baseUrl, assetDbWebId);
            
            jobj = await client.GetAsync(url);
            for (int i = 0; i < jobj["Items"].Count(); i++)
            {
                cboCategory.Items.Add(jobj["Items"][i]["Name"].ToString());
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
            if (!string.IsNullOrWhiteSpace(txtNameFilter.Text))
            {
                url.AppendFormat("nameFilter={0}&", txtNameFilter.Text);
            }

            if ((cboTemplate.SelectedItem != null) && (!string.IsNullOrWhiteSpace(cboTemplate.SelectedItem.ToString())))
            {
                url.AppendFormat("templatename={0}&", cboTemplate.SelectedItem.ToString());
            }

            if ((cboCategory.SelectedItem != null) && (!string.IsNullOrWhiteSpace(cboCategory.SelectedItem.ToString())))
            {
                url.AppendFormat("categoryName={0}&", cboCategory.SelectedItem.ToString());
            }

            url.AppendFormat("searchFullHierarchy={0}", chkHierarchy.Checked.ToString());

            JObject jobj = await client.GetAsync(url.ToString());

            for (int i = 0; i < jobj["Items"].Count(); i++)
            {
                lvResults.Items.Add(new AFElementListViewItem(jobj["Items"][i]["Name"].ToString(),
                                        jobj["Items"][i]["Path"].ToString(),
                                        jobj["Items"][i]["Description"].ToString(),
                                        jobj["Items"][i]["CategoryNames"].ToString()));
            }

            lvResults.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lvResults.EndUpdate();
        }
    }
}
