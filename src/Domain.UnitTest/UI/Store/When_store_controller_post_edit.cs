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
    public class When_store_controller_post_edit
    {
        #region Establish value

        static MockController<StoreController> mockController;

        static ActionResult result;

        static EditStoreCommand input;

        #endregion

        Establish establish = () =>
                                  {
                                      input = Pleasure.Generator.Invent<EditStoreCommand>();

                                      mockController = MockController<StoreController>
                                              .When();
                                  };

        Because of = () => { result = mockController.Original.Edit(input); };

        It should_be_success = () => result.ShouldBeIncodingSuccess();

        It should_be_push = () => mockController.ShouldBePush(input);
    }
}