using System.Collections;
using UnityEngine;

namespace Plucky.Unity
{
    /// <summary>
    /// Keep the transform a constant size regardless of the parent transform.
    /// </summary>
    public class ConstantSize : MonoBehaviour
    {
        public float initialScale = -1;

        // Use this for initialization
        protected void Awake()
        {
            if (initialScale <= 0) initialScale = transform.lossyScale.x;
        }

        // Update is called once per frame
        protected void Update()
        {
            float scale = Mathf.Max(1e-4f, transform.lossyScale.x);
            transform.localScale *= initialScale / scale;
        }
    }
}