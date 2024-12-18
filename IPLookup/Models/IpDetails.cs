using System.Text.Json.Serialization;

namespace IPLookup.Models
{
    public class IpDetails
    {
        [JsonPropertyName("ip")]
        public string Ip { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("continent_code")]
        public string ContinentCode { get; set; }

        [JsonPropertyName("continent_name")]
        public string ContinentName { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("region_code")]
        public string RegionCode { get; set; }

        [JsonPropertyName("region_name")]
        public string RegionName { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("zip")]
        public string Zip { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("msa")]
        public string Msa { get; set; }

        [JsonPropertyName("dma")]
        public string Dma { get; set; }

        [JsonPropertyName("radius")]
        public object Radius { get; set; }

        [JsonPropertyName("ip_routing_type")]
        public object IpRoutingType { get; set; }

        [JsonPropertyName("connection_type")]
        public object ConnectionType { get; set; }

        [JsonPropertyName("location")]
        public IpLocation Location { get; set; }

        [JsonPropertyName("time_zone")]
        public IpTimeZone TimeZone { get; set; }

        [JsonPropertyName("currency")]
        public IpCurrency Currency { get; set; }

        [JsonPropertyName("connection")]
        public IpConnection Connection { get; set; }
    }

    public class IpLocation
    {
        [JsonPropertyName("geoname_id")]
        public int? GeonameId { get; set; }

        [JsonPropertyName("capital")]
        public string Capital { get; set; }

        [JsonPropertyName("languages")]
        public List<IpLanguage> Languages { get; set; }

        [JsonPropertyName("country_flag")]
        public string CountryFlag { get; set; }

        [JsonPropertyName("country_flag_emoji")]
        public string CountryFlagEmoji { get; set; }

        [JsonPropertyName("country_flag_emoji_unicode")]
        public string CountryFlagEmojiUnicode { get; set; }

        [JsonPropertyName("calling_code")]
        public string CallingCode { get; set; }

        [JsonPropertyName("is_eu")]
        public bool? IsEu { get; set; }
    }

    public class IpLanguage
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("native")]
        public string Native { get; set; }
    }

    public class IpTimeZone
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("current_time")]
        public string CurrentTime { get; set; }

        [JsonPropertyName("gmt_offset")]
        public int? GmtOffset { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("is_daylight_saving")]
        public bool? IsDaylightSaving { get; set; }
    }

    public class IpCurrency
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("plural")]
        public string Plural { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("symbol_native")]
        public string SymbolNative { get; set; }
    }

    public class IpConnection
    {
        [JsonPropertyName("asn")]
        public int? Asn { get; set; }

        [JsonPropertyName("isp")]
        public string Isp { get; set; }

        [JsonPropertyName("sld")]
        public string Sld { get; set; }

        [JsonPropertyName("tld")]
        public string Tld { get; set; }

        [JsonPropertyName("carrier")]
        public string Carrier { get; set; }

        [JsonPropertyName("home")]
        public object Home { get; set; }

        [JsonPropertyName("organization_type")]
        public object OrganizationType { get; set; }

        [JsonPropertyName("isic_code")]
        public object IsicCode { get; set; }

        [JsonPropertyName("naics_code")]
        public object NaicsCode { get; set; }
    }

}
