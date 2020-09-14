using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject hand;
    public GameObject orb;
    private bool isPunching = false;
    float orbTimer = 0;

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

    RaycastHit CheckEnemies()
    {
        RaycastHit res;
        Physics.SphereCast(hand.transform.position, 1, hand.transform.forward, out res, 5);
        return res;
    }

    void Punch()
    {
        //Scan for enemies; current max distance is 7
        RaycastHit hitData = CheckEnemies();
        // if collider is sphere, destroy
        if (hitData.collider != null)
        {
            if (hitData.collider.gameObject.tag.Equals("BODY")) {
                // bounce back
                GameObject enemy = hitData.collider.gameObject;
                var magnitude = 1500;
                var force = enemy.transform.position - transform.position;
                force.Normalize();
                enemy.GetComponent<Rigidbody>().AddForce(force * magnitude);
                enemy.GetComponent<EnemyScript>().enabled = false;
            } else {
                Destroy(hitData.collider.gameObject);
                Debug.Log("DESTROYED " + hitData.collider.gameObject.tag);
            }
        }
        
        //Move the hand
        hand.GetComponent<MeshRenderer>().enabled = true;
        StartCoroutine(PunchAnim());
    }

    void Scare()
    {
        orb.SetActive(true);
        orbTimer = 2.0f;
        foreach(ParticleSystem particle in orb.GetComponentsInChildren<ParticleSystem>())
        {
            particle.time = 2;
        }
        RaycastHit hitData = CheckEnemies();
        if (hitData.collider != null)
        {
            if (hitData.collider.gameObject.tag.Equals("HEART")) {
                // bounce back
                GameObject enemy = hitData.collider.gameObject;
                var magnitude = 1500;
                var force = enemy.transform.position - transform.position;
                force.Normalize();
                enemy.GetComponent<Rigidbody>().AddForce(force * magnitude);
                enemy.GetComponent<EnemyScript>().enabled = false;
            } else {
                Destroy(hitData.collider.gameObject);
                Debug.Log("DESTROYED " + hitData.collider.gameObject.tag);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        Debug.DrawLine(hand.transform.position, hand.transform.forward * 5 +  hand.transform.position, Color.red);
        if(Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("killing people is bad");
        } else if (Input.GetKeyDown(KeyCode.D) && !isPunching)
        {
            Punch();
        } else if (Input.GetKeyDown(KeyCode.A))
        {
            Scare();
        }
        if (orbTimer > 0) orbTimer -= Time.deltaTime;
        else orb.SetActive(false);
    }
}
