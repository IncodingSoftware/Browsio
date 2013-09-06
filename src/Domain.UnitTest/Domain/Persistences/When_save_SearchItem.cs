namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using Browsio.Domain;
    using FluentNHibernate.Testing;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(SearchItem))]
    public class When_save_SearchItem : SpecWithPersistenceSpecification<SearchItem>
    {
        Because of = () => persistenceSpecification
                                   .CheckProperty(r => r.OwnerId, Pleasure.Generator.String())
                                   .CheckProperty(r => r.Query, Pleasure.Generator.String())
                                   .CheckProperty(r => r.Type, Pleasure.Generator.Enum<SearchItemOfType>());

        It should_be_verify = () => persistenceSpecification.VerifyMappingAndSchema();
    }
}