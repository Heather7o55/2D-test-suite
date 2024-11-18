using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallcamerashake : MonoBehaviour
{
    private GameObject camera;
    private CameraController cameraScript;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindWithTag("MainCamera");
        cameraScript = camera.GetComponent<CameraController>();
    }

    // Update is called once per frame
   void OnColisionEnter2D(Collider2D collider)
   {
        StartCoroutine(cameraScript.Shake(0.2f, 0.2f));
   }
}
