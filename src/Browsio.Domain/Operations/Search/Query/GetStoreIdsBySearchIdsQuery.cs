namespace Browsio.Domain.Search.Query
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Incoding.CQRS;

    public class GetStoreIdsBySearchIdsQuery : QueryBase<string[]>
    {
        public string[] SearchIds { get; set; }

        protected override string[] ExecuteResult()
        {

            var storeIds = new List<string>();
            foreach (var item in Repository.Query(whereSpecification:new EntityContainIdSpec<SearchItem>(SearchIds)))   
            {
                switch (item.Type)
                {
                    case SearchItemOfType.Product:
                        var product = Repository.GetById<Product>(item.OwnerId);
                        storeIds.AddRange(this.Repository.Query(whereSpecification: new ProductByASINWhereSpec(product.Asin)).Select(r => r.Store.Id.ToString()).ToList());
                        break;
                    case SearchItemOfType.Store:
                        storeIds.Add(item.OwnerId);
                        break;
                    case SearchItemOfType.User:
                        storeIds.AddRange(Repository.GetById<User>(item.OwnerId).Stores.ToList().Select(r => r.Id.ToString()).ToList());
                        break;
                }
            }
            return storeIds.ToArray();
        }
    }
}