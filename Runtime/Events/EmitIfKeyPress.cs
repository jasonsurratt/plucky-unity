using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace dockbound
{
    public class EmitIfKeyPress : MonoBehaviour
    {
        [Tooltip("If any of these keycodes are true (GetKey) and trigger is called, an event will be emitted.")]
        public KeyCode[] keyCodes = Array.Empty<KeyCode>();
        public UnityEvent onTriggerAndKeyDown;

        public void Trigger()
        {
            foreach (var kc in keyCodes)
            {
                if (Input.GetKey(kc))
                {
                    onTriggerAndKeyDown.Invoke();
                }
            }
        }
    }
}