namespace Browsio.Controllers
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Browsio.Domain;
    using Incoding.CQRS;
    using Incoding.Extensions;
    using Incoding.MvcContrib;

    #endregion

    public class SearchItemController : IncControllerBase
    {
        #region Constructors

        public SearchItemController(IDispatcher dispatcher)
            : base(dispatcher) { }

        #endregion

        #region Http action

        [HttpGet]
        public ActionResult Fetch(GetSearchItemsQuery query)
        {
            var vms = this.dispatcher.Query(query)
                           .GroupBy(r => r.Type, r => r);

            var res = new List<SearchItemVm>();
            foreach (var groupByType in vms)
            {
                res.Add(new SearchItemVm(groupByType.Key.ToLocalization(), groupByType.Key ,string.Join(",",groupByType.ToList().Select(r=>r.Id.ToString()).ToList())));
                res.AddRange(groupByType.Select(r => new SearchItemVm(r)));
            }

            return IncJson(res);
        }
        #endregion
    }
}