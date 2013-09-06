namespace Browsio.Domain
{
    #region << Using >>

    using System;
    using Incoding.Block.Logging;
    using Incoding.CQRS;
    using Incoding.Data;

    #endregion

    ////ncrunch: no coverage start
    public class DataBaseSetUp : ISetUp
    {
        #region Fields

        readonly IManagerDataBase managerDataBase;

        #endregion

        #region Constructors

        public DataBaseSetUp(IManagerDataBase managerDataBase)
        {
            this.managerDataBase = managerDataBase;
        }

        #endregion

        #region ISetUp Members

        public int GetOrder()
        {
            return 0;
        }

        public void Execute()
        {
            Exception ex;
            if (!this.managerDataBase.IsExist(out ex))
            {
                this.managerDataBase.Create();
                LoggingFactory.Instance.LogException(LogType.Debug, ex);
            }
        }

        #endregion

        #region Disposable

        public void Dispose() { }

        #endregion
    }

    ////ncrunch: no coverage end
}