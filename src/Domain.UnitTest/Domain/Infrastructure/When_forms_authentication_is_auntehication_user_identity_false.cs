namespace Browsio.UnitTest
{
    using System.Security.Principal;
    using System.Web;
    using Browsio.Domain;
    using Incoding.CQRS;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    [Subject(typeof(FormAuthentication))]
    public class When_forms_authentication_is_auntehication_user_identity_false
    {
        Establish establish = () =>
                                  {
                                      var dispatcher = Pleasure.Mock<IDispatcher>();
                                      var httpContext = Pleasure.MockStrictAsObject<HttpContextBase>(mock => mock.Setup(r => r.User.Identity.IsAuthenticated).Returns(false));

                                      forms = new FormAuthentication(dispatcher.Object, httpContext);
                                  };

        Because of = () => { result = forms.IsAuthentication(); };

        It should_be_result = () => result.ShouldBeFalse();

        static FormAuthentication forms;

        static bool result;
    }
}