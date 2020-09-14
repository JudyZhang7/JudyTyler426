using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public Vector4 color;
    SpriteRenderer spriteRenderer;
    float timer = -1;

    // Start is called before the first frame update
    void Start()
    {
        color.w = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = color;
    }

    public void StartTimer()
    {
        timer = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (timer >= 0) timer += Time.deltaTime;
        if (color.w < 1)
        {
            color.w += Time.deltaTime*0.5f;
            spriteRenderer.color = color;
        }
        else if (timer > 5) SceneManager.LoadScene(0);
        //if (timer > 3) SceneManager.LoadScene(0);
    }
}
