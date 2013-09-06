namespace Browsio.Domain
{
    #region << Using >>

    using System.Linq;
    using Incoding.CQRS;

    #endregion

    public class DeleteProductByAsinCommand : CommandBase
    {
        #region Properties

        public string Asin { get; set; }

        #endregion

        public override void Execute()
        {
            var product = Repository.Query(whereSpecification: new ProductByASINWhereSpec(Asin))
                                    .First();

            Repository.Delete(product);
        }
    }
}