namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using Browsio.Domain;
    using FluentNHibernate.Testing;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(Product))]
    public class When_save_Product : SpecWithPersistenceSpecification<Product>
    {
        Because of = () => persistenceSpecification
                                   .CheckProperty(r => r.Name, Pleasure.Generator.String())
                                   .CheckProperty(r => r.Asin, Pleasure.Generator.String())
                                   .CheckProperty(r => r.Author, Pleasure.Generator.String())
                                   .CheckProperty(r => r.Description, Pleasure.Generator.String(length: 1000))
                                   .CheckProperty(r => r.Image, Pleasure.Generator.Bytes())
                                   .CheckReference(r => r.Store, Pleasure.Generator.InventEntity<Store>())
                                   .CheckProperty(r => r.Price, Pleasure.Generator.PositiveFloating());

        It should_be_verify = () => persistenceSpecification.VerifyMappingAndSchema();
    }
}