namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using Browsio.Domain;
    using Incoding;
    using Incoding.Extensions;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(AddStoreCommand))]
    public class When_add_store_with_exist_category
    {
        #region Establish value

        static MockMessage<AddStoreCommand, object> mockCommand;

        static IncWebException exception;

        #endregion

        Establish establish = () =>
                                  {
                                      var command = Pleasure.Generator.Invent<AddStoreCommand>();

                                      mockCommand = MockCommand<AddStoreCommand>
                                              .When(command)
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