using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plucky.Unity
{
    public class ToggleActive : MonoBehaviour
    {
        public void Toggle()
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
    }
}
