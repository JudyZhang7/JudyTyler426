using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject strongHeartPrefab;
    public GameObject strongBodPrefab;
    public GameObject player;
    private float xPos;
    private float zPos;
    private float spawnTime;
    // environment updates
    public Slider slider;
    public GameObject skyDome;
    private Renderer rend;
    private float domeTime;
    private float startDomeTime = 0.0f; //6.66

    // Start is called before the first frame update
    void Start()
    {
        xPos = -0.2f;
        zPos = 3.0f;
        spawnTime = 2.0f;
        StartCoroutine(EnemyDrop());
        rend = skyDome.GetComponent<Renderer>();
        domeTime = startDomeTime;
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
            // Debug.Log(player_xPos + " " + player_zPos);
            
            if (Random.Range(0, 2) == 0) {
                Instantiate(strongHeartPrefab, new Vector3(xPos, 2, zPos), Quaternion.identity);
            } else {
                Instantiate(strongBodPrefab, new Vector3(xPos, 2, zPos), Quaternion.identity);
            }
            if (Time.time < 10.0f) {

            } else if (Time.time < 30.0f) {
                spawnTime = 1.5f;
            } else {
                spawnTime = 1.0f;
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }

    void Update () {
        // skydome
        float offset = domeTime * 0.03f * 0.5f;
        domeTime += Time.deltaTime;
        if (offset < 0.43){
            rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }
        //Displays the value of the slider in the console.
        slider.value = Time.time/50.0f;
    }
}
