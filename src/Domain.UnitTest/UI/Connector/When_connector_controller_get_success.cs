namespace Browsio.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.Block.IoC;
    using Incoding.CQRS;
    using Incoding.Data;
    using Incoding.Extensions;
    using Incoding.MSpecContrib;
    using Machine.Specifications;
    using Moq;
    using It = Machine.Specifications.It;

    [Subject(typeof(ConnectorController))]
    public class When_connector_controller_get_success
    {
        #region Establish value

        static MockController<ConnectorController> mockController;

        static ConnectorOfType connectorOfType;

        static Mock<IOAuthProvider> oAuthProvider;

        static ActionResult result;

        #endregion

        Establish establish = () =>
                                  {
                                      var extendedArgs = new Dictionary<string, string>()
                                                             {
                                                                     {"code","VA"}
                                                             };
                                      string accessToken = "CAACEdEose0cBALg6h5IBB01e4bnUCFogqhIR2DNzHuod2fYONj9sxIUL8rM1DbxLU5tX8rbPrUPHSYV5Q3672NQcbZAKCzpDrdEs0CotjOMjuNrfWvhI3677crt0XXNUYjyBZB1QXoKtL7ZCWgmXFu7aJAclY3MpTy135togQZDZD";
                                      oAuthProvider = Pleasure.Mock<IOAuthProvider>(mock => mock.Setup(r => r.GetAuthorizeToken(Pleasure.MockIt.Is<Dictionary<string, string>>(dictionary => dictionary.ShouldNotBeNull()))).Returns(accessToken));
                                      connectorOfType = ConnectorOfType.Facebook;
                                      IoCFactory.Instance.StubTryResolveByNamed(connectorOfType.ToString(), oAuthProvider.Object);
                                      Mock<ISocialUser> socialUser = Pleasure.Mock<ISocialUser>(mock => mock.Setup(r => r.Id).Returns(Pleasure.Generator.PositiveNumber().ToString));
                                      var connectProviderClient = Pleasure.Mock<IConnectProviderClient>(mock => mock.Setup(r => r.Client(accessToken)).Returns(socialUser.Object));
                                      IoCFactory.Instance.StubTryResolveByNamed(connectorOfType.ToString(), connectProviderClient.Object);
                                      var query = Pleasure.Generator.Invent<ExistFacebookUserQuery>();

                                      mockController = MockController<ConnectorController>
                                              .When()
                                              .StubQueryString("")
                                              .StubRequestUrl(new Uri("http://sample.com"))
                                              .StubUrlAction("/Browsio")
                                              .StubUrlAction("/Connector/Success".AppendToQueryString(new { connectorOfType = connectorOfType.ToString(), type = "client_cred" }))
                                              .StubQuery(new ExistFacebookUserQuery { FBId = socialUser.Object.Id }, new IncStructureResponse<bool>(false));
                                  };

        Because of = () => result = mockController.Original.Success(connectorOfType);

        It should_be_redirect = () => result.ShouldBeRedirectToAction<BrowsioController>(r => r.Index());
    }
}