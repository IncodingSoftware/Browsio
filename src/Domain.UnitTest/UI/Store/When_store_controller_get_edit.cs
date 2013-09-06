namespace Browsio.UnitTest
{
    #region << Using >>

    using System.Web.Mvc;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(StoreController))]
    public class When_store_controller_get_edit
    {
        #region Establish value

        static MockController<StoreController> mockController;

        static ActionResult result;

        static GetStoreByIdQuery query;

        static Store expected;

        #endregion

        Establish establish = () =>
                                  {
                                      query = Pleasure.Generator.Invent<GetStoreByIdQuery>();
                                      expected = Pleasure.Generator.Invent<Store>(dsl => dsl.GenerateTo(r => r.Genre));

                                      mockController = MockController<StoreController>
                                              .When()
                                              .StubQuery(query, expected);
                                  };

        Because of = () => { result = mockController.Original.Edit(query); };

        It should_be_result = () => result.ShouldBeIncodingSuccess();

        It should_be_render
                = () => mockController.ShouldBeRenderModel<EditStoreCommand>(command => command.ShouldEqualWeak(expected, dsl => dsl.ForwardToValue(r => r.GenreId, expected.Genre.Id.ToString())
                                                                                                                                    .ForwardToValue(r => r.Category, (int)expected.Category)
                                                                                                                                    .IgnoreBecauseNotUse(r => r.Image)));
    }
}