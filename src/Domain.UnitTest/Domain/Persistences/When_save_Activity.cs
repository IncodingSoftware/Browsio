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

    [Subject(typeof(Activity))]
    public class When_save_Activity : SpecWithPersistenceSpecification<Activity>
    {

        Because of = () => persistenceSpecification
                                   .CheckProperty(r => r.Title, Pleasure.Generator.String())
                                   .CheckProperty(r => r.Type, Pleasure.Generator.Enum<ActivityOfType>())
                                   .CheckProperty(r => r.Description, Pleasure.Generator.String())
                                   .CheckProperty(r => r.Date, Pleasure.Generator.DateTime())
                                   .CheckProperty(r => r.ObjectActivity, Pleasure.Generator.String())
                                   .CheckReference(r => r.ActivityToUser, Pleasure.Generator.Invent<User>())
                                   .CheckReference(r => r.ActivityFromUser, Pleasure.Generator.Invent<User>());


        It should_be_verify = () => persistenceSpecification.VerifyMappingAndSchema();

    }
}
