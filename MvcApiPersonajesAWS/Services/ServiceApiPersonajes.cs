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

        public async Task<string> TestApiAsync() {
            string request = "/api/personajes/GetPersonajes";

            // Utilizamos un manejador para la petición del HttpClient
            var handler = new HttpClientHandler();

            // Indicamos al manejador como se comportará al recibir peticiones
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => {
                return true;
            };

            HttpClient client = new HttpClient(handler);
            client.BaseAddress = new Uri(this.UrlApi);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(this.header);

            HttpResponseMessage response = await client.GetAsync(request);
            return "Respuesta: " + response.StatusCode;
        }

        public async Task<T?> CallApiAsync<T>(string request) {
            using (HttpClientHandler handler = new HttpClientHandler()) {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => {
                    return true;
                };
                using (HttpClient client = new HttpClient(handler)) {
                    client.BaseAddress = new Uri(this.UrlApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(this.header);

                    HttpResponseMessage response = await client.GetAsync(request);
                    return (response.IsSuccessStatusCode) ? await response.Content.ReadAsAsync<T>() : default(T);
                }
            }
        }

        public async Task<List<Personaje>> GetPersonajesAsync() {
            string request = "/api/personajes";
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

    }
}
