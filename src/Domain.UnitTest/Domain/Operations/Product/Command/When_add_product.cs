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

    [Subject(typeof(AddProductCommand))]
    public class When_add_product
    {
        #region Establish value

        static MockMessage<AddProductCommand, object> mockCommand;

        static Mock<Store> store;

        #endregion

        Establish establish = () =>
                                  {
                                      var command = Pleasure.Generator.Invent<AddProductCommand>(dsl => dsl.Tuning(r => r.Image, Pleasure.Generator.HttpPostedFile()));
                                      store = Pleasure.Mock<Store>();

                                      mockCommand = MockCommand<AddProductCommand>
                                              .When(command)
                                              .StubGetById(command.StoreId, store.Object)
                                              .StubPublish<OnChangeSearchItemEvent>(@event => @event.ShouldEqualWeak(command, dsl => dsl.ForwardToValue(r => r.Type, SearchItemOfType.Product)
                                                                                                                                        .ForwardToValue(r => r.Query, command.Name)
                                                                                                                                        .ForwardToAction(r => r.OwnerId, s => s.OwnerId.ShouldNotBeEmpty())));
                                  };

        Because of = () => mockCommand.Original.Execute();

        It should_be_verify
                = () => store.Verify(r => r.AddProduct(Pleasure.MockIt.IsWeak<Product, AddProductCommand>(mockCommand.Original, 
                                                                                                          dsl => dsl.IgnoreBecauseRoot(product => product.Store)
                                                                                                                    .ForwardToValue(product => product.Image, new HttpMemoryPostedFile(mockCommand.Original.Image).ContentAsBytes))));
    }
}