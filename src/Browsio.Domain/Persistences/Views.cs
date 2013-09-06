using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browsio.Domain
{
    using System.Diagnostics.CodeAnalysis;
    using Incoding.Quality;
    using JetBrains.Annotations;

    public class Views : BrowsioEntityBase
    {
        public virtual string IPVisitors { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual User User { get; set; }

        public virtual Store Store { get; set; }

        [UsedImplicitly, Obsolete(ObsoleteMessage.ClassNotForDirectUsage, true), ExcludeFromCodeCoverage]
        public class Map : BrowsioEntityMap<Views>
        {
            ////ncrunch: no coverage start
            protected Map()
            {
                MapEscaping(r => r.IPVisitors);
                MapEscaping(r => r.Date);

                DefaultReference(r => r.User);
                DefaultReference(r => r.Store);
            }
            ////ncrunch: no coverage end
        }
    }
}
