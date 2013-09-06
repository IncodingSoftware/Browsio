namespace Browsio.UnitTest
{
    using System.Web.Mvc;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.Block.IoC;
    using Incoding.MSpecContrib;
    using Machine.Specifications;
    using Moq;
    using It = Machine.Specifications.It;

    [Subject(typeof(AccountController))]
    public class When_account_controller_sign_out
    {
        #region Establish value

        static MockController<AccountController> mockController;

        static string expectedRoute;

        static Mock<IFormAuthentication> formAuthentication;

        static ActionResult result;

                #endregion

        Establish establish = () =>
                                  {
                                      formAuthentication = Pleasure.MockStrict<IFormAuthentication>(mock => mock.Setup(r => r.SignOut()));
                                      IoCFactory.Instance.StubTryResolve(formAuthentication.Object);
                                      expectedRoute = "/Browsio";
                                      mockController = MockController<AccountController>
                                              .When()
                                              .StubUrlAction(expectedRoute);

                                  };

        Because of = () => result = mockController.Original.SignOut();

        It should_be_verify = () => formAuthentication.VerifyAll();

        It should_be_result = () => result.ShouldBeIncodingRedirect(expectedRoute);

    }
}