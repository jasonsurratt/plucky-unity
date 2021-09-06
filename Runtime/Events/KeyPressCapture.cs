using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Plucky.Unity
{
    public class KeyPressCapture : MonoBehaviour
    {
        public UnityEvent keyPressEvent;
        public KeyCode keyCode;

        protected void Update()
        {
            if (Input.GetKeyDown(keyCode))
            {
                keyPressEvent.Invoke();
            }
        }
    }
}
