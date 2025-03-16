using UnityEngine;

namespace GameCore
{
    public sealed class PlayingGrid
    {
        private readonly int[] _size = new int[2] {1, 1};

        private TileGridCalculations _gridCalculations;

        private int[,] _itemsGrid;

        public PlayingGrid(int[] size)
        {
            _size = size;

            InitGrid();
        }

        private void InitGrid()
        {
            _gridCalculations = new TileGridCalculations((_size[0], _size[1]));

            _itemsGrid = new int[_size[0], _size[1]];
        }

        public (int x, int z) GetTileOriginCoordinate(Vector3 pos)
        {
            return (_gridCalculations.GetCoordinateInt(pos.x),
                _gridCalculations.GetCoordinateInt(pos.z));
        }

        public (int x, int y) GetTileIndex((int x, int z) tilePos)
        {
            return _gridCalculations.TilePositionToIndex(tilePos);
        }

        public (int x, int y) GetTileIndex(Vector3 pos)
        {
            var tileOriginPos = GetTileOriginCoordinate(pos);

            return _gridCalculations.TilePositionToIndex(tileOriginPos);
        }

        public bool IsAreaFree((int x, int y) origin, (int x, int y) size)
        {
            for (int i = origin.x; i < origin.x + size.x; i++)
            {
                for (var j = origin.y; j < origin.y + size.y; j++)
                {
                    if (_itemsGrid[i, j] != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void SetArea((int x, int y) origin, (int x, int y) size, int id)
        {
            for (int i = origin.x; i < origin.x + size.x; i++)
            {
                for (var j = origin.y; j < origin.y + size.y; j++)
                {
                    _itemsGrid[i, j] = id;
                }
            }
        }

        public void ClearArea((int x, int y) origin, (int x, int y) size)
        {
            SetArea(origin, size, 0);
        }
    }
}