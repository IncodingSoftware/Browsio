namespace Browsio.Controllers
{
    #region << Using >>

    using System.Web.Mvc;
    using Incoding.CQRS;
    using Incoding.MvcContrib;

    #endregion

    public class BrowseController : IncControllerBase
    {
        #region Constructors

        public BrowseController(IDispatcher dispatcher)
                : base(dispatcher) { }

        #endregion

        #region Http action

        [HttpGet]
        public ActionResult Index()
        {
            return IncView();
        }

        #endregion
    }
}