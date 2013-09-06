namespace Browsio.Domain
{
    #region << Using >>

    using Incoding.Extensions;
    using Incoding.MvcContrib;

    #endregion

    public class StoreVm : EntityVm
    {
        #region Constructors

        public StoreVm(Store store)
                : base(store)
        {
            Name = store.Name;
            Description = store.Description;
            Category = store.Genre.Category.ToLocalization();            
            Genre = store.Genre.Name;            
        }

        #endregion

        #region Properties

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }
        
        public string Genre { get; set; }

        #endregion
    }
}