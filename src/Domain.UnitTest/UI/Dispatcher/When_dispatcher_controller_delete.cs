namespace Browsio.UnitTest
{
    #region << Using >>

    using System.Web.Mvc;
    using Browsio.Controllers;
    using Incoding.CQRS;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(DispatcherController))]
    public class When_dispatcher_controller_delete
    {
        #region Establish value

        static MockController<DispatcherController> mockController;

        static ActionResult result;

        static DeleteEntityByIdCommand input;

        #endregion

        Establish establish = () =>
                                  {
                                      input = Pleasure.Generator.Invent<DeleteEntityByIdCommand>(dsl => dsl.Tuning(r => r.AssemblyQualifiedName, typeof(When_dispatcher_controller_delete).AssemblyQualifiedName));

                                      mockController = MockController<DispatcherController>
                                              .When();
                                  };

        Because of = () => { result = mockController.Original.Delete(input.Id, input.AssemblyQualifiedName); };

        It should_be_success = () => result.ShouldBeIncodingSuccess();

        It should_be_push = () => mockController.ShouldBePush(input);
    }
}