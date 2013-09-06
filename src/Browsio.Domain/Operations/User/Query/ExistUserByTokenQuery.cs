namespace Browsio.Domain
{
    #region << Using >>

    using System.Linq;
    using Incoding.CQRS;

    #endregion

    public class ExistUserByTokenQuery : QueryBase<IncStructureResponse<bool>>
    {
        #region Properties

        public string ResetToken { get; set; }

        #endregion

        protected override IncStructureResponse<bool> ExecuteResult()
        {
            bool has = Repository.Query(whereSpecification: new UserByResetTokenWhereSpec(ResetToken))
                                 .Any();
            return new IncStructureResponse<bool>(has);
        }
    }
}