namespace Browsio.Controllers
{
    #region << Using >>

    using System.Web.Mvc;
    using Incoding.CQRS;
    using Incoding.MvcContrib;

    #endregion

    public class TemplateController : IncControllerBase
    {
        #region Constructors

        public TemplateController(IDispatcher dispatcher)
                : base(dispatcher) { }

        #endregion

        #region Http action

        [HttpGet]
        public ActionResult Get(string name)
        {
            return IncPartialView(name);
        }

        #endregion
    }
}