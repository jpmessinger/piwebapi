using System.Windows.Forms;

namespace PIWebAPI_Demo
{
    public class DataItemListViewItem : ListViewItem
    {
        public DataItemListViewItem(string timestamp, string value, string uom)
            : base(timestamp)
        {
            this.SubItems.Add(value);
            this.SubItems.Add(uom);
        }
    }
}
