using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Browsio.Controllers
{
    using Browsio.Domain;
    using Incoding.CQRS;
    using Incoding.MvcContrib;

    public class NotificationController : IncControllerBase
    {
        //
        // GET: /Notification/

        public NotificationController(IDispatcher dispatcher)
                : base(dispatcher) { }

        public ActionResult Landing(NotificationVm notificationVm)
        {
            return View(notificationVm);
        }

        public ActionResult LandingAjax(NotificationVm notificationVm)
        {
            return IncPartialView("LandingPartial", notificationVm);
        }

    }
}
