using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    Text text;
    float timer = 0.0f;
    Vector4 target = new Vector4(255, 255, 255, 0);
    Vector4 start = new Vector4(255, 255, 255,1);

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    public void Flash()
    {
        //text.color = start;
        //timer = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(InputManager.numSpared == 1) text.text = InputManager.numSpared + " life spared";
        else text.text = InputManager.numSpared + " lives spared";
        if(timer <= 0.75f && timer > 0.0f)
        {
            text.color = Vector4.Lerp(start, target, 1-timer / 0.75f);
        }
        if(timer > 0.0f) timer -= Time.deltaTime;
    }
}
