namespace Browsio.UnitTest
{
    using System;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.Block.IoC;
    using Incoding.MSpecContrib;
    using Machine.Specifications;
    using System.Web.Mvc;
    using Incoding.Extensions;

    [Subject(typeof(ConnectorController))]
    public class When_connector_controller_add_o_auth_with_wrong_authentication
    {
        #region Establish value

        static MockController<ConnectorController> mockController;

        static ActionResult result;

        #endregion

        Establish establish = () =>
                                  {
                                      IoCFactory.Instance.StubTryResolve(Pleasure.MockStrictAsObject<IFormAuthentication>(mock => mock.Setup(r => r.IsAuthentication()).Returns(false)));
                                      type = ConnectorOfType.Facebook;
                                      url = Pleasure.Generator.String();

                                      const string callbackUrl = "http://sample.com/Connector/Success?connectorOfType=Facebook&type=client_cred";
                                      IOAuthProvider oAuthProvider = Pleasure.MockStrictAsObject<IOAuthProvider>(mock => mock
                                                                                                                        .Setup(r => r.GetAuthorizationUri(Pleasure.MockIt.IsStrong(new Uri(callbackUrl))))
                                                                                                                        .Returns(url));
                                      IoCFactory.Instance.StubTryResolveByNamed(type.ToString(), oAuthProvider);

                                      mockController = MockController<ConnectorController>
                                              .When()
                                              .StubUrlAction("/Connector/Success".AppendToQueryString(new { connectorOfType = type.ToString(), type = "client_cred" }))
                                              .StubRequestUrl(new Uri("http://sample.com"));
                                  };

        Because of = () => { result = mockController.Original.AddOAuth((int)type); };


        It should_be_result = () => result.ShouldBeIncodingRedirect(url);

        static ConnectorOfType type;

        static string url;
    }
}