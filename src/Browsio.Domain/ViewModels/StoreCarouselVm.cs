namespace Browsio.Domain
{
    #region << Using >>

    using Incoding.MvcContrib;

    #endregion

    public class StoreCarouselVm : EntityVm
    {
        #region Constructors

        public StoreCarouselVm(Store store, bool isFirst)
                : base(store)
        {
            IsFirst = isFirst;
            Name = store.Name;
            UserId = store.User.Id.ToString();
            User = store.User.FullName;
            Description = store.Description;
        }

        #endregion

        #region Properties

        public bool IsFirst { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public string User { get; set; }

        public string Description { get; set; }

        #endregion
    }
}