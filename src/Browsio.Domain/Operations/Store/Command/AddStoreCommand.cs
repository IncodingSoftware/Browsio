namespace Browsio.Domain
{
    #region << Using >>

    using System.Linq;
    using System.Web;
    using Incoding;
    using Incoding.CQRS;
    using Incoding.Extensions;
    using Incoding.Maybe;
    using Incoding.MvcContrib;

    #endregion

    public class AddStoreCommand : CommandBase, IStoreCommand
    {
        #region IStoreCommand Members

        public string GenreId { get; set; }

        public int Category { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public HttpPostedFileBase Image { get; set; }

        #endregion

        public override void Execute()
        {
            var categoryOfType = (CategoryOfType)Category;
            if (Repository.Query(whereSpecification: new StoreByCategoryOptWhereSpec(categoryOfType).And(new StoreByUserWhereSpec(BrowsioApp.UserId)))
                          .Any())
                throw IncWebException.For<AddStoreCommand>(r => r.Category, "Store with {0} category exist".F(categoryOfType.ToLocalization()));

            var user = Repository.GetById<User>(BrowsioApp.UserId);
            var store = new Store
                            {
                                    Category = categoryOfType, 
                                    Description = Description, 
                                    Image = Image.ReturnOrDefault(@base => new HttpMemoryPostedFile(@base).ContentAsBytes, new byte[0]), 
                                    Genre = Repository.GetById<Genre>(GenreId), 
                                    Name = Name
                            };
            user.AddStore(store);

            EventBroker.Publish(new OnChangeSearchItemEvent(store));
        }
    }
}