using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempPlayer : MonoBehaviour
{
    public Slider slider;
    // control environment
    public GameObject skyDome;
    private Renderer rend;
    private float domeTime;
    private float startDomeTime = 0f; //6.66

    // Start is called before the first frame update
    void Start()
    {
        rend = skyDome.GetComponent<Renderer>();
        domeTime = startDomeTime;
    }

    public float speed = 20f;
 
     void Update () {
         // skydome
        float offset = domeTime * 0.03f * 0.5f;
        domeTime += Time.deltaTime;
        if (offset < 0.43){
            rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        }

         Vector3 pos = transform.position;
 
         if (Input.GetKey ("w")) {
             pos.z += speed * Time.deltaTime;
         }
         if (Input.GetKey ("s")) {
             pos.z -= speed * Time.deltaTime;
         }
         if (Input.GetKey ("d")) {
             pos.x += speed * Time.deltaTime;
         }
         if (Input.GetKey ("a")) {
             pos.x -= speed * Time.deltaTime;
         }
         transform.position = pos;
         //Displays the value of the slider in the console.
        Debug.Log(slider.value);
        slider.value = transform.position.z/250;

     }
}
