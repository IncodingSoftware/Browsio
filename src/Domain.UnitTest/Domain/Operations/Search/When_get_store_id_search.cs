using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browsio.UnitTest
{
    using Browsio.Domain;
    using Browsio.Domain.Search.Query;
    using Incoding.CQRS;
    using Incoding.MSpecContrib;
    using Machine.Specifications;
    using Incoding.Extensions;

    [Subject(typeof(GetStoreIdsBySearchIdsQuery))]
    public class When_get_store_id_search
    {
        #region Establish value

        static MockMessage<GetStoreIdsBySearchIdsQuery, string[]> mockQuery;

        static SearchItem searchItemStore;

        static SearchItem searchItemProduct;

        static SearchItem searchItemUser;

        static Product product;
        
                #endregion

        Establish establish = () =>
                                  {
                                      var query = Pleasure.Generator.Invent<GetStoreIdsBySearchIdsQuery>();

                                      searchItemStore = Pleasure.Generator.Invent<SearchItem>(dsl => dsl.Tuning(r => r.Type, SearchItemOfType.Store));

                                      searchItemProduct = Pleasure.Generator.Invent<SearchItem>(dsl => dsl.Tuning(r => r.Type, SearchItemOfType.Product));

                                      searchItemUser = Pleasure.Generator.Invent<SearchItem>(dsl => dsl.Tuning(r => r.Type, SearchItemOfType.User));

                                      product = Pleasure.Generator.Invent<Product>(dsl => dsl.Tuning(r => r.Store, Pleasure.Generator.Invent<Store>()));

                                      userStore = Pleasure.Generator.Invent<Store>();
                                      var user = Pleasure.Generator.Invent<User>();
                                      user.SetValue("stores", Pleasure.ToList(userStore));

                                      mockQuery = MockQuery<GetStoreIdsBySearchIdsQuery, string[]>
                                              .When(query)
                                              .StubQuery(whereSpecification: new EntityContainIdSpec<SearchItem>(query.SearchIds)
                                                         , entities: new[] { searchItemProduct, searchItemStore, searchItemUser })
                                              .StubGetById<Product>(searchItemProduct.OwnerId
                                                                    , product)
                                              .StubQuery(whereSpecification: new ProductByASINWhereSpec(product.Asin)
                                                         , entities: product)
                                              .StubGetById<User>(searchItemUser.OwnerId, user);

                                  };

        Because of = () => mockQuery.Original.Execute();

        It should_be_resultStore1 = () => mockQuery.ShouldBeIsResult(r =>
                                                                         {
                                                                             r.Length.ShouldEqual(3);
                                                                             r[0].ShouldEqual(product.Store.Id);
                                                                             r[1].ShouldEqual(searchItemStore.OwnerId);
                                                                             r[2].ShouldEqual(userStore.Id);
                                                                         });

        static Store userStore;
    }
}