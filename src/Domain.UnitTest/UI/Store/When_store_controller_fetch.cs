namespace Browsio.UnitTest
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Web.Mvc;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.Extensions;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(StoreController))]
    public class When_store_controller_fetch
    {
        #region Establish value

        static MockController<StoreController> mockController;

        static ActionResult result;

        static GetStoresByUserQuery query;

        static List<Store> expected;

        #endregion

        Establish establish = () =>
                                  {
                                      query = Pleasure.Generator.Invent<GetStoresByUserQuery>();
                                      expected = Pleasure.ToList(Pleasure.Generator.Invent<Store>(dsl => dsl.GenerateTo(r => r.Genre)));

                                      mockController = MockController<StoreController>
                                              .When()
                                              .StubQuery(query, expected);
                                  };

        Because of = () => { result = mockController.Original.Fetch(query); };

        It should_be_result
                = () => result.ShouldBeIncodingData<List<StoreVm>>(vms => vms.ShouldEqualWeakEach(expected, (dsl, i) => dsl.ForwardToValue(r => r.Genre, expected[i].Genre.Name)
                                                                                                                           .ForwardToValue(r => r.Category, expected[i].Genre.Category.ToLocalization())));
    }
}