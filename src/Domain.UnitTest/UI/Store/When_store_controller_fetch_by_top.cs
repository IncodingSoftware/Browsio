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

    [Subject(typeof(StoreController))]
    public class When_store_controller_fetch_by_top
    {
        #region Establish value

        static MockController<StoreController> mockController;

        static ActionResult result;

        static GetStoresByTopQuery query;

        static List<Store> expected;

        #endregion

        Establish establish = () =>
                                  {
                                      query = Pleasure.Generator.Invent<GetStoresByTopQuery>();

                                      var user = Pleasure.Generator.Invent<User>(factoryDsl => factoryDsl.Tuning(r => r.Id, Pleasure.Generator.String()));
                                      var store = Pleasure.Generator.Invent<Store>(dsl => dsl.Tuning(r => r.User, user));
                                      expected = Pleasure.ToList(store, store);

                                      mockController = MockController<StoreController>
                                              .When()
                                              .StubQuery(query, expected);
                                  };

        Because of = () => { result = mockController.Original.FetchByTop(query); };

        It should_be_result
                = () => result.ShouldBeIncodingData<List<StoreCarouselVm>>(vms => vms.ShouldEqualWeakEach(expected, 
                                                                                                          (dsl, i) => dsl.ForwardToValue(r => r.User, expected[i].User.FullName)
                                                                                                                         .ForwardToValue(r => r.UserId, expected[i].User.Id.ToString())
                                                                                                                         .ForwardToAction(r => r.IsFirst, vm =>
                                                                                                                                                              {
                                                                                                                                                                  if (i == 0)
                                                                                                                                                                      vm.IsFirst.ShouldBeTrue();
                                                                                                                                                                  else
                                                                                                                                                                      vm.IsFirst.ShouldBeFalse();
                                                                                                                                                              })));
    }
}