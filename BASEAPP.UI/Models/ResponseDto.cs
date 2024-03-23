using Newtonsoft.Json;

namespace BASEAPP.UI.Models
{
    public class ResponseWithListItem<T> 
    {
        [JsonProperty("result")]
        public ResponseItem<T> Result { get; set; }
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; } = true;
        [JsonProperty("message")]
        public string Message { get; set; } = "";
    }

    public class Response<T>
    {
        [JsonProperty("result")]
        public T Result { get; set; }
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; } = true;
        [JsonProperty("message")]
        public string Message { get; set; } = "";
    }

    public class ResponseItem<T>
    {
        [JsonProperty("items")]
        public T Items { get; set; }

        [JsonProperty("totalItems")]
        public int TotalItems { get; set; }
    }
}
