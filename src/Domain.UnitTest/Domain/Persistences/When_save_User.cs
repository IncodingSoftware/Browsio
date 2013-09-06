namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using Browsio.Domain;
    using FluentNHibernate.Testing;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion

    [Subject(typeof(User))]
    public class When_save_User : SpecWithPersistenceSpecification<User>
    {
        Because of = () => persistenceSpecification
                                   .CheckProperty(r => r.Login, Pleasure.Generator.String())
                                   .CheckProperty(r => r.FbId, Pleasure.Generator.String())
                                   .CheckProperty(r => r.Password, Pleasure.Generator.String())
                                   .CheckProperty(r => r.FirstName, Pleasure.Generator.String())
                                   .CheckProperty(r => r.LastName, Pleasure.Generator.String())
                                   .CheckProperty(r => r.Sex, Pleasure.Generator.Enum<SexOfType>())
                                   .CheckProperty(r => r.Image, Pleasure.Generator.Bytes())
                                   .CheckProperty(r => r.Activated, Pleasure.Generator.Bool())
                                   .CheckProperty(r => r.ResetToken, Pleasure.Generator.String())
                                   .CheckProperty(r => r.AccessToken, Pleasure.Generator.String())
                                   .CheckList(r => r.WishProducts, Pleasure.ToEnumerable(Pleasure.Generator.InventEntity<Product>()), (user, product) => user.AddWishProduct(product))
                                   .CheckList(r => r.Stores, Pleasure.ToList(Pleasure.Generator.InventEntity<Store>()), (user, store) => user.AddStore(store))
                                   .CheckList(r => r.Friends, Pleasure.ToList(Pleasure.Generator.InventEntity<User>()), (user, friend) => user.AddFriend(friend));

        It should_be_verify = () => persistenceSpecification.VerifyMappingAndSchema(setting =>
                                                                                        {
                                                                                            setting.IgnoreBecauseCalculate(r => r.FullName);
                                                                                            setting.IgnoreBecauseCalculate(r => r.Followers);
                                                                                        });
    }
}