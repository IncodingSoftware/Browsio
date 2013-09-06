using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Browsio.Domain;

namespace Browsio.UnitTest.Domain.Infrastructure
{
    using Browsio.Domain;
    using Machine.Specifications;
    using Moq;
    using It = Machine.Specifications.It;

    [Subject(typeof(DateExtension))]
    public class When_TimeSpanToString_DateExtension
    {
        It should_be_months = () => DateTime.Now.AddMonths(-2)
                                           .TimeSpanToString()
                                           .ShouldEqual("2 months ago");

        It should_be_month = () => DateTime.Now.AddMonths(-1)
                                       .TimeSpanToString()
                                       .ShouldEqual("one month ago");  
        
        It should_be_days = () => DateTime.Now.AddDays(-21)
                                       .TimeSpanToString()
                                       .ShouldEqual("21 days ago");
        
        It should_be_day = () => DateTime.Now.AddDays(-1)
                                       .TimeSpanToString()
                                       .ShouldEqual("one day ago");        

        It should_be_hours = () => DateTime.Now.AddHours(-11)
                                       .TimeSpanToString()
                                       .ShouldEqual("11 hours ago");
        
        
        It should_be_hour = () => DateTime.Now.AddHours(-1)
                                       .TimeSpanToString()
                                       .ShouldEqual("one hour ago");

        It should_be_minutes = () => DateTime.Now.AddMinutes(-20)
                                       .TimeSpanToString()
                                       .ShouldEqual("20 minutes ago");

        It should_be_minute = () => DateTime.Now.AddSeconds(-70)
                               .TimeSpanToString()
                               .ShouldEqual("one minute ago");

        It should_be_just_now = () => DateTime.Now.AddSeconds(-15)
                       .TimeSpanToString()
                       .ShouldEqual("just now");
    }
}