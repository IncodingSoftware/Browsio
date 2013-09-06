namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;
    using Moq;
    using It = Machine.Specifications.It;

    #endregion

    [Subject(typeof(AddUserOnOAuthCommand))]
    public class When_add_user_on_o_auth
    {
        public class SocialImplementation : ISocial
        {
            public string Id { get; set; }
        }

        #region Establish value

        static MockMessage<AddUserOnOAuthCommand, object> mockCommand;

        #endregion

        Establish establish = () =>
                                  {
                                      id = Pleasure.Generator.String();
                                      var command = Pleasure.Generator.Invent<AddUserOnOAuthCommand>(dsl =>
                                          dsl.Tuning(r => r.Friends, Pleasure.ToList<ISocial>(new SocialImplementation { Id = id })));

                                      friendUser = Pleasure.MockStrict<User>(
                                          mock =>
                                              {
                                                  mock.SetupSet(r => r.Id);
                                                  mock.Setup(r1 => r1.AddFriend(Pleasure.MockIt.Is<User>(user1 => user1.ShouldEqualWeak(
                                                      command, 
                                                      dsl => dsl.ForwardToValue(r => r.Password, null)
                                                                                                                              .ForwardToValue(r => r.Activated, true)
                                                                                                                              .IgnoreBecauseNotUse(r => r.Followers)
                                                                                                                              .IgnoreBecauseNotUse(r => r.WishProducts)
                                                                                                                              .IgnoreBecauseCalculate(r => r.FullName)
                                                                                                                              .ForwardToValue(r => r.ResetToken, null)
                                                                                                                              .IgnoreBecauseNotUse(r => r.Stores)
                                                                                                                              .IgnoreBecauseNotUse(r => r.Friends)
                                                                                                                              .IgnoreBecauseNotUse(r => r.Image))
                                                                                      )));
                                              }
                                          );
                                      mockCommand = MockCommand<AddUserOnOAuthCommand>
                                              .When(command)
                                              .StubQuery(whereSpecification: new UserByFBIdWhereSpec(id), 
                                               entities: friendUser.Object)
                                              .StubPublish<OnChangeSearchItemEvent>(@event => @event.ShouldEqualWeak(command, dsl => dsl.ForwardToAction(r => r.OwnerId, s => s.OwnerId.ShouldNotBeEmpty())
                                                                                                                                        .ForwardToValue(r => r.Query, command.Login)
                                                                                                                                        .ForwardToValue(r => r.Type, SearchItemOfType.User)));
                                  };

        Because of = () => mockCommand.Original.Execute();

        It should_be_save = () => mockCommand.ShouldBeSave<User>(
                                                                 user => user.ShouldEqualWeak(
                                                                                              mockCommand.Original,
                                                                                              dsl => dsl
                                                                                                             .ForwardToValue(r => r.Password, null)
                                                                                                             .ForwardToValue(r => r.Activated, true)
                                                                                                             .IgnoreBecauseNotUse(r => r.Followers)
                                                                                                             .IgnoreBecauseNotUse(r => r.WishProducts)
                                                                                                             .IgnoreBecauseCalculate(r => r.FullName)
                                                                                                             .ForwardToValue(r => r.ResetToken, null)
                                                                                                             .IgnoreBecauseNotUse(r => r.Stores)
                                                                                                             .ForwardToAction(r => r.Friends,
                                                                                                             user1 => user1.Friends[0].ShouldEqual(friendUser.Object))
                                                                                                             .IgnoreBecauseNotUse(r => r.Image))
                                          );

        static string id;

        static Mock<User> friendUser;
    }
}