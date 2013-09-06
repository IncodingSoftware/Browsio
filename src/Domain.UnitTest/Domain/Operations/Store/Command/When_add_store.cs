namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using System.IO;
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Incoding.MvcContrib;
    using Machine.Specifications;
    using Moq;
    using It = Machine.Specifications.It;

    #endregion

    [Subject(typeof(AddStoreCommand))]
    public class When_add_store
    {
        #region Establish value

        static MockMessage<AddStoreCommand, object> mockCommand;

        static Mock<User> user;

        static Stream imageStream;

        static Genre genre;

        #endregion

        Establish establish = () =>
                                  {
                                      var command = Pleasure.Generator.Invent<AddStoreCommand>(dsl => dsl.Tuning(r => r.Image, Pleasure.Generator.HttpPostedFile()));
                                      user = Pleasure.Mock<User>();
                                      genre = Pleasure.Generator.Invent<Genre>();

                                      mockCommand = MockCommand<AddStoreCommand>
                                              .When(command)
                                              .StubGetById(BrowsioPleasure.UserId, user.Object)
                                              .StubGetById(command.GenreId, genre)
                                              .StubPublish<OnChangeSearchItemEvent>(@event => @event.ShouldEqualWeak(command, dsl => dsl.ForwardToAction(r => r.OwnerId, s => s.OwnerId.ShouldNotBeEmpty())
                                                                                                                                        .ForwardToValue(r => r.Query, command.Name)
                                                                                                                                        .ForwardToValue(r => r.Type, SearchItemOfType.Store)));
                                  };

        Because of = () => mockCommand.Original.Execute();

        It should_be_add
                = () => user.Verify(r => r.AddStore(Pleasure.MockIt.IsWeak<Store, AddStoreCommand>(mockCommand.Original, 
                                                                                                   dsl => dsl.IgnoreBecauseNotUse(store => store.Products)
                                                                                                             .IgnoreBecauseCalculate(store => store.CategoryAsAmazon)
                                                                                                             .IgnoreBecauseCalculate(store => store.CategoryAsClass)
                                                                                                             .ForwardToValue(store => store.Genre, genre)
                                                                                                             .ForwardToValue(store => store.Category, (CategoryOfType)mockCommand.Original.Category)
                                                                                                             .ForwardToValue(store => store.Image, new HttpMemoryPostedFile(mockCommand.Original.Image).ContentAsBytes)
                                                                                                             .IgnoreBecauseRoot(store => store.User)
                                                                                                             .IgnoreBecauseNotUse(store => store.Followers)
                                                                                                             .IgnoreBecauseNotUse(store => store.Views))));
    }
}