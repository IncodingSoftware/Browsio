namespace Browsio.Domain
{
    using System.Linq;
    using Incoding.CQRS;

    public class GetStoreByIdQuery : QueryBase<Store>
    {
        #region Properties

        public string Id { get; set; }

        #endregion

        protected override Store ExecuteResult()
        {
            return this.Repository.Query(whereSpecification: new EntityByIdSpec<Store>(this.Id), 
                                    fetchSpecification: new StoreWithGenreFetchSpec())
                             .First();
        }
    }
}