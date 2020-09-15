using UnityEngine;
using System.Collections;
// stolen from https://gist.github.com/ftvs/5822103
public class CameraEffects : MonoBehaviour {

	public Transform camTransform;
	// How long the object should shake for.
	public float shakeDuration = 0.3f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.2f;
	public float decreaseFactor = 1.0f;
	public bool enabled = false;

	Vector3 originalPos;
	
	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}
	
	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	void Update()
	{
        //if (enabled) {
        //    if (shakeDuration > 0)
        //    {
        //        camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
                
        //        shakeDuration -= Time.deltaTime * decreaseFactor;
        //    }
        //    else
        //    {
        //        shakeDuration = 0f;
        //        camTransform.localPosition = originalPos;
        //    }
        //}
	}
}
