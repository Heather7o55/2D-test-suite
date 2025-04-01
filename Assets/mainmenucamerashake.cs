using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainmenucamerashake : MonoBehaviour
{
    public CameraController cameraController;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(cameraShake());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator cameraShake()
    {
        while(true)
        {
            cameraController.CameraShake(0.5f, 1f);
            yield return new WaitForSeconds(1f);
        }
    }
}
