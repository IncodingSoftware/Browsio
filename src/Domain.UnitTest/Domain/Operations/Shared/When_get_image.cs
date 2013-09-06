namespace Browsio.UnitTest.Domain
{
    #region << Using >>

    using Browsio.Domain;
    using Incoding.MSpecContrib;
    using Machine.Specifications;

    #endregion


    [Subject(typeof(GetImageQuery))]
    public class When_get_image 
    {
        #region Establish value

        protected static MockMessage<GetImageQuery, byte[]> mockQuery;

        protected static byte[] expected;

        #endregion


        public static void Establish(SearchItemOfType type)
        {
            var query = Pleasure.Generator.Invent<GetImageQuery>(dsl => dsl.Tuning(r => r.Type, type));
            expected = Pleasure.Generator.Bytes();

            mockQuery = MockQuery<GetImageQuery, byte[]>
                    .When(query);

        }


        It should_be_user = () =>
                                {
                                    Establish(SearchItemOfType.User);
                                    mockQuery
                                        .StubGetById(mockQuery.Original.Id, Pleasure.MockAsObject<User>(mock => mock.SetupGet(r => r.Image).Returns(expected)));

                                    mockQuery.Original.Execute();
                                    mockQuery.ShouldBeIsResult(expected);
                                };


        It should_be_product = () =>
                                {
                                    Establish(SearchItemOfType.Product);
                                    mockQuery
                                        .StubGetById(mockQuery.Original.Id, Pleasure.MockAsObject<Product>(mock => mock.SetupGet(r => r.Image).Returns(expected)));

                                    mockQuery.Original.Execute();
                                    mockQuery.ShouldBeIsResult(expected);
                                };

        It should_be_store = () =>
                                {
                                    Establish(SearchItemOfType.Store);
                                    mockQuery
                                        .StubGetById(mockQuery.Original.Id, Pleasure.MockAsObject<Store>(mock => mock.SetupGet(r => r.Image).Returns(expected)));

                                    mockQuery.Original.Execute();
                                    mockQuery.ShouldBeIsResult(expected);
                                };
        
    }
}