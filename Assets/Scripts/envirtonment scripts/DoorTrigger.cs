using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject hiddenDoor;
    private Renderer[] renderers;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        ChangeColor(new Color (1, 0, 0, 1));
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player") || collider.CompareTag("puzzlebox"))
        {
            ChangeColor(new Color (0.5f, 0, 0, 1));
            hiddenDoor.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.CompareTag("Player") || collider.CompareTag("puzzlebox"))
        {
            ChangeColor(new Color (1, 0, 0, 1));
            hiddenDoor.SetActive(true);
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
