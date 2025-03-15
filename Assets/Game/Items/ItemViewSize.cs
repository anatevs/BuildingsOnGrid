using UnityEngine;

namespace GameCore
{
    public static class ItemViewSize
    {
        private static readonly float _tolerance = 0.01f;

        public static bool IsObjectHasSize(GameObject parentObject, (int, int) sizeXZ, out (float, float) objectXZ)
        {
            var size = GetCombinedSize(parentObject);

            objectXZ = (size.x, size.z);

            Debug.Log(size);

            return (CompareIntFloat(sizeXZ.Item1, size.x) && CompareIntFloat(sizeXZ.Item2, size.z));
        }

        private static Vector3 GetCombinedSize(GameObject parentObject)
        {
            Renderer[] renderers = parentObject.GetComponentsInChildren<Renderer>();

            if (renderers.Length == 0)
            {
                Debug.LogWarning("No renderers found in this object!");
                return Vector3.zero;
            }

            Bounds combinedBounds = renderers[0].bounds;

            foreach (Renderer renderer in renderers)
            {
                combinedBounds.Encapsulate(renderer.bounds);
            }

            return combinedBounds.size;
        }

        private static bool CompareIntFloat(int intVal, float floatVal)
        {
            var diff = intVal - floatVal;

            return (diff <= _tolerance && diff >= -_tolerance);
        }
    }
}