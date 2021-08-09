using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Docent
{
    /// <summary>
    /// SafeArea rescales the current canvas to omit any "notches" in the screen.
    /// 
    /// Got this from stack overflow or a Unity board. Doh, can't find the link. :(
    /// </summary>
    public class SafeArea : MonoBehaviour
    {
        public float bottomOffset = 0;
        public float leftOffset = 0;
        public float rightOffset = 0;
        public float topOffset = 0;
        RectTransform panel;
        public Rect LastSafeArea = new Rect(0, 0, 0, 0);

        protected void Awake()
        {
            panel = GetComponent<RectTransform>();
            Refresh();
        }

        protected void Update()
        {
            Refresh();
        }

        void Refresh()
        {
            Rect safeArea = Screen.safeArea;
            safeArea.yMax -= topOffset;
            safeArea.xMin += leftOffset;
            safeArea.yMin += bottomOffset;
            safeArea.xMax -= rightOffset;

            if (safeArea != LastSafeArea) ApplySafeArea(safeArea);
        }

        void ApplySafeArea(Rect r)
        {
            LastSafeArea = r;
            

            Debug.Log($"{r.min} {r.max} {Display.main.renderingWidth}x{Display.main.renderingHeight}");


            panel.offsetMax = new Vector2(r.xMax - Display.main.renderingWidth, -(Display.main.renderingHeight - r.yMax));
            panel.offsetMin = new Vector2(r.xMin, r.yMin);
            Debug.Log($"{panel.offsetMin} {panel.offsetMax}");
        }
    }
}
