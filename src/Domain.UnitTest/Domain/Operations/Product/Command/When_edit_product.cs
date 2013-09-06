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

    [Subject(typeof(EditProductCommand))]
    public class When_edit_product
    {
        #region Establish value

        static MockMessage<EditProductCommand, object> mockCommand;

        static Mock<Product> product;

        #endregion

        Establish establish = () =>
                                  {
                                      var command = Pleasure.Generator.Invent<EditProductCommand>(dsl => dsl.Tuning(r => r.Image, Pleasure.Generator.HttpPostedFile()));
                                      product = Pleasure.MockStrict<Product>(mock =>
                                                                                 {
                                                                                     mock.SetupGet(r => r.Id).Returns(Pleasure.Generator.TheSameString());
                                                                                     mock.SetupGet(r => r.Name).Returns(command.Name);

                                                                                     mock.SetupSet(r => r.Name = command.Name);
                                                                                     mock.SetupSet(r => r.Author = command.Author);
                                                                                     mock.SetupSet(r => r.Description = command.Description);
                                                                                     mock.SetupSet(r => r.Price = command.Price);
                                                                                     mock.SetupSet(r => r.Asin = command.Asin);
                                                                                     var content = new HttpMemoryPostedFile(command.Image).ContentAsBytes;
                                                                                     mock.SetupSet(r => r.Image = content);
                                                                                 });

                                      mockCommand = MockCommand<EditProductCommand>
                                              .When(command)
                                              .StubGetById(command.Id, product.Object)
                                              .StubPublish<OnChangeSearchItemEvent>(@event => @event.ShouldEqualWeak(command, dsl => dsl.ForwardToValue(r => r.Type, SearchItemOfType.Product)
                                                                                                                                        .ForwardToValue(r => r.Query, command.Name)
                                                                                                                                        .ForwardToValue(r => r.OwnerId, Pleasure.Generator.TheSameString())));
                                  };

        Because of = () => mockCommand.Original.Execute();

        It should_be_verify = () => product.VerifyAll();
    }
}