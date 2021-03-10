using UnityEngine;

namespace Plucky.Unity
{
    public static class GameObjectExtentions
    {
        //public static float ApproximateRadius(this GameObject go)
        //{
        //    var bounds = Util.GetBounds(go);

        //    return Mathf.Max(bounds.extents.x, bounds.extents.y, bounds.extents.z);
        //}

        ///// Closest returns the closest GameObject in the provided collection. If the GameObject
        ///// supports IBodyPartTransforms then the location transform will be used.
        //public static GameObject Closest(this IEnumerable<GameObject> en, Vector3 from)
        //{
        //    float d2 = float.MaxValue;
        //    GameObject result = null;
        //    foreach (var it in en)
        //    {
        //        Vector3 to = it.transform.position;
        //        IBodyPartTransforms body = it.GetComponent<IBodyPartTransforms>();
        //        if (body != null)
        //        {
        //            to = body.GetLocationTransform().position;
        //        }
        //        float d = (to - from).sqrMagnitude;

        //        if (d < d2)
        //        {
        //            d2 = d;
        //            result = it;
        //        }
        //    }

        //    return result;
        //}

        public static GameObject FindObject(this GameObject parent, string name, bool recursive = false)
        {
            Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
            foreach (Transform t in trs)
            {
                if (t.name == name && (recursive == true || t.parent == parent.transform))
                {
                    return t.gameObject;
                }
            }
            return null;
        }

        /// Make a best effort to determine the game object's bounds in world coordinates.
        public static Bounds GetBounds(this GameObject go)
        {
            bool boundsSet = false;
            Bounds b = BoundsExtensions.invalid;

            Collider[] colliders = go.GetComponentsInChildren<Collider>();
            foreach (Collider c in colliders)
            {
                if (c.isTrigger)
                {
                    continue;
                }
                if (!boundsSet)
                {
                    boundsSet = true;
                    b = c.bounds;
                }
                else
                {
                    b.Encapsulate(c.bounds);
                }
            }
            if (boundsSet)
            {
                return b;
            }


            Renderer[] renderers = go.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in renderers)
            {
                if (!boundsSet)
                {
                    boundsSet = true;
                    b = r.bounds;
                }
                else
                {
                    b.Encapsulate(r.bounds);
                }
            }

            // if the bounds still aren't set, fall back and use triggers
            if (!boundsSet)
            {
                foreach (Collider c in colliders)
                {
                    if (!boundsSet)
                    {
                        boundsSet = true;
                        b = c.bounds;
                    }
                    else
                    {
                        b.Encapsulate(c.bounds);
                    }
                }
            }

            return b;
        }

        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            T result = go.GetComponent<T>();
            if (!result)
            {
                result = go.gameObject.AddComponent<T>();
            }
            return result;
        }

        public static string GetPath(this GameObject go) => go.transform.GetPath();

        //public static IEnumerable<GameObject> Living(this IEnumerable<GameObject> go)
        //{
        //    return go.Where(delegate (GameObject x)
        //    {
        //        var h = x.GetComponent<Health>();
        //        if (h && h.IsAlive()) return true;
        //        return false;
        //    });
        //}

    }
}
