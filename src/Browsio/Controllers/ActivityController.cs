using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Browsio.Controllers
{
    using System.Web.Http;
    using Browsio.Domain;
    using Incoding.CQRS;
    using Incoding.MvcContrib;

    public class ActivityController : IncControllerBase
    {
        //
        // GET: /Activity/

        public ActivityController(IDispatcher dispatcher)
            : base(dispatcher) { }

        [HttpGet]
        public ActionResult Index()
        {
            return IncPartialView("Index");
        }

        [HttpPost]
        public ActionResult FetchActivity(GetActivityQuery query)
        {
            var activity = this.dispatcher.Query(query);
            return IncJson(activity);
        }
    }
}
