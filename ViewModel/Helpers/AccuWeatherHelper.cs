using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.ViewModel.Helpers
{
    public class AccuWeatherHelper
    {
        //Base url for API endpoint
        public const string BASE_URL = "http://dataservice.accuweather.com/";
        //Query for autocomplete
        public const string AUTOCOMPLETE_ENDPOINT = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        //Current condition endpoint
        public const string CURRENT_CONDITIONS_ENDPOINT = "currentconditions/v1/{0}?apikey={1}";
        //API key
        public const string API_KEY = "lgxlpAJtvfRbDu2gwTK3GLMMxFeZnbiG";

        //Make async request to the endpoint GET
        public static async Task<List<City>> GetCities(string query)
        {
            List<City> cities = new List<City>();
            //Setup the URL with formatting
            string url = BASE_URL + string.Format(AUTOCOMPLETE_ENDPOINT, API_KEY, query);

            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                //Deserialize json
                cities = JsonConvert.DeserializeObject<List<City>>(json);

            }
            return cities;
        }
        //Make async request the current conditions GET
        public static async Task<CurrentConditions> GetCurrentConditions(string cityKey)
        {
            CurrentConditions currentConditions = new CurrentConditions();
            //Setup the URL with formatting
            string url = BASE_URL + string.Format(CURRENT_CONDITIONS_ENDPOINT, cityKey, API_KEY);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                //Deserialize json
                currentConditions = (JsonConvert.DeserializeObject<List<CurrentConditions>>(json)).FirstOrDefault();

            }

            return currentConditions;
        }
    }
}
