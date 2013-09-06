namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(DeleteProductByAsinCommand))]
    public class When_delete_product_by_asin
    {
        #region Establish value

        static MockMessage<DeleteProductByAsinCommand, object> mockCommand;

        static Product expected;

        #endregion

        Establish establish = () =>
                                  {
                                      var command = Pleasure.Generator.Invent<DeleteProductByAsinCommand>();
                                      expected = Pleasure.Generator.Invent<Product>();

                                      mockCommand = MockCommand<DeleteProductByAsinCommand>
                                              .When(command)
                                              .StubQuery(whereSpecification: new ProductByASINWhereSpec(command.Asin), 
                                                         entities: expected);
                                  };

        Because of = () => mockCommand.Original.Execute();

        It should_be_delete = () => mockCommand.ShouldBeDelete(expected);
    }
}