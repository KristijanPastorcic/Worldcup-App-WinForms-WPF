using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldcupDAL.Enums;
using WorldcupDAL.Models;

namespace WorldcupDAL
{
    public class WorldcupRepository
    {
        private static string W_TEAMS_API = "http://worldcup.sfg.io/teams/results";
        private static string M_TEAMS_API = "http://world-cup-json-2018.herokuapp.com/teams/results";

        private static string W_MATCHES_API = "http://worldcup.sfg.io/matches/country?fifa_code=";
        private static string M_MATCHES_API = "http://world-cup-json-2018.herokuapp.com/matches/country?fifa_code=";

        public static async Task<IEnumerable<CountryTeam>> GetCountriesAsync(Gender gender)
        {

            RestClient api;
            switch (gender)
            {
                case Gender.Male:
                    api = new RestClient(M_TEAMS_API);
                    break;
                case Gender.Female:
                    api = new RestClient(W_TEAMS_API);
                    break;
                default:
                    throw new Exception("no teem");
            }
            RestResponse response = await api.ExecuteAsync(new RestRequest());

            IList<CountryTeam> countryTeams = 
                JsonConvert.DeserializeObject<List<CountryTeam>>(response.Content);
            return countryTeams;
        }

        public static async Task<IList<Data>> GetTeemDataAsync(string country, Gender gender)
        {

            RestClient api;
            switch (gender)
            {
                case Gender.Male:
                    api = new RestClient(M_MATCHES_API + country);
                    break;
                case Gender.Female:
                    api = new RestClient(W_MATCHES_API + country);
                    break;
                default:
                    throw new Exception("no teem");
            }

            RestResponse response = await api.ExecuteAsync(new RestRequest());
            var data = JsonConvert.DeserializeObject<IList<Data>>(response.Content);
            return data;
        }

    }
}
