using System.Windows.Forms;

namespace PIWebAPI_Demo
{
    public class AFAttributeListViewItem : ListViewItem
    {
        public AFAttributeListViewItem(string name, string path, string webid)
            : base(name)
        {
            this.SubItems.Add(path);
            this.SubItems.Add(webid);
        }
    }
}
