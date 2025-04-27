using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSaver
{

    public enum MainListItemTagTypes
    {
        RepoSave,
        OrhpanArchive,
    }

    public class MainListItemTag
    {
        public MainListItemTagTypes Type { get; }

        public RepoSave? RepoSave;
        public string? OrphanArchive;

        public MainListItemTag(RepoSave save)
        {
            RepoSave = save;
            Type = MainListItemTagTypes.RepoSave;
        }

        public MainListItemTag(string orphanArchive)
        {
            OrphanArchive = orphanArchive;
            Type = MainListItemTagTypes.OrhpanArchive;
        }
    }
}
