using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string sceneName;
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene(sceneName);
    }
}
