using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public PlayerShooting.Gun localGun;
    // Start is called before the first frame update
     void OnTriggerEnter2D(Collider2D collider)
     {
        if(collider.CompareTag("Player"))
        {
            PlayerShooting.activeGun = localGun;
            Destroy(gameObject);
        }
     }
}
