using System.Windows.Forms;

namespace PIWebAPI_Demo
{
    public class AFElementListViewItem : ListViewItem
    {
        public AFElementListViewItem(string name, string description, string path, string categories)
            : base(name)
        {
            this.SubItems.Add(path);
            this.SubItems.Add(description);
            this.SubItems.Add(categories);
        }
    }
}
