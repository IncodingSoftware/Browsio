namespace Browsio.Controllers
{
    #region << Using >>

    using System.Web.Mvc;
    using Browsio.Contrib;
    using Browsio.Domain;
    using Incoding.CQRS;
    using Incoding.MvcContrib;

    #endregion

    public class ProfileController : IncControllerBase
    {
        #region Constructors

        public ProfileController(IDispatcher dispatcher)
            : base(dispatcher) { }

        #endregion

        #region Http action

        [HttpGet]
        public ActionResult Detail(GetUserDetailQuery query)
        {
            var vm = this.dispatcher.Query(query);
            this.dispatcher.Push(new AddViewsCommand(query));
            return IncView(vm);
        }

        [HttpGet, BrowsioAuthorize]
        public ActionResult Edit()
        {
            var user = this.dispatcher.Query(new GetEntityByIdQuery<User>(BrowsioApp.UserId));
            return IncView(new EditUserCommand(user));
        }

        [HttpPost, BrowsioAuthorize]
        public ActionResult Edit(EditUserCommand input)
        {
            return TryPush(input);
        }

        [HttpPost]
        public ActionResult Followers(GetFollowersQuery query)
        {
            return IncJson(this.dispatcher.Query(query));
        }

        [HttpGet]
        public ActionResult UserLogin(GetUserLoginQuery query)
        {
            return IncJson(this.dispatcher.Query(query));
        }

        #endregion

        [HttpGet]
        public ActionResult IsSocial(IsUserSocialQuery query)
        {
            return IncJson(this.dispatcher.Query(query));
        }

        public ActionResult Index()
        {
            return IncView();
        }
    }
}