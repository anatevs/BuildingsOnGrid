using System;

namespace SaveLoad
{
    [Serializable]
    public struct MapItemsData
    {
        public OneMapItemData[] Data;

        public MapItemsData(OneMapItemData[] data)
        {
            Data = data;
        }
    }


    [Serializable]
    public struct OneMapItemData
    {
        public string TypeName;

        public string Name;

        public (int x, int z) Position;

        public (int x, int y) OriginIndex;

        public OneMapItemData(string typeName, string name,
            (int x, int z) position, (int x, int y) otriginIndex)
        {
            TypeName = typeName;
            Name = name;
            Position = position;
            OriginIndex = otriginIndex;
        }
    }
}