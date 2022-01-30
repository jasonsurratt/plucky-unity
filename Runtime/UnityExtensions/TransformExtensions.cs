using UnityEngine;

namespace Plucky.UnityExtensions
{
    public static class TransformExtensions
    {
        public static void DestroyChildren(this Transform transform, float delay = 0)
        {
            if (delay > 0)
            {
                // in rare instances this approach may not destroy all children.
                foreach (Transform child in transform)
                {
                    GameObject.Destroy(child.gameObject, delay);
                }
            }
            else
            {
                // this approach is more reliable and will log a warning if something fails.
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
        }

        public static string GetPath(this Transform current)
        {
            if (current.parent == null) return "/" + current.name;
            return current.parent.GetPath() + "/" + current.name;
        }
    }
}
