using MvcApiPersonajesAWS.Models;
using System.Net.Http.Headers;

namespace MvcApiPersonajesAWS.Services {
    public class ServiceApiPersonajes {

        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceApiPersonajes(IConfiguration configuration) {
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiPersonajes");
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<T?> CallApiAsync<T>(string request) {
            using (HttpClient client = new HttpClient()) {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);

                HttpResponseMessage response = await client.GetAsync(request);
                return (response.IsSuccessStatusCode) ? await response.Content.ReadAsAsync<T>() : default(T);
            }
        }

        public async Task<List<Personaje>> GetPersonajesAsync() {
            string request = "/api/personajes";
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

    }
}
