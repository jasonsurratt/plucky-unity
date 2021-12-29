using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Plucky.Unity
{
    public class KeyPressCapture : MonoBehaviour
    {
        public UnityEvent keyPressEvent;
        public Key keyCode;

        [Tooltip("If onlyOnFocus is set, the event will only be fired if onlyOnFocus has focus.")]
        public bool hadFocus = false;
        public InputField onlyOnFocus;

        protected void Update()
        {
            if (Keyboard.current[keyCode].wasPressedThisFrame &&
                (onlyOnFocus == null || hadFocus))
            {
                keyPressEvent.Invoke();
            }
            hadFocus = onlyOnFocus == null ? false : onlyOnFocus.isFocused;
        }
    }
}
