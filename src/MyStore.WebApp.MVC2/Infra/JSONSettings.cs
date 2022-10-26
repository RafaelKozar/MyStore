using Newtonsoft.Json;

namespace MyStore.WebApp.MVC2.Infra
{
    public class JSONSettings
    {
        public static JsonSerializerSettings serializationSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.None,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            PreserveReferencesHandling = PreserveReferencesHandling.All
            //PreserveReferencesHandling = PreserveReferencesHandling.Objects     
        };
    }
}
