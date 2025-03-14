using Newtonsoft.Json;

namespace SaveLoad
{
    public static class JsonConverters
    {
        public static JsonConverter[] Converters
        {
            get => _converters;
            private set => _ = _converters;
        }

        private static readonly JsonConverter[] _converters = new JsonConverter[]
        {
        new Vector3JsonConverter(),
        new QuaternionJsonConverter()
        };
    }
}