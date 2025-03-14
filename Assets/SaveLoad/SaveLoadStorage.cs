using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

namespace SaveLoad
{
    public class SaveLoadStorage
    {
        private readonly string _filePath;

        private Dictionary<string, string> _data = new();

        public SaveLoadStorage()
        {
            _filePath = Path.Combine(Application.persistentDataPath, "gameData.json");
        }

        public void SetData<T>(T value)
        {
            string keyName = typeof(T).Name;

            string paramsJson = JsonConvert.SerializeObject(value, JsonConverters.Converters);

            _data[keyName] = paramsJson;
        }

        public T GetData<T>()
        {
            string paramsInJson = _data[typeof(T).Name];

            return JsonConvert.DeserializeObject<T>(paramsInJson);
        }

        public bool TryGetData<T>(out T value)
        {
            if (_data.TryGetValue(typeof(T).Name, out var paramsInJson))
            {
                value = JsonConvert.DeserializeObject<T>(paramsInJson);
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }

        public void SaveState()
        {
            string dataJSON = JsonConvert.SerializeObject(_data);

            File.WriteAllText(_filePath, dataJSON);
        }

        public void LoadState()
        {
            if (File.Exists(_filePath))
            {
                string jsonData = File.ReadAllText(_filePath);

                _data = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);
            }
            else
            {
                Debug.LogWarning("File not found!");
            }
        }
    }
}