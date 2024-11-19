using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    Transform followTarget;
    void Start()
    {
        GameObject playerfollow = GameObject.FindWithTag("Player");
        followTarget = playerfollow.transform;
    }
    public float smoothTime = 0.3f;
    public void CameraShake(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }
    // Start is called before the first frame updatepublic IEnumerator Shake(float duration, float magnitude)
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsedTime = 0f;
        while(elapsedTime < duration)
        {
            float xOffset = Random.Range(-0.5f, 0.5f) * magnitude;
            float yOffset = Random.Range(-0.5f, 0.5f) * magnitude;
            
            transform.localPosition = new Vector3((transform.localPosition.x + xOffset), (transform.localPosition.y + yOffset), originalPos.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
    public void ZoomCamera(float target, float zoomSpeed)
    {
        GetComponent<Camera>().fieldOfView = Mathf.MoveTowards(GetComponent<Camera>().fieldOfView, target, zoomSpeed * Time.deltaTime);
    }
    void LateUpdate()
    {
        Vector3 targetPosition = followTarget.position;
        targetPosition.z = transform.position.z;

        UpdateCameraPosition(targetPosition, smoothTime);
    }
    
    void UpdateCameraPosition(Vector3 Position, float smoothing)
    {
        transform.position = Vector3.SmoothDamp(transform.position, Position, ref velocity, smoothing);
    }   
}
