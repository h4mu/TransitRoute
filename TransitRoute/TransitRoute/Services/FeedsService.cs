using System;
using System.Collections.Generic;
using System.Net;
using System.Json;
using System.Threading.Tasks;
using TransitRoute.Models;
using System.IO;
using System.Linq;

namespace TransitRoute.Services
{
    public class FeedsService
    {
        private static async Task<T> MakeJsonRequest<T>(string url, Func<JsonValue, T> parse)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "GET";

            using (WebResponse response = await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            {
                return await Task.Run(() => JsonValue.Load(stream)).ContinueWith(val => parse(val.Result));
            }
        }

        public static async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            return await MakeJsonRequest("https://pkgstore.datahub.io/core/country-list:data_json/data/data_json.json",
                jsonDoc => jsonDoc.OfType<JsonObject>().Select(c => new Country { Name = c["Name"], Code = c["Code"] }));
        }

        public static async Task<IEnumerable<Operator>> GetFeedsAsync(Country country)
        {
            return await MakeJsonRequest("https://transit.land/api/v1/operators/?country=" + country.Code,
                jsonDoc => jsonDoc["operators"].OfType<JsonObject>().Select(f => new Operator
                {
                    Name = f["name"],
                    FeedIds = f["represented_in_feed_onestop_ids"].OfType<JsonValue>().Select(id => id.ToString()).ToList()
                }));
        }

        public static async Task<Feed> GetFeedsAsync(Operator op)
        {
            return await MakeJsonRequest("https://transit.land/api/v1/feeds/" + op.FeedIds.First(),
                f => new Feed
                {
                    Name = f["name"],
                    Uri = new Uri(f["url"]),
                    LicenseUri = new Uri(f["license_url"]),
                    LicenseText = f["license_attribution_text"],
                    LicenseAttributionRequired = bool.TryParse(f["license_use_without_attribution"], out bool licenseAttributionRequired) ? licenseAttributionRequired : false
                });
        }
    }
}
