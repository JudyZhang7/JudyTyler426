using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 3.0f;
    private Vector3 targetPosition;
    
    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = new Vector3(0, 1, -10);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad < 10.0f) {
            speed = 3.0f;
        } else if (Time.timeSinceLevelLoad < 30.0f) {
            speed = 5.0f;
        } else {
            speed = 10.0f;
        }
        float step =  speed * Time.deltaTime; // calculate distance to move
        // timer -= timer * Time.deltaTime;
        // Leave some space between
        if (Vector3.Distance(transform.position, targetPosition) > 2.0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
    }
}
