using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winbox : MonoBehaviour
{
    public UIManager uIManager;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            uIManager.Win();
        }
    }
}
