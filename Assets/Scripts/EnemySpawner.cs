using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject strongHeart;
    public GameObject strongBod;
    public GameObject player;
    public float xPos;
    public float zPos;
    public static int enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        xPos = -0.2f;
        zPos = 3.0f;
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop() {
        while(enemyCount < 50) {
            float player_xPos = player.transform.position.x;
            float player_zPos = player.transform.position.z;
            xPos = Random.Range(-6, 6);
            zPos = Random.Range(player_zPos +10, player_zPos + 20);
            Debug.Log(player_xPos + " " + player_zPos);
            
            if (Random.Range(0, 2) == 0) {
                Instantiate(strongHeart, new Vector3(xPos, 2, zPos), Quaternion.identity);
            } else {
                Instantiate(strongBod, new Vector3(xPos, 2, zPos), Quaternion.identity);
            }
            yield return new WaitForSeconds(1.0f);
            enemyCount += 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
