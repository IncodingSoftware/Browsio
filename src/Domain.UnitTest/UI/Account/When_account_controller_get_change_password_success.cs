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
    public class When_account_controller_get_change_password_success
    {
        #region Establish value

        static MockController<AccountController> mockController;

        static ExistUserByTokenQuery query;

        static ChangeUserPasswordCommand command;

        static ActionResult result;

        #endregion

        Establish establish = () =>
                                  {

                                      IncStructureResponse<bool> Val = new IncStructureResponse<bool>(true);
                                      query = Pleasure.Generator.Invent<ExistUserByTokenQuery>();
                                      mockController = MockController<AccountController>
                                              .When()
                                              .StubQuery(query, Val);

                                  };

        Because of = () =>  result = mockController.Original.ChangePassword(query);

        It should_be_result = () => result.ShouldBeModel(new ChangeUserPasswordCommand(query.ResetToken));
    }
}
