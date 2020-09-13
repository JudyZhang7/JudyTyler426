using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject hand;
    private bool isPunching = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void MoveCamera()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        if (mousePos.x > Screen.width*7/8) mousePos.x = Screen.width*7/8;
        if (mousePos.x < Screen.width/8) mousePos.x = Screen.width / 8;

        Vector3 inputPos = Camera.main.ScreenToWorldPoint(mousePos);
        inputPos.y = Camera.main.transform.position.y;
        //if (inputPos.x > 10) inputPos.x = 10;
        //if (inputPos.x < -10) inputPos.x = -10;
        Camera.main.transform.LookAt(inputPos - Camera.main.transform.position);
        hand.transform.LookAt(inputPos - Camera.main.transform.position + hand.transform.position);
        Debug.DrawLine(transform.position, hand.transform.forward * 5 + transform.position, Color.red);
    }

    IEnumerator PunchAnim()
    {
        isPunching = true;
        float timer = 0.0f;
        Vector3 target;
        while (timer <= 0.2f)
        {
            target = hand.transform.forward * 3f + transform.position;

            if (timer < 0.1f)
            {
                hand.transform.position = Vector3.Lerp(transform.position, target, timer/0.1f);
            }
            else 
            {
                hand.transform.position = Vector3.Lerp(target,transform.position, (timer - 0.1f)/0.1f);
            } 
            timer += Time.deltaTime;
            yield return null;
        }
        hand.GetComponent<MeshRenderer>().enabled = false;
        isPunching = false;
    }
    void Punch()
    {
        //Scan for enemies
        //Move the hand
        hand.GetComponent<MeshRenderer>().enabled = true;
        StartCoroutine(PunchAnim());
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();

        if(Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("killing people is bad");
        } else if (Input.GetKeyDown(KeyCode.D) && !isPunching)
        {
            Punch();
        } else if (Input.GetKeyDown(KeyCode.A))
        {

        }
    }
}
