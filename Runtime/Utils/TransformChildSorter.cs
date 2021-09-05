using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Plucky.Unity
{
    /// TransformChildSorter sorts the children of this object by name. This fixes the arbitrary
    /// re-ordering of children that sometimes happens in the editor.
    public class TransformChildSorter : MonoBehaviour
    {
        class Comparer : IComparer<Transform>
        {
            public int Compare(Transform a, Transform b)
            {
                return a.name.CompareTo(b.name);
            }
        }

        public void Apply()
        {
            Sort(transform);
        }

        public static void Sort(Transform target)
        {
            // use this craziness to get RectTransform and Transform.
            var children = target.gameObject.GetComponentsInChildren<Transform>().
                Where(x => x.parent == target).ToList();
            children.Sort(new Comparer());

            for (int i = 0; i < children.Count; i++)
            {
                children[i].SetSiblingIndex(i);
            }
        }

        void Awake()
        {
            Apply();
        }
    }
}
