using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorMove : MonoBehaviour
{
    public GameObject endScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad < 50.0f) {
            transform.Translate(Vector3.forward * -Time.deltaTime * 5);
            // Debug.Log("Time: " + Time.time);
        } else
        {
            if(endScreen != null && !endScreen.activeSelf)
            {
                if(InputManager.numSpared >= 30) endScreen.GetComponent<EndScreen>().color = new Vector4(255, 255, 255, 0);
                else endScreen.GetComponent<EndScreen>().color = new Vector4(0, 0, 0, 0);
                endScreen.SetActive(true);
            }
        }
    }
}
