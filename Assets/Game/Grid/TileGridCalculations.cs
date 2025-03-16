using UnityEngine;

namespace GameCore
{
    public class TileGridCalculations
    {
        private readonly (int x, int y) _mapSize;

        private readonly int _cellSize = 1;

        private (int x, int y) _centerShift;

        public TileGridCalculations((int x, int y) size)
        {
            _mapSize = size;

            _centerShift = (
                _mapSize.x / 2,
                _mapSize.y / 2
                );
        }

        public bool IsValidIndex((int x, int y) index)
        {
            return (index.x >= 0 && index.x < _mapSize.x &&
                index.y >= 0 && index.y < _mapSize.y);
        }

        public Vector2Int PositionToIndex(Vector3 position)
        {
            var x = GetCoordinateInt(position.x) + _centerShift.x;
            var y = GetCoordinateInt(position.z) + _centerShift.y;

            return new Vector2Int(x, y);
        }

        public int GetCoordinateInt(float coordinate)
        {
            var res = coordinate / _cellSize;

            return Mathf.FloorToInt(res);
        }
    }
}