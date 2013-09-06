using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browsio.Domain
{
    using System.Web;
    using Incoding.Block.IoC;
    using Incoding.CQRS;

    public class AddViewsCommand : CommandBase
    {
        public string UserId { get; set; }

        public string StoreId { get; set; }

        public AddViewsCommand() : base() { }

        public AddViewsCommand(GetUserDetailQuery query)
            : base()
        {
            this.StoreId = query.SelectedStoreId;
            this.UserId = query.Id;
        }

        public override void Execute()
        {
            var IpUser = HttpContext.Current.Request.UserHostName;
            var store = this.Repository.GetById<Store>(this.StoreId);
            if (store.Views.All(r => r.IPVisitors != IpUser))
            {
                Views views = new Views();
                views.IPVisitors = IpUser;
                views.Date = DateTime.Now;
                views.Store = store;
                if (!string.IsNullOrWhiteSpace(UserId))
                    views.User = this.Repository.GetById<User>(UserId);
                this.Repository.Save(views);
            }
        }
    }
}