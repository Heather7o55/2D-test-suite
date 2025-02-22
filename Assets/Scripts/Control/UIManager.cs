using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject storeUI;
    public GameObject gameOverScreen;
    public GameObject pistol;
    public Transform[] PowerUpspawnPoints;
    public static bool isPaused = false;
    public static bool storeActive = false;
    public static bool lost = false;
    public EndlessSpawner spawner;


    void Update()
    {
        if(lost) gameOverScreen.SetActive(true);
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(storeActive) 
            {
                GameManagement.Difficulty += 5;
                spawner.Spawnwave();
                storeActive = false;
            }
            else if(isPaused) ResumeGame();
            else PauseGame();
        }
        storeUI.SetActive(storeActive);    
    }
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void StoreActive()
    {
        storeActive = storeActive ? false : true;
    }
    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void FlipTables()
    {
        GameManagement.tablesActive = true;
    }
    public void IncreaseTimer()
    {
        GameManagement.powerUpTimer += GameManagement.powerUpTimer * 1.25f;
    }
    public void InstaKill()
    {
        BatController.instakill = true;
    }
    public void SpawnPistol()
    {
        Instantiate(pistol, PowerUpspawnPoints[Random.Range(0, PowerUpspawnPoints.Length)]);
    }
}
