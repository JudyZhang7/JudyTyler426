using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public Vector4 color;
    SpriteRenderer spriteRenderer;
    public GameObject mainAudio;
    float timer = 0;
    public AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {
        color.w = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = color;
        mainAudio.GetComponent<AudioSource>().Pause();
        if (color.x > 0) GetComponent<AudioSource>().clip = clips[1];
        else GetComponent<AudioSource>().clip = clips[0];
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (color.w < 1)
        {
            color.w += Time.deltaTime*0.75f;
            spriteRenderer.color = color;
        }
        else if (timer > 5) SceneManager.LoadScene(0);
    }
}
