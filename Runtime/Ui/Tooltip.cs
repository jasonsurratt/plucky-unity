using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Plucky.Unity
{
    public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Tooltip("If showOnStart is true, show an effect to draw attention to the tooltip on start, but only on start.")]
        public bool effectOnStart = true;
        public bool showOnStart = false;
        private static GameObject _prefab;
        [TextArea(3, 10)]
        public string tip;
        private GameObject _tipDisplay;

        public void Hide()
        {
            if (_tipDisplay != null)
            {
                Destroy(_tipDisplay);
                _tipDisplay = null;
            }
        }

        public void OnDestroy()
        {
            Hide();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Show();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Hide();
        }

        public void SetTip(string tip)
        {
            this.tip = tip;
            if (_tipDisplay != null)
            {
                _tipDisplay.GetComponentInChildren<Text>().text = tip;
            }
        }

        public void Show()
        {
            if (tip == "" || _tipDisplay != null)
            {
                return;
            }

            if (_prefab == null)
            {
                _prefab = Resources.Load<GameObject>("Prefabs/Ui/Tooltip");
            }

            Canvas canvas = GetComponentInParent<Canvas>();
            _tipDisplay = Instantiate(_prefab);
            _tipDisplay.transform.SetParent(canvas.transform);

            if (effectOnStart && showOnStart)
            {
                // only do this once.
                effectOnStart = false;

                var effect = _tipDisplay.transform.Find("DisplayEffect");
                if (effect) effect.gameObject.SetActive(true);
            }

            // this is a really ugly way to retrieve the bounds. Is there a better way?
            var rt = GetComponent<RectTransform>();
            Vector3[] fourCorners = new Vector3[4];
            rt.GetWorldCorners(fourCorners);

            Vector3[] worldCorners = new Vector3[4];
            canvas.GetComponent<RectTransform>().GetWorldCorners(worldCorners);

            Vector3 pos = Input.mousePosition;
            Vector2 pivot = _tipDisplay.GetComponent<RectTransform>().pivot;

            float offset = 10;

            Vector3 thisCenter = (fourCorners[2] + fourCorners[0]) / 2f;
            Vector3 worldCenter = (worldCorners[2] + worldCorners[0]) / 2f;

            if (thisCenter.y < worldCenter.y)
            {
                pos.y = fourCorners[1].y + offset;
                pivot.y = 0;
            }
            else
            {
                pos.y = fourCorners[0].y - offset;
                pivot.y = 1;
            }

            if (thisCenter.x < worldCenter.x)
            {
                pos.x = thisCenter.x;
                pivot.x = 0;
            }
            else
            {
                pos.x = thisCenter.x;
                pivot.x = 1;
            }
            //Rect rect = GetComponent<RectTransform>().rect;

            _tipDisplay.transform.position = pos;
            _tipDisplay.GetComponent<RectTransform>().pivot = pivot;
            _tipDisplay.transform.localScale = Vector3.one;

            Text txt = _tipDisplay.GetComponentInChildren<Text>();
            if (txt) txt.text = tip;
            var txtPro = _tipDisplay.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            if (txtPro) txtPro.text = tip;
        }

        public void Start()
        {
            if (showOnStart)
            {
                Show();
                Invoke("Hide", 10f);
            }
        }
    }
}
