namespace Browsio.Domain
{
    #region << Using >>

    using System;
    using System.Diagnostics.CodeAnalysis;
    using Incoding.Quality;
    using JetBrains.Annotations;

    #endregion

    public class Product : BrowsioEntityBase, IImageEntity
    {
        #region Properties

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual string Author { get; set; }

        public virtual float Price { get; set; }

        public virtual Store Store { get; protected internal set; }

        public virtual string Asin { get; set; }

        #endregion

        #region IImageEntity Members

        public virtual byte[] Image { get; set; }

        #endregion

        #region Nested classes

        [UsedImplicitly, Obsolete(ObsoleteMessage.ClassNotForDirectUsage, true), ExcludeFromCodeCoverage]
        public class Map : BrowsioEntityMap<Product>
        {
            ////ncrunch: no coverage start
            #region Constructors

            protected Map()
            {
                MapEscaping(r => r.Name);
                MapEscaping(r => r.Asin);
                MapEscaping(r => r.Author);
                MapEscaping(r => r.Description).Length(int.MaxValue / 2);
                MapEscaping(r => r.Image).Length(int.MaxValue);
                MapEscaping(r => r.Price).Precision(2);
                DefaultReference(r => r.Store);
            }

            #endregion

            ////ncrunch: no coverage end        
        }

        #endregion
    }
}