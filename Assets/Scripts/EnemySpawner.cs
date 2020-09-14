using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject strongHeartPrefab;
    public GameObject strongBodPrefab;
    public GameObject player;
    private float xPos;
    private float zPos;
    private float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        xPos = -0.2f;
        zPos = 3.0f;
        spawnTime = 1.5f;
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop() {
        while(Time.time < 50.0f) {
            float player_xPos = player.transform.position.x;
            float player_zPos = player.transform.position.z;
            if (Random.Range(0, 2) == 1) {
                xPos = Random.Range(-12, -4);
            } else {
                xPos = Random.Range(4, 12);
            }
            zPos = Random.Range(player_zPos +20, player_zPos + 30);
            Debug.Log(player_xPos + " " + player_zPos);
            
            if (Random.Range(0, 2) == 0) {
                Instantiate(strongHeartPrefab, new Vector3(xPos, 2, zPos), Quaternion.identity);
            } else {
                Instantiate(strongBodPrefab, new Vector3(xPos, 2, zPos), Quaternion.identity);
            }
            if (Time.time < 10.0f) {

            } else if (Time.time < 30.0f) {
                spawnTime = 1.0f;
            } else {
                spawnTime = 0.5f;
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
