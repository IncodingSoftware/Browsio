using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browsio.Domain
{
    using Incoding.Data;
    using Incoding.EventBroker;

    public class ActivitySubscriber : IEventSubscriber<OnActivity>
    {
        readonly IRepository Repository;

        public ActivitySubscriber(IRepository repository)
        {
            Repository = repository;
        }

        public void Subscribe(OnActivity @event)
        {;
            this.Repository.Save(new Activity
                                     {
                                         Title = @event.Title,
                                         Description = @event.Description,
                                         Type = @event.Type,
                                         Date = DateTime.Now,
                                         ObjectActivity = @event.ObjectActivity,
                                         ActivityToUser = @event.ActivityToUser,
                                         ActivityFromUser = @event.ActivityFromUser
                                     });
        }

        ////ncrunch: no coverage start

        public void Dispose() {  }

        ////ncrunch: no coverage end  
    }
}
