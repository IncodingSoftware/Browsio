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
    public class When_store_controller_post_add
    {
        #region Establish value

        static MockController<StoreController> mockController;

        static ActionResult result;

        static AddStoreCommand input;

        #endregion

        Establish establish = () =>
                                  {
                                      input = Pleasure.Generator.Invent<AddStoreCommand>();

                                      mockController = MockController<StoreController>
                                              .When();
                                  };

        Because of = () => { result = mockController.Original.Add(input); };

        It should_be_success = () => result.ShouldBeIncodingSuccess();

        It should_be_push = () => mockController.ShouldBePush(input);
    }
}