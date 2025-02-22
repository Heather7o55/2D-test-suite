using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    public static int i = 0;
    public static int totalkills = 0;

    // Start is called before the first frame update
    void Update()
    {
        
        if(i == GameManagement.Difficulty) UIManager.storeActive = true;
        else if(i == GameManagement.Difficulty && UIManager.storeActive == false) ResetInt();
        else UIManager.storeActive = false;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
        {
            UIManager.lost = true;
        }
    }
    public static void ResetInt()
    {
        i = 0;
    }
}
