using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherAppWPF.Model;

namespace WeatherAppWPF.ViewModel.Helpers
{
    public class AccuWeatherHelper
    {
        public const string BASE_URL = "http://dataservice.accuweather.com/";
        //zwrócić uwagę na te {} => placeholdery
        public const string AUTOCOMPLETE_ENDPOINT = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        public const string CURRENT_CONDITIONS_ENDPOINT = "currentconditions/v1/{0}?apikey={1}";
        public const string API_KEY = "RHEaGKDdBlcw50ep0TWXQlCCscG30efP";

        public static async Task<List<City>> GetCities(string query)
        {
            List<City> cities = new List<City>();

            //string.Format zastępuje placeholdery stringami
            string url = BASE_URL + string.Format(AUTOCOMPLETE_ENDPOINT, API_KEY, query);

            //request
            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                //otrzymuje pewnego jsona z listą miast
                cities = JsonConvert.DeserializeObject<List<City>>(json);
            }

            return cities;
        }
        
        public static async Task<CurrentConditions> GetCurrentConditionsAsync(string cityKey)
        {
            CurrentConditions currentConditions = new CurrentConditions();
            string url = BASE_URL + string.Format(CURRENT_CONDITIONS_ENDPOINT, cityKey, API_KEY);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                //otrzymuje pewnego jsona z curr conditions, a przypisuje go do 1 obiektu -> muszę wyciągnąć FirstOrDefault
                currentConditions = (JsonConvert.DeserializeObject<List<CurrentConditions>>(json)).FirstOrDefault();
            }

            return currentConditions;
        }

    }
}
