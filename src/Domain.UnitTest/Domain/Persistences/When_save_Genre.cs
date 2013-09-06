namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using Browsio.Domain;
    using FluentNHibernate.Testing;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(Genre))]
    public class When_save_Genre : SpecWithPersistenceSpecification<Genre>
    {
        Because of = () => persistenceSpecification
                                   .CheckProperty(r => r.Name, Pleasure.Generator.String())
                                   .CheckProperty(r => r.Category, Pleasure.Generator.Enum<CategoryOfType>());

        It should_be_verify = () => persistenceSpecification.VerifyMappingAndSchema();
    }
}