namespace Browsio.UnitTest
{
    using Browsio.Controllers;
    using Browsio.Domain;
    using Machine.Specifications;
    using Incoding.MSpecContrib;
    using System.Web.Mvc;

    [Subject(typeof(AccountController))]
    public class When_account_controller_post_sing_with_not_activated
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
                                                                                           .Tuning(r => r.Activated, false));
                                      mockController = MockController<AccountController>.When()
                                          .StubQuery(query, user);

                                  };

        Because of = () => { result = mockController.Original.SignIn(query); };


        It should_be_result = () => result.ShouldBeIncodingFail();
    }
}