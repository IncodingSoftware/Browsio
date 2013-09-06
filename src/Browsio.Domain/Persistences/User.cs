namespace Browsio.Domain
{
    #region << Using >>

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using Incoding.Data;
    using Incoding.Extensions;
    using Incoding.Quality;
    using JetBrains.Annotations;

    #endregion

    public class User : BrowsioEntityBase, IImageEntity
    {
        #region Fields

        readonly IList<User> friends = new List<User>();

        readonly IList<Store> stores = new List<Store>();

        readonly IList<Store> followers = new List<Store>();

        readonly IList<Product> wishProducts = new List<Product>(); 

        #endregion

        #region Properties

        public virtual string Login { get; set; }

        public virtual string Password { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string FullName { get { return "{0} {1}".F(FirstName, LastName); } }

        public virtual SexOfType Sex { get; set; }

        public virtual bool Activated { get; set; }

        public virtual string ResetToken { get; set; }

        public virtual string AccessToken { get; set; }

        public virtual  string FbId { get; set; }

        public virtual ReadOnlyCollection<Store> Stores { get { return new ReadOnlyCollection<Store>(this.stores); } }

        public virtual ReadOnlyCollection<Store> Followers { get { return new ReadOnlyCollection<Store>(this.followers); } }

        public virtual ReadOnlyCollection<Product> WishProducts { get { return new ReadOnlyCollection<Product>(this.wishProducts); } }

        public virtual ReadOnlyCollection<User> Friends { get { return new ReadOnlyCollection<User>(this.friends);} }

        #endregion

        #region IImageEntity Members

        public virtual byte[] Image { get; set; }

        #endregion

        #region Api Methods

        public virtual void AddStore(Store store)
        {
            store.User = this;
            this.stores.Add(store);
        }
        
        public virtual void AddWishProduct(Product product)
        {
            this.wishProducts.Add(product);
        }

        public virtual void AddFriend(User user)
        {
            this.friends.Add(user);
        }

        #endregion

        #region Nested classes

        [UsedImplicitly, Obsolete(ObsoleteMessage.ClassNotForDirectUsage, true), ExcludeFromCodeCoverage]
        public class Map : BrowsioEntityMap<User>
        {
            #region Constructors

            protected Map()
            {
                MapEscaping(r => r.Login);
                MapEscaping(r => r.Password);
                MapEscaping(r => r.FirstName);
                MapEscaping(r => r.LastName);
                MapEscaping(r => r.Sex).CustomType<int>();
                MapEscaping(r => r.Image).Length(int.MaxValue);
                MapEscaping(r => r.Activated);
                MapEscaping(r => r.ResetToken);
                MapEscaping(r => r.AccessToken);
                MapEscaping(r => r.FbId);

                DefaultHasMany(r => r.Stores);
                DefaultHasManyToMany(r => r.Followers);
                DefaultHasManyToMany(r => r.WishProducts);
                DefaultHasManyToMany(r => r.Friends).ParentKeyColumn("UserFriend_Id");
            }

            #endregion
        }

        #endregion
    }
}