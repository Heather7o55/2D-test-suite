using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    Camera cameraSettings;
    private GameObject player;
    Vector3 followTarget;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        followTarget = player.transform.localPosition;
        cameraSettings = GetComponent<Camera>();
    }
    public float smoothTime = 0.3f;
    public void CameraShake(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = followTarget;
        float elapsedTime = 0f;
        while(elapsedTime < duration)
        {
            float xOffset = Random.Range(-0.5f, 0.5f) * magnitude;
            float yOffset = Random.Range(-0.5f, 0.5f) * magnitude;
            
            transform.position = new Vector3((transform.position.x + xOffset), (transform.position.y + yOffset), originalPos.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPos;
    }
    public void ZoomCamera(float target, float zoomSpeed)
    {
        GetComponent<Camera>().fieldOfView = Mathf.MoveTowards(GetComponent<Camera>().fieldOfView, target, zoomSpeed * Time.deltaTime);
    }
    void LateUpdate()
    {
        // if(Input.GetKey("z"))
        // {
        //     Vector3 mousePosition = Input.mousePosition;
        //     mousePosition = cameraSettings.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
        //     followTarget = (mousePosition * 0.8f);
        //     followTarget += player.transform.position;
        //     followTarget.z = transform.position.z;
        //     smoothTime = 0.0f;
        // }
        // else
        // {
        followTarget = player.transform.position;
        followTarget.z = transform.position.z;
        smoothTime = 0.3f;
        // }  

        UpdateCameraPosition(followTarget, smoothTime);
    }
    
    void UpdateCameraPosition(Vector3 Position, float smoothing)
    {
        transform.position = Vector3.SmoothDamp(transform.position, Position, ref velocity, smoothing);
    }   
}
