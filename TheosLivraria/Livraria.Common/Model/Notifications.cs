using MediatR;
using Newtonsoft.Json;

namespace Livraria.Common.Model
{
    public class Notifications : INotification
    {
        [JsonProperty(Order = -2)]
        public string Key { get; }

        [JsonProperty(Order = -1)]
        public string Value { get; }

        public Notifications(
            string key,
            string value)
        {
            Key = key;
            Value = value;
        }
    }
}
