namespace Browsio.Controllers
{
    #region << Using >>

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Browsio.Domain;
    using Browsio.Domain.Search.Query;
    using Incoding.Block.IoC;
    using Incoding.CQRS;
    using Incoding.MvcContrib;
    using FluentNHibernate.Conventions;

    #endregion

    public class StoreController : IncControllerBase
    {
        #region Constructors

        public StoreController(IDispatcher dispatcher)
            : base(dispatcher) { }

        #endregion

        #region Http action

        [HttpGet]
        public ActionResult Add()
        {
            return IncView(new AddStoreCommand());
        }

        [HttpPost]
        public ActionResult Add(AddStoreCommand input)
        {
            return TryPush(input);
        }

        [HttpGet]
        public ActionResult Detail(GetStoreDetailQuery query)
        {
            var vms = this.dispatcher.Query(query);
            return IncJson(vms);
        }

        [HttpGet]
        public ActionResult Edit(GetStoreByIdQuery query)
        {
            var store = this.dispatcher.Query(query);
            return IncView(new EditStoreCommand(store));
        }

        [HttpPost]
        public ActionResult Edit(EditStoreCommand input)
        {
            return TryPush(input);
        }

        [HttpGet]
        public ActionResult Fetch(GetStoresByUserQuery query)
        {
            var vms = this.dispatcher.Query(query)
                          .Select(r => new StoreVm(r))
                          .ToList();
            return IncJson(vms);
        }

        [HttpGet]
        public ActionResult FetchByTop(GetStoresByTopQuery query)
        {
            var vms = this.dispatcher.Query(query)
                          .Select((r, i) => new StoreCarouselVm(r, i == 0))
                          .ToList();

            return IncJson(vms);
        }

        [HttpGet]
        public ActionResult Search(SearchStoreQuery query, string searchIds, string friends)
        {
            List<string> storeIds = null;
            if (!string.IsNullOrWhiteSpace(friends))
            {
                storeIds = dispatcher.Query(new GetStoreIdsByFriends()).ToList();
            }

            if (!string.IsNullOrWhiteSpace(searchIds))
            {
                List<string> storeIdsFriend = null;
                if (storeIds != null)
                {
                    storeIdsFriend = storeIds;
                }
                storeIds = dispatcher.Query(new GetStoreIdsBySearchIdsQuery()
                                                      {
                                                              SearchIds = searchIds.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                                                      }).ToList();
                if (storeIdsFriend != null)
                {
                    storeIds = storeIds.Intersect(storeIdsFriend).ToList();
                }
            }
            if (storeIds != null)
            {
                query.StoreIds = storeIds.ToArray();
            }
            var vms = this.dispatcher.Query(query);
            return IncJson(vms);
        }

        [HttpPost]
        public ActionResult Watches(CountWatchesQuery query)
        {
            return IncJson(this.dispatcher.Query(query));
        }

        [BrowsioAuthorize]
        [HttpPost]
        public ActionResult FollowThisStore(AddFollowCommand command)
        {
            return TryPush(command);
        }

        [BrowsioAuthorize]
        [HttpPost]
        public ActionResult UnFollowThisStore(DeleteFollowCommand command)
        {
            return TryPush(command);
        }

        [HttpPost]
        public ActionResult LikesThisStore(CountLikesQuery query)
        {
            return IncJson(this.dispatcher.Query(query));
        }

        #endregion
    }
}