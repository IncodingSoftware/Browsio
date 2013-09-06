namespace Browsio.UnitTest
{
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.Block.IoC;
    using Incoding.MSpecContrib;
    using Machine.Specifications;
    using System.Web.Mvc;


    [Subject(typeof(ConnectorController))]
    public class When_connector_controller_add_o_auth_with_authentication
    {
        #region Establish value

        static MockController<ConnectorController> mockController;

        static ActionResult result;

        #endregion

        Establish establish = () =>
                                  {
                                      IoCFactory.Instance.StubTryResolve(Pleasure.MockStrictAsObject<IFormAuthentication>(mock => mock.Setup(r => r.IsAuthentication()).Returns(true)));
                                      type = Pleasure.Generator.Enum<ConnectorOfType>();
                                      expectedRoute = "/Browsio";

                                      mockController = MockController<ConnectorController>
                                              .When()
                                              .StubUrlAction(expectedRoute);
                                  };

        Because of = () => { result = mockController.Original.AddOAuth((int)type); };


        It should_be_result = () => result.ShouldBeIncodingRedirect(expectedRoute);

        static ConnectorOfType type;

        static string expectedRoute;
    }
}