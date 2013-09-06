namespace Browsio.Domain
{
    #region << Using >>

    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Web;
    using Incoding;
    using Incoding.CQRS;
    using Incoding.Extensions;
    using Incoding.MvcContrib;
    using Incoding.Quality;
    using JetBrains.Annotations;

    #endregion

    public class EditStoreCommand : CommandBase, IStoreCommand
    {
        #region Constructors

        [UsedImplicitly, Obsolete(ObsoleteMessage.SerializeConstructor, true), ExcludeFromCodeCoverage]
        public EditStoreCommand() { }

        public EditStoreCommand(Store store)
        {
            Id = store.Id.ToString();
            Category = (int)store.Category;
            Description = store.Description;
            Name = store.Name;
            GenreId = store.Genre.Id.ToString();
        }

        #endregion

        #region Properties

        public string Id { get; set; }

        #endregion

        #region IStoreCommand Members

        public int Category { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public HttpPostedFileBase Image { get; set; }

        public string GenreId { get; set; }

        #endregion

        public override void Execute()
        {
            var categoryOfType = (CategoryOfType)Category;

            var store = Repository.GetById<Store>(Id);
            if (store.Category != categoryOfType && Repository.Query(whereSpecification: new StoreByCategoryOptWhereSpec(categoryOfType).And(new StoreByUserWhereSpec(BrowsioApp.UserId)))
                                                              .Any())
                throw IncWebException.For<AddStoreCommand>(r => r.Category, "Store with {0} category exist".F(categoryOfType.ToLocalization()));

            store.Category = categoryOfType;
            store.Name = Name;
            store.Description = Description;
            store.Genre = Repository.GetById<Genre>(GenreId);
            if (Image != null)
                store.Image = new HttpMemoryPostedFile(Image).ContentAsBytes;

            EventBroker.Publish(new OnChangeSearchItemEvent(store));
        }
    }
}