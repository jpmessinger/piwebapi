using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace PIWebAPI_Demo
{
    public partial class frmTagSearch : Form
    {
        private PIWebAPIClient client;
        private string baseUrl;

        public frmTagSearch()
        {
            InitializeComponent();
            baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            client = new PIWebAPIClient(ConfigurationManager.AppSettings["userid"], ConfigurationManager.AppSettings["password"]);
            LoadUI();
            LoadDataArchiveServers();
        }

        private async void LoadDataArchiveServers()
        {
            cboServers.Items.Clear();
            string url = string.Format(@"{0}/dataservers", baseUrl);
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
            lvResults.Columns.Clear();
            lvResults.Columns.Add("Tag", -2, HorizontalAlignment.Left);
            lvResults.Columns.Add("Descriptor", -2, HorizontalAlignment.Left);
            lvResults.Columns.Add("Point Type", -2, HorizontalAlignment.Left);
            lvResults.Columns.Add("Eng Units", -2, HorizontalAlignment.Left);
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
            string url = string.Format(@"{0}/dataservers?path=\\{1}", baseUrl, cboServers.SelectedItem);
            JObject jobj = await client.GetAsync(url);

            // Get the WebID of the selected server
            string webId = jobj["WebId"].ToString();

            url = string.Format(@"{0}/dataservers/{1}/points?namefilter={2}&pointsource={3}", baseUrl, webId, txtTagMask.Text, txtPointSource.Text);

            url = string.Format(@"{0}/dataservers/{1}/points?namefilter={2}&pointsource={3}", baseUrl, webId,txtTagMask.Text,txtPointSource.Text);
            jobj = await client.GetAsync(url);

            for (int i = 0; i < jobj["Items"].Count(); i++)
            {
                lvResults.Items.Add(new PIPointListViewItem(jobj["Items"][i]["Name"].ToString(), 
                                        jobj["Items"][i]["Descriptor"].ToString(), 
                                        jobj["Items"][i]["PointType"].ToString(), 
                                        jobj["Items"][i]["EngineeringUnits"].ToString()));
            }

            lvResults.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lvResults.EndUpdate();
        }
    }
}
