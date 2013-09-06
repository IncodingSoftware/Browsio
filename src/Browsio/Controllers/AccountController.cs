namespace Browsio.Controllers
{
    #region << Using >>

    using System;
    using System.Configuration;
    using System.Web.Mvc;
    using Browsio.Domain;
    using Incoding.Block.IoC;
    using Incoding.CQRS;
    using Incoding.Extensions;
    using Incoding.MvcContrib;

    #endregion

    public class AccountController : IncControllerBase
    {
        #region Constructors

        public AccountController(IDispatcher dispatcher)
            : base(dispatcher) { }

        #endregion

        #region Http action


        [HttpGet]
        public ActionResult ChangePassword(ExistUserByTokenQuery query)
        {
            if (this.dispatcher.Query(query).Value)
                return View(new ChangeUserPasswordCommand(query.ResetToken));

            return RedirectToAction("Landing", "Notification", new { Title = "Error. Link is not valid!", Type = "error", Message = "Link is not valid!" });
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangeUserPasswordCommand command)
        {
            if (ModelState.IsValid)
            {
                this.dispatcher.Push(command);
                return RedirectToAction("ChangePasswordByEmailSuccess", "Account");
            }
            return View(command);
        }

        [HttpGet]
        public ActionResult Confirm(ConfirUserCommand input)
        {
            return TryPush(input, setting => { setting.SuccessResult = () => RedirectToAction("ConfirmSuccess", "Account"); });
        }

        [HttpGet]
        public ActionResult ConfirmSuccess()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return IncView();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotUserPasswordCommand command)
        {
            command.Token = Guid.NewGuid().ToString().Replace("-", string.Empty);
            command.ChangePasswordUrl = Url.Action("ChangePassword", "Account", AnonymousHelper.ToDictionary(new { ResetToken = command.Token }), "http", Request.Url.DnsSafeHost);
            return TryPush(command, setting => setting.SuccessResult = () => RedirectToAction("LandingAjax", "Notification", new { type = "success", message = "Your new password sent to you by e-mail" }));
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return IncPartialView("SignIn");
        }

        [HttpPost]
        public ActionResult SignIn(GetUserByCredentialQuery query)
        {
            var user = this.dispatcher.Query(query);
            if (user == null)
            {
                ModelState.AddModelError("Server", "Invalid login or password");
                return IncodingResult.Error(ModelState);
            }

            if (!user.Activated)
            {
                ModelState.AddModelError("Server", "Your account not confirmed");
                return IncodingResult.Error(ModelState);
            }

            IoCFactory.Instance.TryResolve<IFormAuthentication>().SignIn(user.Id.ToString(), true);
            return IncRedirectToAction("Index", "Browsio");
        }

        [HttpGet]
        public ActionResult SignOut()
        {
            IoCFactory.Instance.TryResolve<IFormAuthentication>().SignOut();
            return IncRedirectToAction("index", "Browsio");
        }

        [HttpPost]
        public ActionResult SignUp(AddUserCommand input)
        {
            //IoCFactory.Instance.TryResolve<IFullUrlBuilder>().FromUrl(Url.Action("Confirm", "Account"));
            string pathResult = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority + Url.Action("Confirm", "Account");
            input.PathResult = pathResult;
            return TryPush(input, setting => { setting.SuccessResult = () => RedirectToAction("LandingAjax", "Notification", new { type = "success", message = "Your account sign up. Please verify email." }); });
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return IncPartialView("SignUp");
        }

        #endregion

        #region Api Methods

        public ActionResult ChangePasswordByEmailSuccess()
        {
            return View();
        }

        #endregion
    }
}