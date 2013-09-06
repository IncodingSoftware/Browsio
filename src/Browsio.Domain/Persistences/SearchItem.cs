namespace Browsio.Domain
{
    #region << Using >>

    using System;
    using System.Diagnostics.CodeAnalysis;
    using Incoding.Data;
    using Incoding.Quality;
    using JetBrains.Annotations;

    #endregion

    public class SearchItem : BrowsioEntityBase
    {

        #region Properties

        public virtual string OwnerId { get; set; }

        public virtual string Query { get; set; }

        public virtual SearchItemOfType Type { get; set; }

        #endregion

        #region Nested classes

        [UsedImplicitly, Obsolete(ObsoleteMessage.ClassNotForDirectUsage, true), ExcludeFromCodeCoverage]
        public class Map : BrowsioEntityMap<SearchItem>
        {
            ////ncrunch: no coverage start
            #region Constructors

            protected Map()
            {
                MapEscaping(r => r.OwnerId);
                MapEscaping(r => r.Query);
                MapEscaping(r => r.Type).CustomType<int>();
            }

            #endregion

            ////ncrunch: no coverage end        
        }

        #endregion
    }
}