using UnityEngine;

namespace Plucky.UnityExtensions
{
    public static class TransformExtensions
    {
        public static void DestroyChildren(this Transform transform)
        {
            int watchdog = transform.childCount * 2 + 10;
            while (transform.childCount > 0)
            {
                GameObject.DestroyImmediate(transform.GetChild(0).gameObject);
                if (watchdog-- <= 0)
                {
                    Debug.Log(transform.childCount);
                    Debug.LogWarning("Failed to delete all children.");
                    return;
                }
            }
        }

        public static string GetPath(this Transform current)
        {
            if (current.parent == null) return "/" + current.name;
            return current.parent.GetPath() + "/" + current.name;
        }
    }
}
