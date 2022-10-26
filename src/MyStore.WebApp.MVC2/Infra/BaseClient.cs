using MyStore.WebApp.MVC2.Infra;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;
using RestSharp.Serializers.NewtonsoftJson;
using System.Configuration;

namespace WebApi.Infra
{
    public class BaseClient
    {
        //public T Get<T>(string path, params KeyValuePair<string, object>[] parameters) where T : new()
        //{
        //    var client = GetClient();
        //    var request = new RestRequest(path, Method.GET);
             
        //    parameters.ToList().ForEach(p => request.AddQueryParameter(p.Key, p.Value.ToString()));
        //    var response = client.Execute(request);
        //    ValidadeErrors(response);
        //    if (response.ErrorException != null) throw new Exception(response.ErrorException.ToString());
        //    var result = JsonConvert.DeserializeObject<T>(response.Content, JSONSettings.serializationSettings);
        //    return result;
        //}

        //public async Task<RestResponse> GetAsync<T>(string path, params KeyValuePair<string, object>[] parameters) where T : new()
        //{
        //    var client = GetClient();
        //    var request = new RestRequest(path, Method.Get);
             

        //    parameters.ToList().ForEach(p => request.AddQueryParameter(p.Key, p.Value.ToString()));

        //    var response  = await client.GetAsync(request);            
        //    JsonSerializer.Deserialize
        //}

        //public T Post<T>(string path, object obj) where T : new()
        //{
        //    var client = GetClient();
        //    var request = new RestRequest(path, Method.POST);
             
        //    var json = JsonConvert.SerializeObject(obj, JSONSettings.serializationSettings);
        //    request.AddParameter("Application/Json", json, ParameterType.RequestBody);
        //    var response = client.Execute<T>(request);
            
        //    return response.Data;
        //}

        //public void Post(string path, object obj,
        //    params KeyValuePair<string, object>[] parameters)
        //{
        //    var client = GetClient();
        //    var request = new RestRequest(path, Method.Post);
             
        //    parameters.ToList().ForEach(p => request.AddQueryParameter(p.Key, p.Value.ToString()));
        //    var json = JsonConvert.SerializeObject(obj, JSONSettings.serializationSettings);
        //    request.AddParameter("Application/Json", json, ParameterType.RequestBody);
        //    var response = client.Execute(request);            
        //}

        //public async Task PostAsync<T>(string path, object obj)
        //    where T : new()
        //{
        //    var client = GetClient();
        //    var request = new RestRequest(path, Method.Post);
             
        //    request.AddJsonBody(obj);
        //    await client.ExecuteAsync(request);
        //}

        //private RestClient GetClient()
        //{
        //    var client = new RestClient("https://localhost:7139/");
        //    client.UseNewtonsoftJson();
        //    return client;
        //}      
    }
}
