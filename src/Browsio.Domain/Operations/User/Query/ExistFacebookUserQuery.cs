namespace Browsio.Domain
{
    #region << Using >>

    using System.Linq;
    using Incoding.CQRS;

    #endregion

    public class ExistFacebookUserQuery : QueryBase<IncStructureResponse<bool>>
    {
        #region Properties

        public string FBId { get; set; }

        #endregion

        protected override IncStructureResponse<bool> ExecuteResult()
        {
            bool isAny = Repository.Query(whereSpecification: new UserByFBIdWhereSpec(FBId))
                                   .Any();

            return new IncStructureResponse<bool>(isAny);
        }
    }
}