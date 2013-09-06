namespace Browsio.UnitTest
{
    #region << Using >>

    using System.Web.Mvc;
    using Browsio.Controllers;
    using Incoding.MSpecContrib;
    using Machine.Specifications;
    
    #endregion

    [Subject(typeof(TemplateController))]
    public class When_template_controller_get
    {
        #region Establish value

        static MockController<TemplateController> mockController;

        static ActionResult result;

        static string name;

        #endregion

        Establish establish = () =>
                                  {
                                      name = Pleasure.Generator.String();

                                      mockController = MockController<TemplateController>
                                              .When();
                                  };

        Because of = () => { result = mockController.Original.Get(name); };

        It should_be_result = () => result.ShouldBeIncodingSuccess();

        It should_be_render = () => mockController.ShouldBeRenderView(name);
    }
}