using System.Collections;
using UnityEngine;

namespace docker
{
    /// <summary>
    /// FixedWorldLocation forces this object to a fixed world location every frame.
    /// </summary>
    public class FixedWorldLocation : MonoBehaviour
    {
        public Vector3 position;
        public Vector3 rotation;

        protected void Update()
        {
            transform.position = position;
            transform.eulerAngles = rotation;
        }
    }
}