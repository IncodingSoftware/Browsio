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
    using NHibernate;

    public class Activity : BrowsioEntityBase
    {
        public virtual string Title { get; set; }

        public virtual ActivityOfType Type { get; set; }

        public virtual string Description { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual string ObjectActivity { get; set; }

        public virtual User ActivityToUser { get; set; }

        public virtual User ActivityFromUser { get; set; }

        [UsedImplicitly, Obsolete(ObsoleteMessage.ClassNotForDirectUsage, true), ExcludeFromCodeCoverage]
        public class Map : BrowsioEntityMap<Activity>
        {

            ////ncrunch: no coverage start
            protected Map()
            {
                MapEscaping(r => r.Title);
                MapEscaping(r => r.Type);
                MapEscaping(r => r.Description);
                MapEscaping(r => r.Date).CustomType("timestamp");
                MapEscaping(r => r.ObjectActivity);

                DefaultReference(r => r.ActivityToUser);
                DefaultReference(r => r.ActivityFromUser);
            }
            ////ncrunch: no coverage end 
        }
    }
}
