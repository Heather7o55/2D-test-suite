using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool instakill = false;
    void OnTriggerStay2D(Collider2D collision)
    {
        if(instakill) collision.gameObject.GetComponent<BaseEntity>().Die();
        else collision.gameObject.GetComponent<BaseEntity>().ModifyHealth(-2);
        
    }
}
