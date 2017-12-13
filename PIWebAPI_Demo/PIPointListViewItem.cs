using System.Windows.Forms;

namespace PIWebAPI_Demo
{
    public class PIPointListViewItem : ListViewItem
    {
        public PIPointListViewItem(string name,string descriptor, string pointtype, string engunits)
            :base(name)
        {
            this.SubItems.Add(descriptor);
            this.SubItems.Add(pointtype);
            this.SubItems.Add(engunits);
        }
    }
}
