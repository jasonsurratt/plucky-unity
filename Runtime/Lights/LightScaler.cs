using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plucky.Unity
{
    /// <summary>
    /// Light scaler changes the range of a light as the scale changes.
    /// </summary>
    public class LightScaler : MonoBehaviour
    {
        [Tooltip("The target for flicker, if null GetComponent<Light> will be used.")]
        public Light target;
        [Tooltip("How frequently should the lighting change in seconds")]
        public float repeatRate = 0.1f;
        [Tooltip("The max intensity for the light, if < 0 this will be 1.01 * target.intensity")]
        public float maxIntensity = -1;
        [Tooltip("The min intensity for the light, if < 0 this will be 0.99 * target.intensity")]
        public float minIntensity = -1;
        [Tooltip("The max range for the light, if < 0 this will be 1.01 * target.range")]
        public float maxRange = -1;
        [Tooltip("The min range for the light, if < 0 this will be 0.99 * target.range")]
        public float minRange = -1;

        [Tooltip("The lower bound when moving the light")]
        public Vector3 minOffset = Vector3.one * 0.1f;
        [Tooltip("The upper bound when moving the light")]
        public Vector3 maxOffset = Vector3.one * -0.1f;

        // The starting near plane is stored so it can be scaled with the light
        float startNearPlane;
        Vector3 startLocal;

        // Start is called before the first frame update
        protected void Awake()
        {
            if (target == null)
            {
                target = GetComponent<Light>();
            }

            if (minRange < 0) minRange = target.range * 0.99f;
            if (maxRange < 0) maxRange = target.range * 1.01f;
            if (minIntensity < 0) minIntensity = target.intensity * 0.99f;
            if (maxIntensity < 0) maxIntensity = target.intensity * 1.01f;

            InvokeRepeating(nameof(Flicker), 0, repeatRate);

            startNearPlane = target.shadowNearPlane;
            startLocal = transform.localPosition;
        }

        public void Flicker()
        {
            float scale = transform.lossyScale.x;

            float t = Random.Range(0f, 1f);
            target.intensity = Mathf.Lerp(minIntensity, maxIntensity, t) * Mathf.Sqrt(scale);
            target.range = Mathf.Lerp(minRange, maxRange, t) * scale;
            target.shadowNearPlane = startNearPlane * scale;

            transform.localPosition = startLocal + new Vector3(
                Random.Range(minOffset.x, maxOffset.x),
                Random.Range(minOffset.y, maxOffset.y),
                Random.Range(minOffset.z, maxOffset.z));
        }
    }
}
