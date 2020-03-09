using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ShopifyWatcher
{
    public class SiteMap
    {
       // public UrlSet UrlSet { get; set; }
        public List<Url> Urls { get; set; }
    }
    public class UrlSet
    {
        [XmlElement(ElementName = "url")]
        public List<Url> Urls { get; set; } = new List<Url>();
    }
    public class Url
    {
        [XmlElement(ElementName = "loc")]
        public string Loc { get; set; }
        [XmlElement(ElementName = "changefreq")]
        public string ChangeFreq { get; set; }
        [XmlElement(ElementName = "lastmod")]
        public DateTime LastMod { get; set; }
    }
}
