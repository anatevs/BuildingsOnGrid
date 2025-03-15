using System.Collections;
using UnityEngine;

namespace GameCore
{
    public class TypeItemsConfig<T> : ScriptableObject where T : MapItem
    {
        [SerializeField]
        private string _nameID;

        [SerializeField]
        private T[] _itemsPrefabs;

        private void OnEnable()
        {
            for (int i = 0; i < _itemsPrefabs.Length; i++)
            {
                _itemsPrefabs[i].Init(i, _nameID);
            }
        }
    }
}