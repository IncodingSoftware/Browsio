namespace Browsio.UnitTest
{
    #region << Using >>

    using System.Web.Mvc;
    using Browsio.Controllers;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(BrowseController))]
    public class When_browse_cotroller_index
    {
        #region Establish value

        static MockController<BrowseController> mockController;

        static ActionResult result;

        #endregion

        Establish establish = () =>
                                  {
                                      mockController = MockController<BrowseController>
                                              .When();
                                  };

        Because of = () => { result = mockController.Original.Index(); };

        It should_be_result = () => result.ShouldBeIncodingSuccess();

        It should_be_render = () => mockController.ShouldBeRenderView();
    }
}