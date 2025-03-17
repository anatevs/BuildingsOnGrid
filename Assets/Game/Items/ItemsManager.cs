using System;
using UnityEngine;
using VContainer.Unity;

namespace GameCore
{
    public sealed class ItemsManager :
        IInitializable,
        IDisposable
    {
        private Player _player;

        private MapItemSpawner _spawner;

        private int _currentSelectedID = -1;

        public ItemsManager(Player player,
            MapItemSpawner spawner)
        {
            _player = player;
            _spawner = spawner;
        }

        void IInitializable.Initialize()
        {
            _player.OnItemRemoved += _spawner.Unspawn;
        }

        void IDisposable.Dispose()
        {
            _player.OnItemRemoved -= _spawner.Unspawn;
        }

        public void SetCurrentSelected(int id)
        {
            _currentSelectedID = id;
        }

        public void SetItem()
        {
            if (_currentSelectedID == -1)
            {
                return;
            }

            if (_player.IsSticking)
            {
                _player.UnsetCurrentSticking(out var current);

                _spawner.Unspawn(current);
            }

            var item = _spawner.Spawn(_currentSelectedID);

            _player.SetCurrentSticking(item);

            _currentSelectedID = -1;
        }
    }
}