namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using Browsio.Domain;
    using FluentNHibernate.Testing;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(Store))]
    public class When_save_Store : SpecWithPersistenceSpecification<Store>
    {
        Because of = () => persistenceSpecification
                                   .CheckProperty(r => r.Name, Pleasure.Generator.String())                                   
                                   .CheckProperty(r => r.Description, Pleasure.Generator.String(length: 1000))
                                   .CheckProperty(r => r.Category, Pleasure.Generator.Enum<CategoryOfType>())
                                   .CheckProperty(r => r.Image, Pleasure.Generator.Bytes(size: 300))
                                   .CheckList(r => r.Products, Pleasure.ToEnumerable(Pleasure.Generator.InventEntity<Product>()), (store, product) => store.AddProduct(product))
                                   .CheckList(r => r.Followers, Pleasure.ToEnumerable(Pleasure.Generator.InventEntity<User>()), (store, user) => store.AddFollower(user))
                                   .CheckList(r => r.Views, Pleasure.ToEnumerable(Pleasure.Generator.InventEntity<Views>()), (store, view) => store.AddView(view))
                                   .CheckReference(r => r.User, Pleasure.Generator.InventEntity<User>())
                                   .CheckReference(r => r.Genre, Pleasure.Generator.InventEntity<Genre>());

        It should_be_verify
                = () => persistenceSpecification.VerifyMappingAndSchema(setting => setting.IgnoreBecauseCalculate(r => r.CategoryAsClass)
                                                                                          .IgnoreBecauseCalculate(r => r.CategoryAsAmazon));
    }
}