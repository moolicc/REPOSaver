using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSaver
{
    static class Extensions
    {
        public static List<DynamicProperty> Fields(this TreeNode node)
        {
            return (List<DynamicProperty>)node.Tag!;
        }

        public static MainListItemTag ItemTag(this ListViewItem item)
        {
            return (MainListItemTag)item.Tag!;
        }

        public static void RepoSaveTag(this ListViewItem item, Action<RepoSave> ifSave)
        {
            MainListItemTag tag = item.ItemTag();
            if(tag.Type == MainListItemTagTypes.RepoSave)
            {
                ifSave(tag.RepoSave!);
            }
        }

        public static void OrphanArchiveTag(this ListViewItem item, Action<string> ifOrphanArchive)
        {
            MainListItemTag tag = item.ItemTag();
            if (tag.Type == MainListItemTagTypes.OrhpanArchive)
            {
                ifOrphanArchive(tag.OrphanArchive!);
            }
        }
    }
}
