using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FacebookApi.Client
{
    public partial class PlacesResponse
    {
        [JsonProperty("data")]
        public List<Place> Venues { get; set; }
    }

    public partial class Place
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("about")]
        public string About { get; set; }

        [JsonProperty("category_list")]
        public List<CategoryList> CategoryList { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("hours", NullValueHandling = NullValueHandling.Ignore)]
        public List<Hour> Hours { get; set; }

        [JsonProperty("overall_star_rating", NullValueHandling = NullValueHandling.Ignore)]
        public double? OverallStarRating { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("single_line_address")]
        public string SingleLineAddress { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public partial class CategoryList
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Hour
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }
    }

    public partial class PlacesResponse
    {
        public static PlacesResponse FromJson(string json) => JsonConvert.DeserializeObject<PlacesResponse>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this PlacesResponse self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
