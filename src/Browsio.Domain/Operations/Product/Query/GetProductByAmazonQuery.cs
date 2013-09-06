namespace Browsio.Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;
    using Browsio.Amazon;
    using Incoding.Block.IoC;
    using Incoding.CQRS;
    using Incoding.Extensions;

    #endregion

    public class GetProductByAmazonQuery : QueryBase<List<ProductAmazonVm>>
    {
        #region Properties

        public string StoreId { get; set; }

        public string Title { get; set; }

        public int Page { get; set; }

        #endregion

        protected override List<ProductAmazonVm> ExecuteResult()
        {
            if (string.IsNullOrWhiteSpace(Title))
                return new List<ProductAmazonVm>();

            var amazonService = IoCFactory.Instance.TryResolve<IAmazonService>();
            var store = Repository.GetById<Store>(StoreId);

            var items = amazonService.Fetch(Title, Page, store.CategoryAsAmazon);
            return items
                    .Select((r, i) => new ProductAmazonVm(r)
                                          {
                                                  Exist = Repository.Query(whereSpecification: new ProductByStoreWhereSpec(StoreId)
                                                                                   .And(new ProductByASINWhereSpec(r.ASIN)))
                                                                    .Any(), 
                                                  IsLast = items.Count - 1 == i
                                          })
                    .ToList();
        }
    }
}