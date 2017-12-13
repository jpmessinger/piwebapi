using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace PIWebAPI_Demo
{
    public partial class frmMain : Form
    {
        private string baseUrl;

        private PIWebAPIClient client;

        public frmMain()
        {
            InitializeComponent();
            baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            client = new PIWebAPIClient(ConfigurationManager.AppSettings["userid"], ConfigurationManager.AppSettings["password"]);
        }

        private void LoadUI()
        {
            cboAFservers.Items.Clear();
            cboDAservers.Items.Clear();
            cboStreamType.Items.Clear();
            cboStreamType.Items.Add("GetInterpolated");
            cboStreamType.Items.Add("GetInterpolatedAtTimes");
            cboStreamType.Items.Add("GetPlot");
            cboStreamType.Items.Add("GetRecorded");
            cboStreamType.Items.Add("GetRecordedAtTime");
            cboStreamType.Items.Add("GetSummary");
            cboStreamType.Items.Add("GetValue");

        }

        private async void btnPIServers_Click(object sender, EventArgs e)
        {
            cboDAservers.Items.Clear();
            string url = string.Format(@"{0}/dataservers", baseUrl);
            try
            {
                JObject serverList = await client.GetAsync(url);

                for (int i = 0; i < serverList["Items"].Count(); i++)
                {
                    cboDAservers.Items.Add(serverList["Items"][i]["Name"].ToString());
                }

                MessageBox.Show("Success!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnAFServers_Click(object sender, EventArgs e)
        {
            cboAFservers.Items.Clear();
            string url = string.Format(@"{0}/assetservers", baseUrl);
            try
            {
                JObject serverList = await client.GetAsync(url);

                for (int i = 0; i < serverList["Items"].Count(); i++)
                {
                    cboAFservers.Items.Add(serverList["Items"][i]["Name"].ToString());
                }

                MessageBox.Show("Success!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTagSearch_Click(object sender, EventArgs e)
        {
            var dlgTagSearch = new frmTagSearch();
            dlgTagSearch.ShowDialog();
        }

        private void btnElementSearch_Click(object sender, EventArgs e)
        {
            var dlgAFSearch = new frmElementSearch();
            dlgAFSearch.ShowDialog();
        }

        private void btnData_Click(object sender, EventArgs e)
        {

        }

        private void btnAttrSearch_Click(object sender, EventArgs e)
        {
            var dlgAttributeData = new frmAttributeData();
            dlgAttributeData.ShowDialog();
        }
    }
}
