namespace Browsio.UnitTest
{
    #region << Using >>

    using System.Web.Mvc;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(DispatcherController))]
    public class When_dispatcher_controller_get_img
    {
        #region Establish value

        static MockController<DispatcherController> mockController;

        static FileContentResult result;

        static GetImageQuery query;

        static byte[] expected;

        #endregion

        Establish establish = () =>
                                  {
                                      query = Pleasure.Generator.Invent<GetImageQuery>();
                                      expected = Pleasure.Generator.Bytes();

                                      mockController = MockController<DispatcherController>
                                              .When()
                                              .StubQuery(query, expected);
                                  };

        Because of = () => result = mockController.Original.Img(query);

        It should_be_result = () => result.FileContents.ShouldEqualWeakEach(expected);
    }
}