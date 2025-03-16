using System.Collections.Generic;

namespace SaveLoad
{
    public sealed class SaveLoadManager
    {
        private readonly IEnumerable<ISaveLoad> _saveLoads;

        public SaveLoadManager(IEnumerable<ISaveLoad> saveLoads)
        {
            _saveLoads = saveLoads;
        }

        public void SaveAll()
        {
            foreach (var saveLoad in _saveLoads)
            {
                saveLoad.Save();
            }
        }

        public void LoadDefaultAll()
        {
            foreach (var saveLoad in _saveLoads)
            {
                saveLoad.LoadDefault();
            }
        }

        public void LoadAll()
        {
            foreach (var saveLoad in _saveLoads)
            {
                saveLoad.Load();
            }
        }
    }
}