using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void MoveCamera()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        Vector3 inputPos = Camera.main.ScreenToWorldPoint(mousePos);
        Camera.main.transform.LookAt(inputPos - Camera.main.transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        MoveCamera();   
    }
}
