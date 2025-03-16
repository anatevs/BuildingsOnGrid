using System.Drawing;
using UnityEngine;

namespace GameCore
{
    public static class ItemViewSize
    {
        private static readonly float _tolerance = 0.01f;

        public static bool IsObjectHasSize(GameObject parentObject, (int, int) sizeXZ, out (float, float) objectXZ)
        {
            var bounds = GetBounds(parentObject);

            return IsBoundsHaveSize(bounds, sizeXZ, out objectXZ);
        }

        public static bool IsBoundsHaveSize(Bounds bounds, (int, int) sizeXZ, out (float, float) objectXZ)
        {
            objectXZ = (bounds.size.x, bounds.size.z);

            return (CompareIntFloat(sizeXZ.Item1, bounds.size.x) && CompareIntFloat(sizeXZ.Item2, bounds.size.z));
        }

        public static Bounds GetBounds(GameObject parentObject)
        {
            Renderer[] renderers = parentObject.GetComponentsInChildren<Renderer>();

            if (renderers.Length == 0)
            {
                Debug.LogWarning("No renderers found in this object!");
                return default;
            }

            Bounds combinedBounds = renderers[0].bounds;

            foreach (Renderer renderer in renderers)
            {
                combinedBounds.Encapsulate(renderer.bounds);
            }

            return combinedBounds;
        }

        private static bool CompareIntFloat(int intVal, float floatVal)
        {
            var diff = intVal - floatVal;

            return (diff <= _tolerance && diff >= -_tolerance);
        }
    }
}