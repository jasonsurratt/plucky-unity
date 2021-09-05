using UnityEditor;
using UnityEngine;

namespace Plucky.Unity
{
    [CustomEditor(typeof(TransformChildSorter))]
    public class TransformChildSorterEditor : Editor
    {
        [MenuItem("GameObject/Knockback/Sort Children", false, 49)]
        public static void GridifyChildren(MenuCommand cmd)
        {
            GameObject go = (GameObject)cmd.context;
            TransformChildSorter.Sort(go.transform);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            TransformChildSorter t = (TransformChildSorter)target;
            if (GUILayout.Button("Sort"))
            {
                t.Apply();
            }
        }
    }
}
