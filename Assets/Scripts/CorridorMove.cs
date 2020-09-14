using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < 50.0f) {
            transform.Translate(Vector3.forward * -Time.deltaTime * 5);
            // Debug.Log("Time: " + Time.time);
        }
    }
}
