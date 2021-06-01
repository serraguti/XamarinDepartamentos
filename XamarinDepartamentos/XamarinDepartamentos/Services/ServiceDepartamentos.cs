using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using XamarinDepartamentos.Models;

namespace XamarinDepartamentos.Services
{
    public class ServiceDepartamentos
    {
        private String url;

        public ServiceDepartamentos()
        {
            this.url = "https://apicruddepartamentoscorepgs.azurewebsites.net/";
        }

        private async Task<T> CallApiAsync<T>(String request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add
                    (new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    String json =
                        await response.Content.ReadAsStringAsync();
                    T data =
                        JsonConvert.DeserializeObject<T>(json);
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Departamento>> GetDepartamentosAsync()
        {
            String request = "api/departamentos";
            List<Departamento> depts =
                await this.CallApiAsync<List<Departamento>>(request);
            return depts;
        }

        public async Task<Departamento> FindDepartamento(int id)
        {
            String request = "api/departamentos/" + id;
            Departamento dept = await
                this.CallApiAsync<Departamento>(request);
            return dept;
        }

        public async Task InsertDepartamento(int id, String nombre
            , String localidad)
        {
            Departamento departamento = new Departamento
            {
                IdDepartamento = id
                ,
                Nombre = nombre,
                Localidad = localidad
            };
            String json = JsonConvert.SerializeObject(departamento);
            StringContent content =
                new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                String request = "api/departamentos";
                Uri uri = new Uri(this.url + request);
                await client.PostAsync(uri, content);
            }
        }

        public async Task UpdateDepartamento(int id
            , String nombre, String localidad)
        {
            Departamento departamento = await this.FindDepartamento(id);
            departamento.Nombre = nombre;
            departamento.Localidad = localidad;
            String json = JsonConvert.SerializeObject(departamento);
            StringContent content =
                new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                String request = "api/departamentos";
                Uri uri = new Uri(this.url + request);
                await client.PutAsync(uri, content);
            }
        }

        public async Task DeleteDepartamento(int id)
        {
            String request = "api/departamentos/" + id;
            Uri uri = new Uri(this.url + request);
            using (HttpClient client = new HttpClient())
            {
                await client.DeleteAsync(uri);
            }
        }
    }
}
