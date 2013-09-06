namespace Browsio.UnitTest
{
    #region << Using >>

    using System.Web.Mvc;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.Block.IoC;
    using Incoding.MSpecContrib;
    using Machine.Specifications;
    using Moq;
    using It = Machine.Specifications.It;

    #endregion

    [Subject(typeof(AccountController))]
    public class When_account_controller_post_sign_in
    {
        #region Establish value

        static MockController<AccountController> mockController;

        static ActionResult result;

        static GetUserByCredentialQuery query;

        #endregion

        Establish establish = () =>
                                  {
                                      query = Pleasure.Generator.Invent<GetUserByCredentialQuery>();

                                      var user = Pleasure.Generator.Invent<User>(dsl => dsl.Tuning(r => r.Id, Pleasure.Generator.String())
                                          .Tuning(r => r.Activated, true));

                                      formsAuthentication = Pleasure.MockStrict<IFormAuthentication>(mock => mock.Setup(r => r.SignIn(user.Id.ToString(), true)));
                                      IoCFactory.Instance.StubTryResolve(formsAuthentication.Object);

                                      expectedRoute = "/Browsio";

                                      mockController = MockController<AccountController>.When()
                                              .StubQuery(query, user)
                                              .StubUrlAction(expectedRoute);
                                  };

        Because of = () => { result = mockController.Original.SignIn(query); };

        It should_be_result = () => result.ShouldBeIncodingRedirect(expectedRoute);

        static string expectedRoute;

        It should_be_verify = () => formsAuthentication.VerifyAll();

        static Mock<IFormAuthentication> formsAuthentication;
    }
}