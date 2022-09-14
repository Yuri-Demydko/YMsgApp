using Newtonsoft.Json;

namespace YMsgApp.Models.DtoModels.ResponseModels.Wrappers;

public class ODataObjectWrapper<T> where T:class
{
    [JsonProperty("@odata.context")]
    public string Context { get; set; }
    public List<T> Value { get; set; }
}