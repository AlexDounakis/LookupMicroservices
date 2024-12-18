namespace IpDetailsCache.Models
{
    public class IpDetails
    {
        public string Ip { get; set; }
        public string Type { get; set; }
        public string ContinentCode { get; set; }
        public string ContinentName { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Msa { get; set; }
        public string Dma { get; set; }
        public object Radius { get; set; }
        public object IpRoutingType { get; set; }
        public object ConnectionType { get; set; }
        public IpLocation Location { get; set; }
        public IpTimeZone TimeZone { get; set; }
        public IpCurrency Currency { get; set; }
        public IpConnection Connection { get; set; }
    }

    public class IpLocation
    {
        public int? GeonameId { get; set; }
        public string Capital { get; set; }
        public List<IpLanguage> Languages { get; set; }
        public string CountryFlag { get; set; }
        public string CountryFlagEmoji { get; set; }
        public string CountryFlagEmojiUnicode { get; set; }
        public string CallingCode { get; set; }
        public bool? IsEu { get; set; }
    }

    public class IpLanguage
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Native { get; set; }
    }

    public class IpTimeZone
    {
        public string Id { get; set; }
        public string CurrentTime { get; set; }
        public int? GmtOffset { get; set; }
        public string Code { get; set; }
        public bool? IsDaylightSaving { get; set; }
    }

    public class IpCurrency
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Plural { get; set; }
        public string Symbol { get; set; }
        public string SymbolNative { get; set; }
    }

    public class IpConnection
    {
        public int? Asn { get; set; }
        public string Isp { get; set; }
        public string Sld { get; set; }
        public string Tld { get; set; }
        public string Carrier { get; set; }
        public object Home { get; set; }
        public object OrganizationType { get; set; }
        public object IsicCode { get; set; }
        public object NaicsCode { get; set; }
    }
}

