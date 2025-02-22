using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public static bool tablesActive = false;
    public static int money = 0;
    GameObject table;
    void Sart()
    {
        table = GameObject.FindWithTag("Table");
    }
    void Update()
    {
        table.SetActive(tablesActive);
    }

}
