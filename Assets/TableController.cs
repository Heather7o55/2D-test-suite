using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TableController : MonoBehaviour
{
    // Start is called before the first frame update
    private bool timerexpired = false;
    void OnTriggerStay2D(Collider2D collision)
    {
        if(timerexpired) 
            collision.transform.position =  new Vector3(collision.transform.position.x,collision.transform.position.y+0.05f, collision.transform.position.z);
            StartCoroutine(CoolDown(0.1f));
    }
     IEnumerator CoolDown(float timer)
    {
        Debug.Log("cooldown active");
        timerexpired = false;
        yield return new WaitForSeconds(timer);
        timerexpired = true;
    }
}
