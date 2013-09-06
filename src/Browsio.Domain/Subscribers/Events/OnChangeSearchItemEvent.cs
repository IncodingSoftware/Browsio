namespace Browsio.Domain
{
    #region << Using >>

    using System;
    using System.Diagnostics.CodeAnalysis;
    using Incoding.EventBroker;
    using Incoding.Maybe;
    using Incoding.Quality;
    using JetBrains.Annotations;

    #endregion

    public class OnChangeSearchItemEvent : IEvent
    {        
        #region Constructors

        [UsedImplicitly, Obsolete(ObsoleteMessage.SerializeConstructor, true), ExcludeFromCodeCoverage]
        public OnChangeSearchItemEvent() { }

        public OnChangeSearchItemEvent(Product product)
        {
            OwnerId = product.Id.ReturnOrDefault(r => r.ToString(), "Debug");
            Query = product.Name;
            Type = SearchItemOfType.Product;
        }

        public OnChangeSearchItemEvent(User user)
        {
            OwnerId = user.Id.ReturnOrDefault(r => r.ToString(), "Debug");
            Query = user.Login;
            Type = SearchItemOfType.User;
        }

        public OnChangeSearchItemEvent(Store store)
        {
            OwnerId = store.Id.ReturnOrDefault(r => r.ToString(), "Debug");
            Query = store.Name;
            Type = SearchItemOfType.Store;
        }

        #endregion

        #region Properties

        public string OwnerId { get; set; }

        public string Query { get; set; }

        public SearchItemOfType Type { get; set; }

        #endregion
    }
}