namespace Browsio.Controllers
{
    #region << Using >>

    using System.Web.Mvc;
    using Incoding.CQRS;
    using Incoding.MvcContrib;

    #endregion

    public class BrowsioController : IncControllerBase
    {

        ////ncrunch: no coverage start
        
        #region Constructors

        public BrowsioController(IDispatcher dispatcher)
                : base(dispatcher) { }

        #endregion

        #region Api Methods

        public ActionResult Index()
        {
            return View();
        }

        #endregion

        ////ncrunch: no coverage end
    }
}