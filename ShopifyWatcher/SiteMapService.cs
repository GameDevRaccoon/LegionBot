using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace ShopifyWatcher
{
    public interface ISiteMapService
    {
        UrlSet GetSiteMap(string siteXML);
    }
    public class SiteMapService : ISiteMapService
    {
        public UrlSet GetSiteMap(string siteXML)
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "urlset";
            xRoot.Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9";
            xRoot.IsNullable = true;
            XmlSerializer ser = new XmlSerializer(typeof(UrlSet),xRoot);
            using (System.IO.StreamReader r = new System.IO.StreamReader(siteXML))
            {
                return ((UrlSet)ser.Deserialize(r));
            }
        }
    }
}
