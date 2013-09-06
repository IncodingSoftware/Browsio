namespace Browsio.Controllers
{
    #region << Using >>

    using System;
    using System.Web.Mvc;
    using Browsio.Domain;
    using Incoding.CQRS;
    using Incoding.MvcContrib;

    #endregion

    public class DispatcherController : IncControllerBase
    {
        #region Constructors

        public DispatcherController(IDispatcher dispatcher)
                : base(dispatcher) { }

        #endregion

        #region Http action

        [HttpPost]
        public ActionResult Delete(string id, string assemblyType)
        {
            return TryPush(new DeleteEntityByIdCommand(id, Type.GetType(assemblyType)));
        }

        [HttpGet]
        public  FileContentResult Img(GetImageQuery query)
        {            
            var img = this.dispatcher.Query(query);
            return File(img,"img");
        }

        #endregion
    }
}