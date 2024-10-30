using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetbutton : MonoBehaviour
{
    // Start is called before the first frame update
    public float timealloted = 5;
    float sec;
    public GameObject hiddenDoor;
    private Renderer[] renderers;

    void Update()
    {
        if(sec <= timealloted)
        { 
            ChangeColor(new Color (0.5f, 0, 0, 1));
            hiddenDoor.SetActive(false);
            sec += 1*Time.deltaTime;
            Debug.Log(sec); 
        }
        else
        {
            ChangeColor(new Color (1, 0, 0, 1));
            hiddenDoor.SetActive(true);
        }
    }
    void Start()
    {
        sec = timealloted;
        renderers = GetComponentsInChildren<Renderer>();
        ChangeColor(new Color (1, 0, 0, 1));
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player") || collider.CompareTag("bullet"))
        {
            sec = 0;
        }
    }

    public void ChangeColor(Color color)
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.material.color = color;
        }
    }

}
