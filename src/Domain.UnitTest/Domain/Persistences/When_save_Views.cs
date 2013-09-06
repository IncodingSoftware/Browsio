using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browsio.UnitTest.Domain
{
    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;
    using FluentNHibernate.Testing;

    [Subject(typeof(Views))]
    public class When_save_Views : SpecWithPersistenceSpecification<Views>
    {
        Because of = () => persistenceSpecification
                                   .CheckProperty(r => r.IPVisitors, Pleasure.Generator.String())
                                   .CheckProperty(r => r.Date, Pleasure.Generator.DateTime())
                                   .CheckReference(r => r.Store, Pleasure.Generator.InventEntity<Store>())
                                   .CheckReference(r => r.User, Pleasure.Generator.InventEntity<User>());

        It should_be_verify = () => persistenceSpecification.VerifyMappingAndSchema();

    }
}
