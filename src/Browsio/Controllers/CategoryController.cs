namespace Browsio.Controllers
{
    #region << Using >>

    using System.Linq;
    using System.Web.Mvc;
    using Browsio.Domain;
    using Incoding.CQRS;
    using Incoding.MvcContrib;

    #endregion

    public class CategoryController : IncControllerBase
    {
        #region Constructors

        public CategoryController(IDispatcher dispatcher)
                : base(dispatcher) { }

        #endregion

        #region Http action

        [HttpGet]
        public ActionResult Fetch()
        {
            var categories = typeof(CategoryOfType)
                    .ToSelectList()
                    .Select(item => new KeyValueVm(item.Value, item.Text))
                    .ToList();
            return IncJson(new OptGroupVm(categories));
        }

        #endregion
    }
}