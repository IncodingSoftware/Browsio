namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using Browsio.Domain;
    using Incoding;
    using Incoding.Extensions;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(EditStoreCommand))]
    public class When_edit_store_with_exist_command
    {
        #region Establish value

        static MockMessage<EditStoreCommand, object> mockCommand;

        static IncWebException exception;

        #endregion

        Establish establish = () =>
                                  {
                                      var command = Pleasure.Generator.Invent<EditStoreCommand>();

                                      mockCommand = MockCommand<EditStoreCommand>
                                              .When(command)
                                              .StubGetById(command.Id, Pleasure.MockAsObject<Store>(mock => mock.SetupGet(r => r.Category).Returns(((CategoryOfType)command.Category).Inverse<CategoryOfType>())))
                                              .StubNotEmptyQuery(whereSpecification: new StoreByCategoryOptWhereSpec((CategoryOfType)command.Category).And(new StoreByUserWhereSpec(BrowsioPleasure.UserId)));
                                  };

        Because of = () => { exception = Catch.Exception(() => mockCommand.Original.Execute()) as IncWebException; };

        It should_be_exception = () => exception.Should(webException =>
                                                            {
                                                                webException.Message.ShouldNotBeEmpty();
                                                                webException.Property.ShouldEqual("Category");
                                                            });
    }
}