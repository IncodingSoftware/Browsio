namespace Browsio.UnitTest
{
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;
    using System.Web.Mvc;


    [Subject(typeof(DispatcherController))]
    public class When_dispatcher_controller
    {
        #region Establish value

        static MockController<DispatcherController> mockController;

        static FileContentResult result;

        #endregion

        Establish establish = () =>
                                  {
                                      query = Pleasure.Generator.Invent<GetImageQuery>();
                                      expected = Pleasure.Generator.Bytes();

                                      mockController = MockController<DispatcherController>
                                              .When()
                                              .StubQuery(query, expected);
                                  };

        Because of = () => { result = mockController.Original.Img(query); };


        It should_be_result = () => result.FileContents.ShouldEqualWeakEach(expected);

        static GetImageQuery query;

        static byte[] expected;
    }
}