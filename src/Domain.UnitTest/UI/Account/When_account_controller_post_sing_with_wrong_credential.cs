namespace Browsio.UnitTest
{
    #region << Using >>

    using System.Web.Mvc;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(AccountController))]
    public class When_account_controller_post_sing_with_wrong_credential
    {
        #region Establish value

        static MockController<AccountController> mockController;

        static ActionResult result;

        static GetUserByCredentialQuery query;

        #endregion

        Establish establish = () =>
                                  {
                                      query = Pleasure.Generator.Invent<GetUserByCredentialQuery>();

                                      mockController = MockController<AccountController>
                                              .When().BrokenModelState();
                                  };

        Because of = () => { result = mockController.Original.SignIn(query); };

        It should_be_result = () => result.ShouldBeIncodingFail();

    }
}