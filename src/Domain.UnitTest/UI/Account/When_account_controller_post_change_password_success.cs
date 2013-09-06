using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browsio.UnitTest
{
    using System.Web.Mvc;
    using Browsio.Controllers;
    using Browsio.Domain;
    using Incoding.CQRS;
    using Incoding.MSpecContrib;
    using Machine.Specifications;



    [Subject(typeof(AccountController))]
    public class When_account_controller_post_change_password_success
    {
        #region Establish value

        static MockController<AccountController> mockController;

        static ExistUserByTokenQuery query;

        static ChangeUserPasswordCommand command;

        static ActionResult result;

        #endregion

        Establish establish = () =>
        {

            command = Pleasure.Generator.Invent<ChangeUserPasswordCommand>();
            mockController = MockController<AccountController>
                    .When();

        };

        Because of = () => result = mockController.Original.ChangePassword(command);

        It should_be_push = () => mockController.ShouldBePush(command);

        It should_be_redirect = () => result.ShouldBeRedirectToAction<AccountController>(r => r.ChangePasswordByEmailSuccess());
    }
}
