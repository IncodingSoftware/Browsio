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

    [Subject(typeof(EditStoreCommand))]
    public class When_edit_store_same_category
    {
        #region Establish value

        static MockMessage<EditStoreCommand, object> mockCommand;

        static Mock<Store> store;

        #endregion

        Establish establish = () =>
                                  {
                                      var command = Pleasure.Generator.Invent<EditStoreCommand>(dsl => dsl.Tuning(r => r.Image, Pleasure.Generator.HttpPostedFile()));
                                      var genre = Pleasure.Generator.Invent<Genre>();
                                      store = Pleasure.MockStrict<Store>(mock =>
                                                                             {
                                                                                 mock.SetupGet(r => r.Id).Returns(Pleasure.Generator.TheSameString());
                                                                                 mock.SetupGet(r => r.Name).Returns(command.Name);
                                                                                 mock.SetupGet(r => r.Category).Returns((CategoryOfType)command.Category);

                                                                                 mock.SetupSet(r => r.Name = command.Name);
                                                                                 mock.SetupSet(r => r.Description = command.Description);
                                                                                 mock.SetupSet(r => r.Category = (CategoryOfType)command.Category);
                                                                                 mock.SetupSet(r => r.Genre = genre);

                                                                                 var imageAsByte = new HttpMemoryPostedFile(command.Image).ContentAsBytes;
                                                                                 mock.SetupSet(r => r.Image = imageAsByte);
                                                                             });

                                      mockCommand = MockCommand<EditStoreCommand>
                                              .When(command)
                                              .StubGetById(command.Id, store.Object)
                                              .StubGetById(command.GenreId, genre)
                                              .StubPublish<OnChangeSearchItemEvent>(@event => @event.ShouldEqualWeak(command, dsl => dsl.ForwardToValue(r => r.OwnerId, Pleasure.Generator.TheSameString())
                                                                                                                                        .ForwardToValue(r => r.Query, command.Name)
                                                                                                                                        .ForwardToValue(r => r.Type, SearchItemOfType.Store)));
                                  };

        Because of = () => mockCommand.Original.Execute();

        It should_be_verify = () => store.VerifyAll();
    }
}