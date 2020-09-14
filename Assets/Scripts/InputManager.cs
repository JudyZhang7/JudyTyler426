using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public GameObject hand;
    public GameObject orb;
    public GameObject explosionPrefab;
    public GameObject endScreen;

    private bool isPunching = false;
    float orbTimer = 0;
    public AudioSource punch;
    public AudioSource scare;
    public AudioSource shriek;
    public AudioSource grunt;
    private int magnitude = 2500;
    public float dangerMaxTime = 3.5f;
    private float dangerTime;
    public Light backgroundLight;
    public Color safeColor = Color.white;
    public Color dangerColor = Color.black;
    public Color overColor = Color.red;
    public static int numSpared = 0; // counter for saved enemies
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        dangerTime = dangerMaxTime;
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
        punch.Play();
        // if collider is sphere, destroy
        if (hitData.collider != null)
        {
            // hitData.collider.enabled = false;
            if (hitData.collider.gameObject.tag.Equals("BODY")) {
                // bounce back
                GameObject enemy = hitData.collider.gameObject;
                var force = enemy.transform.position - transform.position;
                force.Normalize();
                enemy.GetComponent<Rigidbody>().AddForce(force * magnitude);
                enemy.GetComponent<EnemyScript>().enabled = false;
                enemy.transform.localScale -= new Vector3(1, 1, 1);
                numSpared += 1;
            } else {
                Instantiate(explosionPrefab, hitData.collider.gameObject.transform.position, Quaternion.identity);
                Destroy(hitData.collider.gameObject);
                GetComponent<AudioSource>().Play();
                shriek.Play();
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
        orbTimer = 0.5f;
        scare.Play();
        RaycastHit hitData = CheckEnemies();
        if (hitData.collider != null)
        {
            // hitData.collider.enabled = false;
            if (hitData.collider.gameObject.tag.Equals("HEART")) {
                // bounce back
                GameObject enemy = hitData.collider.gameObject;
                var force = enemy.transform.position - transform.position;
                force.Normalize();
                enemy.GetComponent<Rigidbody>().AddForce(force * magnitude);
                enemy.GetComponent<EnemyScript>().enabled = false;
                enemy.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
                numSpared += 1;
            } else {
                Instantiate(explosionPrefab, hitData.collider.gameObject.transform.position, Quaternion.identity);
                Destroy(hitData.collider.gameObject);
                GetComponent<AudioSource>().Play();
                grunt.Play();
                Debug.Log("DESTROYED " + hitData.collider.gameObject.tag);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        Debug.DrawLine(hand.transform.position, hand.transform.forward * 5 +  hand.transform.position, Color.red);
        if (Input.GetKeyDown(KeyCode.D) && !isPunching)
        {
            Punch();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Scare();
        }
        
        backgroundLight.color = Color.Lerp(safeColor, dangerColor, 0.5f);

        RaycastHit hitData = CheckEnemies();
        if (hitData.collider != null)
        {
            dangerTime -= Time.deltaTime;
            float endLerp = 1 - (dangerTime / dangerMaxTime);
            if (dangerTime <= 2) {
                backgroundLight.color = Color.Lerp(dangerColor, overColor, 0.5f);
            }
            if (dangerTime <= 0)
            {
                if (!endScreen.activeSelf)
                {
                    endScreen.GetComponent<EndScreen>().color = new Vector4(0, 0, 0, 0);
                    endScreen.SetActive(true);
                }
            }
        } else {
            dangerTime = dangerMaxTime;
        }
        if (orbTimer > 0) orbTimer -= Time.deltaTime;
        else orb.SetActive(false);

        // update slider
        //Debug.Log("NUM SPARED: " + numSpared);
        // slider.value = numSpared/30.0f;
        slider.value = Time.time/50.0f;
    }
}
