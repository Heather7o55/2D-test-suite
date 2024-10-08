using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public bool hasAudio;
    public string tag;
    bool playedaudio = false;
    AudioSource audiosource;
    public int Health;
    // Start is called before the first frame update
    void Start()
    {
        if(hasAudio == true)
        {
            audiosource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        Debug.Log(audiosource.time);
        if(hasAudio == true)
        {
            if(audiosource.time >= 1.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if(collision2D.gameObject.CompareTag(tag))
        {
            Health--;
        }
        if(Health <= 1)
        {
            if(hasAudio == true)
            {
                if(playedaudio == false)
                {
                    audiosource.Play();
                    playedaudio = true;
                } 
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}