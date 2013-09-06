namespace Browsio.Amazon
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using Incoding.Maybe;

    #endregion

    public class AmazonService : IAmazonService
    {
        #region Fields

        readonly SignedRequestHelper request;

        #endregion

        #region Constructors

        public AmazonService()
        {
            this.request = new SignedRequestHelper("AKIAJOCFAK3U4XCIS7LA", "r07uN2KcJ243NgunCOpDTQzo4Zpf4wN2XAu8RzHl", "ecs.amazonaws.com");
        }

        #endregion

        #region IAmazonService Members

        public List<AmazonItem> Fetch(string title, int page, string category)
        {
            IDictionary<string, string> query = new Dictionary<string, string>();
            query["Service"] = "AWSECommerceService";
            query["Operation"] = "ItemSearch";
            query["AssociateTag"] = "browseio-20";
            query["ItemPage"] = page.ToString();
            query["ResponseGroup"] = "Large";
            query["Title"] = title;
            query["SearchIndex"] = category;

            var items = (this.request.Execute(query).DocumentElement.GetElementsByTagName("Items")[0] as XmlElement)
                    .GetElementsByTagName("Item");

            var res = new List<AmazonItem>();
            foreach (XmlElement item in items)
            {
                var attributes = item.GetElementsByTagName("ItemAttributes")[0] as XmlElement;

                res.Add(new AmazonItem
                            {
                                    ASIN = item.GetElementsByTagName("ASIN")[0].ReturnOrDefault(r => r.InnerText, string.Empty), 
                                    Title = attributes.GetElementsByTagName("Title")[0].ReturnOrDefault(r => r.InnerText, string.Empty), 
                                    Description = attributes.GetElementsByTagName("Feature")
                                                            .Cast<XmlElement>()
                                                            .Aggregate(string.Empty, (s, element) => s += element.InnerText + "."), 
                                    Price = attributes.GetElementsByTagName("ListPrice")[0]
                                            .With(r => r as XmlElement)
                                            .ReturnOrDefault(r => double.Parse(r.GetElementsByTagName("FormattedPrice")[0].InnerText.Replace("$", string.Empty)), 0), 
                                    Image = item.GetElementsByTagName("MediumImage")[0]
                                            .With(r => r as XmlElement)
                                            .With(r => r.GetElementsByTagName("URL")[0])
                                            .ReturnOrDefault(r => r.InnerText, string.Empty), 
                                    Author = attributes.GetElementsByTagName("Actor")
                                                       .Cast<XmlElement>()
                                                       .Aggregate(string.Empty, (s, element) => s += element.InnerText + ",")
                                                       .Not(string.IsNullOrWhiteSpace)
                                                       .ReturnOrDefault(r => r, attributes.GetElementsByTagName("Brand")[0].ReturnOrDefault(r => r.InnerText, string.Empty))
                            });
            }

            return res;
        }

        #endregion
    }
}