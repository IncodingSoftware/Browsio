namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Incoding.MvcContrib;
    using Machine.Specifications;
    using Moq;
    using It = Machine.Specifications.It;

    #endregion

    [Subject(typeof(EditUserCommand))]
    public class When_edit_user
    {
        #region Establish value

        static MockMessage<EditUserCommand, object> mockCommand;

        static Mock<User> user;

        #endregion

        Establish establish = () =>
                                  {
                                      var command = Pleasure.Generator.Invent<EditUserCommand>(dsl => dsl.Tuning(r => r.DisplayPicture, Pleasure.Generator.HttpPostedFile()));

                                      string login = Pleasure.Generator.String();
                                      user = Pleasure.MockStrict<User>(mock =>
                                                                           {
                                                                               mock.SetupGet(r => r.Id).Returns(Pleasure.Generator.TheSameString());
                                                                               mock.SetupGet(r => r.Login).Returns(login);

                                                                               mock.SetupSet(r => r.LastName = command.LastName);
                                                                               mock.SetupSet(r => r.FirstName = command.FirstName);
                                                                               mock.SetupSet(r => r.Sex = command.Sex);

                                                                               var displayPicture = new HttpMemoryPostedFile(command.DisplayPicture).ContentAsBytes;
                                                                               mock.SetupSet(r => r.Image = displayPicture);
                                                                           });

                                      mockCommand = MockCommand<EditUserCommand>
                                              .When(command)
                                              .StubGetById(BrowsioPleasure.UserId, user.Object)
                                              .StubPublish<OnChangeSearchItemEvent>(@event => @event.ShouldEqualWeak(command, dsl => dsl.ForwardToValue(r => r.OwnerId, Pleasure.Generator.TheSameString())
                                                                                                                                        .ForwardToValue(r => r.Query, login)
                                                                                                                                        .ForwardToValue(r => r.Type, SearchItemOfType.User)));
                                  };

        Because of = () => mockCommand.Original.Execute();

        It should_be_verify = () => user.VerifyAll();
    }
}