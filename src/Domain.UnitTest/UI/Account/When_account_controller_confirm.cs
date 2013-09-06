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
    using Incoding.MSpecContrib;
    using Machine.Specifications;



    [Subject(typeof(AccountController))]
    public class When_account_controller_confirm
    {
        #region Establish value

        static MockController<AccountController> mockController;

        static ActionResult result;

        static ConfirUserCommand input;

        #endregion

        Establish establish = () =>
                                  {
                                      input = Pleasure.Generator.Invent<ConfirUserCommand>();

                                      mockController = MockController<AccountController>
                                              .When();
                                  };

        Because of = () => { result = mockController.Original.Confirm(input); };


        It should_be_success = () => result.ShouldBeRedirectToAction<AccountController>(r => r.ConfirmSuccess());

        It should_be_push = () => mockController.ShouldBePush(input);
    }
}
