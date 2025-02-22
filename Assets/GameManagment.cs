using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public static bool tablesActive = false;
    public static int money = 0;
    public static float powerUpTimer = 4.5f;
    public static int Difficulty = 10;
    public static bool waveactive = true;
    GameObject tableFliped;
    GameObject table;
    void Start()
    {
        table = GameObject.FindWithTag("Table");
        tableFliped = GameObject.FindWithTag("TableFliped");
    }
    void Update()
    {
        table.SetActive(!tablesActive);
        tableFliped.SetActive(tablesActive);
    }

}
