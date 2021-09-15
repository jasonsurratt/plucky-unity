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
        protected void Start()
        {
            if (initialScale <= 0) initialScale = transform.lossyScale.x;
        }

        // Update is called once per frame
        protected void Update()
        {
            float scale = transform.lossyScale.x;
            transform.localScale *= initialScale / scale;
        }
    }
}