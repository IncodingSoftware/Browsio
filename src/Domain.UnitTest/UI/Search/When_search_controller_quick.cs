namespace Browsio.UnitTest
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Web.Mvc;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(SearchItemController))]
    public class When_search_controller_quick
    {
        #region Establish value

        static MockController<SearchItemController> mockController;

        static ActionResult result;

        static GetSearchItemsQuery query;

        static SearchItem product;

        static SearchItem store;

        static SearchItem user;

        #endregion

        Establish establish = () =>
                                  {
                                      query = Pleasure.Generator.Invent<GetSearchItemsQuery>();

                                      product = Pleasure.Generator.Invent<SearchItem>(dsl => dsl.Tuning(r => r.Type, SearchItemOfType.Product));
                                      store = Pleasure.Generator.Invent<SearchItem>(dsl => dsl.Tuning(r => r.Type, SearchItemOfType.Store));
                                      user = Pleasure.Generator.Invent<SearchItem>(dsl => dsl.Tuning(r => r.Type, SearchItemOfType.User));
                                      var expected = Pleasure.ToList(product, store, user);


                                      mockController = MockController<SearchItemController>
                                              .When()
                                              .StubQuery(query, expected);
                                  };

        Because of = () => { result = mockController.Original.Fetch(query); };

        It should_be_result = () => result.ShouldBeIncodingData<List<SearchItemVm>>(vms =>
                                                                                        {

                                                                                            vms.Count.ShouldEqual(6);

                                                                                            vms[0].ShouldEqualWeak(new { IsHeader = true, Message = "Product", Id = product.Id, Type = SearchItemOfType.Product }, dsl => dsl.IgnoreBecauseNotUse(r => r.OwnerId));
                                                                                            vms[1].ShouldEqualWeak(product, dsl => dsl.Forward(r => r.Id, r => r.Id)
                                                                                                                                      .Forward(r => r.OwnerId, r => r.OwnerId)
                                                                                                                                      .ForwardToValue(r => r.IsHeader, false)
                                                                                                                                      .Forward(r => r.Message, r => r.Query)
                                                                                                                                      .Forward(r => r.Type, r => r.Type));

                                                                                            vms[2].ShouldEqualWeak(new { IsHeader = true, Message = "Store", Id = store.Id, Type = SearchItemOfType.Store }, dsl => dsl.IgnoreBecauseNotUse(r => r.OwnerId));
                                                                                            vms[3].ShouldEqualWeak(store, dsl => dsl.Forward(r => r.Id, r => r.Id)
                                                                                                                                      .Forward(r => r.OwnerId, r => r.OwnerId)
                                                                                                                                      .Forward(r => r.Message, r => r.Query)
                                                                                                                                      .Forward(r => r.Type, r => r.Type)
                                                                                                                                      .ForwardToValue(r => r.IsHeader, false));

                                                                                            vms[4].ShouldEqualWeak(new { IsHeader = true, Message = "User", Id = user.Id, Type = SearchItemOfType.User }, dsl => dsl.IgnoreBecauseNotUse(r => r.OwnerId));
                                                                                            vms[5].ShouldEqualWeak(user, dsl => dsl.Forward(r => r.Id, r => r.Id)
                                                                                                                                      .Forward(r => r.OwnerId, r => r.OwnerId)
                                                                                                                                      .Forward(r => r.Message, r => r.Query)
                                                                                                                                      .Forward(r => r.Type, r => r.Type)
                                                                                                                                      .ForwardToValue(r => r.IsHeader, false));

                                                                                        });
    }
}