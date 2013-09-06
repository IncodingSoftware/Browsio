namespace Browsio.Domain
{
    #region << Using >>

    using Incoding.Data;

    #endregion

    public class BrowsioEntityMap<TEntity> : NHibernateEntityMap<TEntity> where TEntity : BrowsioEntityBase
    {
        ////ncrunch: no coverage start
    
        #region Constructors

        protected BrowsioEntityMap()
        {
            Id(r => r.Id).CustomType<string>().GeneratedBy.Assigned();
        }

        #endregion

        ////ncrunch: no coverage end
    }
}