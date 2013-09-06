namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(AddUserCommand))]
    public class When_add_user
    {
        #region Establish value

        static MockMessage<AddUserCommand, object> mockCommand;

        #endregion

        Establish establish = () =>
                                  {
                                      var command = Pleasure.Generator.Invent<AddUserCommand>();

                                      mockCommand = MockCommand<AddUserCommand>
                                              .When(command)
                                              .StubPublish<OnSendEmailEvent>(@event => @event.ShouldEqualWeak(new { To = command.Login, Subject = "Confirm registration" }, 
                                                                                                              dsl => dsl.ForwardToAction(r => r.Body, r => r.Body.ShouldContain("?UserId"))))
                                              .StubPublish<OnChangeSearchItemEvent>(@event => @event.ShouldEqualWeak(command, dsl => dsl.ForwardToAction(r => r.OwnerId, s => s.OwnerId.ShouldNotBeEmpty())
                                                                                                                                        .ForwardToValue(r => r.Query, command.Login)
                                                                                                                                        .ForwardToValue(r => r.Type, SearchItemOfType.User)));
                                  };

        Because of = () => mockCommand.Original.Execute();

        It should_be_save = () => mockCommand.ShouldBeSave<User>(user => user.ShouldEqualWeak(mockCommand.Original, dsl => dsl.ForwardToValue(r => r.Activated, false)
                                                                                                                              .ForwardToValue(r => r.ResetToken, null)
                                                                                                                              .ForwardToValue(r => r.FirstName, null)
                                                                                                                              .ForwardToValue(r => r.LastName, null)
                                                                                                                              .IgnoreBecauseNotUse(r => r.Sex)
                                                                                                                              .IgnoreBecauseNotUse(r => r.WishProducts)
                                                                                                                              .IgnoreBecauseNotUse(r => r.Followers)
                                                                                                                              .ForwardToValue(r => r.FbId,null)
                                                                                                                              .IgnoreBecauseCalculate(r => r.FullName)
                                                                                                                              .IgnoreBecauseNotUse(r => r.Stores)
                                                                                                                              .IgnoreBecauseNotUse(r => r.Image)
                                                                                                                              .IgnoreBecauseNotUse(r => r.Friends)
                                                                                                                              .ForwardToValue(r => r.AccessToken, null)));

        It should_be_pusblished = () => mockCommand.ShouldBePublished();
    }
}