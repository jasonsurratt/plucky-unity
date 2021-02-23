using Plucky.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plucky.Unity
{
    public static class MonoBehaviourExtensions
    {
        public static (T, float) Farthest<T>(this IEnumerable<T> it, Vector3 pos) where T : MonoBehaviour
        {
            return it.Farthest(x => x.transform.position, pos);
        }

        public static (T, float) Farthest<T>(this IEnumerable<T> it, Func<T, Vector3> func, Vector3 pos) where T : class
        {
            float maxDSqr = 0;
            T result = null;
            foreach (var t in it)
            {
                float d = (func(t) - pos).sqrMagnitude;
                if (d > maxDSqr)
                {
                    maxDSqr = d;
                    result = t;
                }
            }

            return (result, Mathf.Sqrt(maxDSqr));
        }

        public static T GetOrAddComponent<T>(this MonoBehaviour mb) where T : Component
        {
            T result = mb.GetComponent<T>();
            if (!result)
            {
                result = mb.gameObject.AddComponent<T>();
            }
            return result;
        }

        public static string GetPath(this MonoBehaviour mb)
        {
            return mb.transform.GetPath() + "/" + mb.GetType().ToString();
        }

        public static T MustGetComponent<T>(this MonoBehaviour mb) where T : Component
        {
            T result = mb.GetComponent<T>();

            if (result == null)
            {
                throw new Exception($"failed to get mandatory component {typeof(T)} {mb}");
            }

            return result;
        }

        public static float MinimumDistance<T>(this IEnumerable<T> it, Vector3 pos) where T : MonoBehaviour
        {
            float minDSqr = float.MaxValue;
            foreach (var t in it)
            {
                float d = (t.transform.position - pos).sqrMagnitude;
                if (d < minDSqr)
                {
                    minDSqr = d;
                }
            }

            return Mathf.Sqrt(minDSqr);
        }

        public static T Nearest<T>(this IEnumerable<T> it, Vector3 pos) where T : MonoBehaviour
        {
            float minDSqr = float.MaxValue;
            T result = null;
            foreach (var t in it)
            {
                float d = (t.transform.position - pos).sqrMagnitude;
                if (d < minDSqr)
                {
                    minDSqr = d;
                    result = t;
                }
            }

            return result;
        }

        public static void WaitOneFrame(this MonoBehaviour mb, Action action)
        {
            IEnumerator WaitFunc()
            {
                yield return null;
                action();
            }

            if (mb) mb.StartCoroutine(WaitFunc());
        }

        public static void WaitRealtime(this MonoBehaviour mb, float delay, Action action)
        {
            IEnumerator WaitFunc()
            {
                yield return new WaitForSecondsRealtime(delay);
                action();
            }

            if (mb) mb.StartCoroutine(WaitFunc());
        }

        public static void WaitForSeconds(this MonoBehaviour mb, float delay, Action action)
        {
            if (delay <= 0)
            {
                action();
                return;
            }

            IEnumerator WaitFunc()
            {
                yield return new WaitForSeconds(delay);
                action();
            }

            if (mb) mb.StartCoroutine(WaitFunc());
        }

        public static void WaitUntil(this MonoBehaviour mb, Func<bool> predicate, Action action)
        {
            if (predicate())
            {
                action.Invoke();
                return;
            }

            IEnumerator WaitFunc()
            {
                while (!predicate()) yield return null;

                action();
            }

            // mb can be invalid w/ a dangling pointer when called from a delayed event or similar.
            if (mb) mb.StartCoroutine(WaitFunc());
        }

        /// <summary>
        /// WithinRadius returns all objects whose position is within the specified radius. This
        /// DOES NOT consider the bounds of the object.
        /// </summary>
        public static IEnumerable<T> WithinRadius<T>(this IEnumerable<T> it, Vector3 pos, float radius) where T : MonoBehaviour
        {
            float sqrRadius = radius * radius;
            foreach (var t in it)
            {
                float d = (t.transform.position - pos).sqrMagnitude;
                if (d <= sqrRadius)
                {
                    yield return t;
                }
            }
        }

    }
}
