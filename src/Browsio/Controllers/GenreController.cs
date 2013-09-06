namespace Browsio.Controllers
{
    #region << Using >>

    using System.Linq;
    using System.Web.Mvc;
    using Browsio.Domain;
    using Incoding.CQRS;
    using Incoding.MvcContrib;

    #endregion

    public class GenreController : IncControllerBase
    {
        #region Constructors

        public GenreController(IDispatcher dispatcher)
                : base(dispatcher) { }

        #endregion

        #region Http action

        [HttpGet]
        public ActionResult Fetch(GetGenresByCategoryQuery query)
        {
            var vms = this.dispatcher.Query(query)
                          .Select(r => new KeyValueVm(r.Id, r.Name))
                          .ToList();

            return IncJson(new OptGroupVm(vms));
        }

        #endregion
    }
}