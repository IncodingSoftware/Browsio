using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browsio.Domain
{
    using System.Web.Mvc;
    using Incoding.CQRS;

    public class GetActivityQuery : QueryBase<List<ActivityVm>>
    {
        protected override List<ActivityVm> ExecuteResult()
        {
            return this.Repository.Query(whereSpecification: new ActivityByUserOwner(BrowsioApp.UserId)).OrderByDescending(activity => activity.Date).ToList()
                .Take(10).OrderBy(activity => activity.Date)
                .Select(activity =>
                                new ActivityVm
                                    {
                                        Title = activity.Title,
                                        TypePicture = GetPicture(activity.Type),
                                        Date = activity.Date.TimeSpanToString(),
                                        Description = activity.Description,
                                        ObjectActivity = activity.ObjectActivity,
                                        ActivityFromUserId = activity.ActivityFromUser.Id.ToString()
                                    }).ToList();
        }
        string GetPicture(ActivityOfType type)
        {
            switch (type)
            {
                case ActivityOfType.Browsio:
                    {
                        return "icon-browsio.png";
                    }
                default:
                    {
                        return "";
                    }
            }
        }
    }
}
