using MvcApiPersonajesAWS.Models;
using Newtonsoft.Json;
using NuGet.Common;
using System.Data;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

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
            string request = "/api/personajes/GetPersonajes";
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

        // POST ================================================================================================
        private async Task<bool> InsertApiAsync<T>(string request, T objeto) {
            using (HttpClientHandler handler = new HttpClientHandler()) {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => {
                    return true;
                };
                using (HttpClient client = new HttpClient(handler)) {
                    client.BaseAddress = new Uri(this.UrlApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(this.header);

                    string json = JsonConvert.SerializeObject(objeto);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(request, content);
                    return response.IsSuccessStatusCode;
                }
            }
        }

        public async Task InsertPersonajeAsync(string nombre, string imagen) {
            string request = "api/personajes/InsertPersonaje/" + nombre + "/" + imagen;
            await this.InsertApiAsync<Personaje>(request, null);
            
            //string request = "api/personajes/InsertPersonaje";
            //Personaje personaje = new Personaje {
            //    IdPersonaje = 0,
            //    Nombre = nombre,
            //    Imagen = imagen
            //};
            //await this.InsertApiAsync<Personaje>(request, personaje);
        }

        // PUT =================================================================================================
        private async Task<bool> UpdateApiAsync<T>(string request, T objeto) {
            using (HttpClientHandler handler = new HttpClientHandler()) {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => {
                    return true;
                };
                using (HttpClient client = new HttpClient(handler)) {
                    client.BaseAddress = new Uri(this.UrlApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(this.header);

                    string json = JsonConvert.SerializeObject(objeto);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync(request, content);
                    return response.IsSuccessStatusCode;
                }
            }
        }

        public async Task UpdatePersonajeAsync(int idPersonaje, string nombre, string imagen) {
            string request = "api/personajes/UpdatePersonaje";
            Personaje personaje = new Personaje {
                IdPersonaje = idPersonaje,
                Nombre = nombre,
                Imagen = imagen
            };
            await this.UpdateApiAsync<Personaje>(request, personaje);
        }

        // DELETE ===============================================================================================
        private async Task<bool> DeleteApiAsync(string request) {
            using (HttpClientHandler handler = new HttpClientHandler()) {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => {
                    return true;
                };
                using (HttpClient client = new HttpClient(handler)) {
                    client.BaseAddress = new Uri(this.UrlApi);
                    client.DefaultRequestHeaders.Clear();

                    HttpResponseMessage response = await client.DeleteAsync(request);
                    return response.IsSuccessStatusCode;
                }
            }
        }

        public async Task DeletePersonajeAsync(int idPersonaje) {
            string request = "api/personajes/DeletePersonaje/" + idPersonaje;
            await this.DeleteApiAsync(request);
        }

    }
}
