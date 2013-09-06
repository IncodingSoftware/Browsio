namespace Browsio.Domain
{
    #region << Using >>

    using System;
    using System.Diagnostics.CodeAnalysis;
    using Incoding.Data;
    using Incoding.Quality;
    using JetBrains.Annotations;

    #endregion

    public class Genre : BrowsioEntityBase
    {
        #region Properties

        public virtual string Name { get; set; }

        public virtual CategoryOfType Category { get; set; }

        #endregion

        #region Nested classes

        [UsedImplicitly, Obsolete(ObsoleteMessage.ClassNotForDirectUsage, true), ExcludeFromCodeCoverage]
        public class Map : BrowsioEntityMap<Genre>
        {
            ////ncrunch: no coverage start
            #region Constructors

            protected Map()
            {
                MapEscaping(r => r.Name);
                MapEscaping(r => r.Category).CustomType<int>();
            }

            #endregion

            ////ncrunch: no coverage end        
        }

        #endregion
    }
}