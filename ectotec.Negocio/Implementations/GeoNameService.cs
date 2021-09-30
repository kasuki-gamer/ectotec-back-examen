using ectotec.Models.ViewModels;
using ectotec.Negocio.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ectotec.Negocio.Implementations
{
    public class GeoNameService: IGeoNameService
    {
        private readonly IHttpClientFactory _clientFactory;

        public GeoNameService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }


        public async Task<GeoNameResponse> GetConsulta(string text)
        {

            var url = "http://api.geonames.org/searchJSON?q="+ text + "&country=MX&maxRows=10&username=kasuki";


            var request = new HttpRequestMessage(HttpMethod.Get, url );
            // request.Headers.Add("Content-Type", "application/json");


            var client = _clientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<GeoNameResponse>(response.Content.ReadAsStringAsync().Result);

            }

            return null;
        }


    }
}
