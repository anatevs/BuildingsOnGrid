using System;

namespace SaveLoad
{
    [Serializable]
    public struct ItemData
    {
        public string TypeName;

        public string Name;

        public ItemData(string typeName, string name)
        {
            TypeName = typeName;
            Name = name;
        }
    }
}