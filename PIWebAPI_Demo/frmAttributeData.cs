using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
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
            cboStreamType.Items.Add("Value");

            cboStreamSetType.Items.Add("Interpolated");
            cboStreamSetType.Items.Add("Plot");
            cboStreamSetType.Items.Add("Recorded");
            cboStreamSetType.Items.Add("Summaries");
            cboStreamSetType.Items.Add("Value");
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
                    /* I'm storing the individual WebId's as a ListViewItem sub-item for persistence so that I don't
                     * have to make another web request to get the WebId when attributes are selected for data retrieval */

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
                MessageBox.Show("Must select at least one attribute for data retrieval!");
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
            url.AppendFormat(@"{0}/streamsets/{1}?webid={2}", baseUrl, cboStreamSetType.SelectedItem.ToString(), attrWebIds[0]);

            for (int i = 1; i < attrWebIds.Count(); i++) // We start the iterator at 1 instead of 0, as we've already added the WebId of the first attribute to the url
            {
                url.AppendFormat(@"&webid={0}", attrWebIds[i]);
            }

            JObject jobj = await client.GetAsync(url.ToString());

            lvData.BeginUpdate();
            if (cboStreamSetType.SelectedItem.ToString().ToUpper().Equals("VALUE"))
            {
                for (int i = 0; i < jobj["Items"].Count(); i++)
                {
                    DataItemListViewItem lvItem;
                    var timestamp = Convert.ToDateTime(jobj["Items"][i]["Value"]["Timestamp"].ToString()).ToLocalTime();
                    lvItem = new DataItemListViewItem(timestamp.ToString(),
                                        jobj["Items"][i]["Value"]["Value"].ToString(),
                                        jobj["Items"][i]["Value"]["UnitsAbbreviation"].ToString());

                    lvData.Items.Add(lvItem);
                }
            }
            else
            {
                for (int i = 0; i < jobj["Items"].Count(); i++)
                {
                    for (int j = 0; j < jobj["Items"][i]["Items"].Count(); j++)
                    {
                        DataItemListViewItem lvItem;
                        var timestamp = Convert.ToDateTime(jobj["Items"][i]["Items"][j]["Timestamp"].ToString()).ToLocalTime();
                        lvItem = new DataItemListViewItem(timestamp.ToString(),
                                            jobj["Items"][i]["Items"][j]["Value"].ToString(),
                                            jobj["Items"][i]["Items"][j]["UnitsAbbreviation"].ToString());
                        lvData.Items.Add(lvItem);
                    }
                }
            }

            lvData.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lvData.EndUpdate();
        }

        private void btnRealtimeData_Click(object sender, EventArgs e)
        {
            lvData.Items.Clear();

            if (lvResults.SelectedItems == null)
            {
                MessageBox.Show("Must select an attribute for data retrieval!");
                return;
            }

            var result = GetAsync(lvResults.SelectedItems[0].SubItems[2].Text, new CancellationToken());

        }

        /* In production code I wouldn't put this function in a WinForm, but as I couldn't figure out in time how to 
         * pass the websocket message out of the function as a Task Result without ending the task, I decided to just
         * drop it here for simplicity. Most code examples I could find usually just wrote the message out to the Console
         * within the function.
         * This piece of code is largely based on the example code from the PI Web API programmer documentation under the 
         * Channels (Core Services) entry. I have made some moifications to suit my project such as setting the RequestHeader
         * and the client credentials, as well as handling the websocket message to parse the JSON and drop the data into
         * a ListView control. */
         
        private async Task GetAsync(string webId, CancellationToken cancellationToken)
        {
            string baseUri = baseUrl.Replace("https", "wss");
            Uri uri = new Uri(string.Format("{0}/streams/{1}/channel?includeInitialValues=true", baseUri, webId));

            WebSocketReceiveResult receiveResult;
            byte[] receiveBuffer = new byte[65536];
            ArraySegment<byte> receiveSegment = new ArraySegment<byte>(receiveBuffer);

            using (ClientWebSocket webSocket = new ClientWebSocket())
            {
                try
                {
                    webSocket.Options.SetRequestHeader("Authorization", "Basic");
                    webSocket.Options.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["userid"], ConfigurationManager.AppSettings["password"]);
                    
                    await webSocket.ConnectAsync(uri, CancellationToken.None);
                }
                catch (WebSocketException)
                {
                    MessageBox.Show("Could not connect to server.");
                    return;
                }

                while (true)
                {
                    try
                    {
                        receiveResult = await webSocket.ReceiveAsync(receiveSegment, cancellationToken);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }

                    if (receiveResult.MessageType != WebSocketMessageType.Text)
                    {
                        await webSocket.CloseAsync(
                            WebSocketCloseStatus.InvalidMessageType,
                            "Message type is not text.",
                            CancellationToken.None);
                        return;
                    }
                    else if (receiveResult.Count > receiveBuffer.Length)
                    {
                        await webSocket.CloseAsync(
                            WebSocketCloseStatus.InvalidPayloadData,
                            "Message is too long.",
                            CancellationToken.None);
                        return;
                    }

                    string message = Encoding.UTF8.GetString(receiveBuffer, 0, receiveResult.Count);
                    JObject msg = JObject.Parse(message);

                    try
                    {
                        var timestamp = Convert.ToDateTime(msg["Items"][0]["Items"][0]["Timestamp"].ToString()).ToLocalTime();
                        var lvItem = new DataItemListViewItem(timestamp.ToString(),
                                            msg["Items"][0]["Items"][0]["Value"].ToString(),
                                            msg["Items"][0]["Items"][0]["UnitsAbbreviation"].ToString());

                        lvData.Items.Add(lvItem);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    
                }

                await webSocket.CloseAsync(
                WebSocketCloseStatus.NormalClosure,
                "Closing connection.",
                CancellationToken.None);
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lvResults.SelectedItems == null)
            {
                MessageBox.Show("Must select an attribute to update!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNewValue.Text))
            {
                MessageBox.Show("Please provide a new value to update the attribute with");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTimestamp.Text))
            {
                MessageBox.Show("Please provide a timestamp to update the attribute with");
                return;
            }

            if (!Regex.IsMatch(txtTimestamp.Text, @"^([\+-]?\d{4}(?!\d{2}\b))((-?)((0[1-9]|1[0-2])(\3([12]\d|0[1-9]|3[01]))?|W([0-4]\d|5[0-2])(-?[1-7])?|(00[1-9]|0[1-9]\d|[12]\d{2}|3([0-5]\d|6[1-6])))([T\s]((([01]\d|2[0-3])((:?)[0-5]\d)?|24\:?00)([\.,]\d+(?!:))?)?(\17[0-5]\d([\.,]\d+)?)?([zZ]|([\+-])([01]\d|2[0-3]):?([0-5]\d)?)?)?)?$"))
            {
                MessageBox.Show("Timestamp is not correctly formatted");
                return;
            }


            var url = new StringBuilder();
            url.AppendFormat(@"{0}/streams/{1}/value", baseUrl, lvResults.SelectedItems[0].SubItems[2].Text);

            var payload = new StringContent(GetUpdateValueJson().ToString(), Encoding.UTF8, "application/json");

            try
            {
                var result = await client.PostAsync(url.ToString(), payload);
                MessageBox.Show(result.StatusCode.ToString());
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Newtonsoft.Json.JsonReaderException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private JObject GetUpdateValueJson()
        {
            JObject jobj = new JObject(new JProperty("Timestamp", txtTimestamp.Text), new JProperty("Value", txtNewValue.Text));
            return jobj;
        }
    }
}
