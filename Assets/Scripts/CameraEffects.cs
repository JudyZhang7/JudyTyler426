using UnityEngine;
using System.Collections;
// stolen from https://gist.github.com/ftvs/5822103
public class CameraEffects : MonoBehaviour {

	// public Transform camTransform;
	// How long the object should shake for.
	// public float shakeDuration = 0.3f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	// public float shakeAmount = 0.2f;
	// public float decreaseFactor = 2.0f;
	// public bool enabled = false;

	// Vector3 originalPos;
	
    // If true, shake with gradually intensify shake
    [System.NonSerialized]
    public bool toShake = false;
    [System.NonSerialized]

    Vector3 originalPos;

    void OnEnable() {
        originalPos = transform.localPosition;
    }

    public void ShakeTimed(float shakeIntensity, float shakeDuration) {
        StartCoroutine(ShakeTimedCoroutine(shakeIntensity, shakeDuration));
    }

    IEnumerator ShakeTimedCoroutine(float shakeIntensity, float shakeDuration) {
        float curTime = 0.0f;
        while (curTime < shakeDuration) {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPos + Random.insideUnitSphere * shakeIntensity, Time.deltaTime * 3);
            curTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
        yield break;
    }

    public void ShakeIntensifying(float minIntensity, float maxIntensity, float timeToMax) {
        toShake = true;
        StartCoroutine(ShakeIntensifyingCoroutine(minIntensity, maxIntensity, timeToMax));
    }

    IEnumerator ShakeIntensifyingCoroutine(float minIntensity, float maxIntensity, float timeToMax) {
        float curTime = 0.0f;
        float curIntensity = minIntensity;
        while (toShake) {
            // intensity approaches maxIntensity as time elapsed approaches timeToMax
            curIntensity = minIntensity + (maxIntensity - minIntensity) * Mathf.Min(1.0f, curTime / timeToMax);
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPos + Random.insideUnitSphere * curIntensity, Time.deltaTime * 3);
            curTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
        yield break;
    }
}
