namespace Browsio.Domain
{
    #region << Using >>

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using Incoding.Data;
    using Incoding.Quality;
    using JetBrains.Annotations;

    #endregion

    public class Store : BrowsioEntityBase, IImageEntity
    {
        #region Fields

        readonly IList<Product> products = new List<Product>();
        readonly IList<User> followers = new List<User>();
        readonly IList<Views> views = new List<Views>();

        #endregion

        #region Properties

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual User User { get; protected internal set; }

        public virtual CategoryOfType Category { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual ReadOnlyCollection<Product> Products { get { return new ReadOnlyCollection<Product>(this.products); } }

        public virtual ReadOnlyCollection<User> Followers { get { return new ReadOnlyCollection<User>(this.followers); } }

        public virtual ReadOnlyCollection<Views> Views { get { return new ReadOnlyCollection<Views>(this.views); } }

        
        public virtual string CategoryAsAmazon
        {
            get
            {
                switch (Category)
                {
                    case CategoryOfType.Book:
                        return "Books";
                    case CategoryOfType.Movie:
                        return "DVD";
                    case CategoryOfType.TVShow:
                        return "DVD";
                    case CategoryOfType.VideoGame:
                        return "VideoGames";

                        ////ncrunch: no coverage start
                    default:
                        throw new ArgumentOutOfRangeException();

                        ////ncrunch: no coverage end                
                }
            }
        }

        public virtual string CategoryAsClass
        {
            get
            {
                switch (Category)
                {
                    case CategoryOfType.Book:
                        return "books";
                    case CategoryOfType.Movie:
                        return "movies";
                    case CategoryOfType.TVShow:
                        return "tv-shows";
                    case CategoryOfType.VideoGame:
                        return "video-games";
                    default:
                        return string.Empty;
                }
            }
        }

        #endregion

        #region IImageEntity Members

        public virtual byte[] Image { get; set; }
        
        #endregion

        #region Api Methods

        public virtual void AddProduct(Product product)
        {
            product.Store = this;
            this.products.Add(product);
        }

        public virtual void AddFollower(User user)
        {
            this.followers.Add(user);
        }

        public virtual void DeleteFollow(User user)
        {
            this.followers.Remove(user);
        }

        public virtual void AddView(Views view)
        {
            view.Store = this;
            this.views.Add(view);
        }

        #endregion

        #region Nested classes

        [UsedImplicitly, Obsolete(ObsoleteMessage.ClassNotForDirectUsage, true), ExcludeFromCodeCoverage]
        public class Map : BrowsioEntityMap<Store>
        {
            ////ncrunch: no coverage start
            #region Constructors

            protected Map()
            {
                MapEscaping(r => r.Name);
                MapEscaping(r => r.Description).Length(int.MaxValue / 2);
                MapEscaping(r => r.Image).Length(int.MaxValue);
                MapEscaping(r => r.Category).CustomType<int>();
                DefaultReference(r => r.User);
                DefaultReference(r => r.Genre);
                DefaultHasMany(r => r.Products);
                DefaultHasMany(r => r.Views);
                DefaultHasManyToMany(r => r.Followers);
            }

            #endregion

            ////ncrunch: no coverage end        
        }

        #endregion
    }
}