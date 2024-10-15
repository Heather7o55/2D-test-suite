using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerashake : MonoBehaviour
{
    public Transform camera;
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = camera.localPosition;
        float elapsedTime = 0f;
        while(elapsedTime < duration)
        {
            float xOffset = Random.Range(-0.5f, 0.5f) * magnitude;
            float yOffset = Random.Range(-0.5f, 0.5f) * magnitude;
            
            camera.localPosition = new Vector3((camera.localPosition.x + xOffset), (camera.localPosition.y + yOffset), originalPos.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        camera.localPosition = originalPos;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        StartCoroutine(Shake(0.2f, 0.2f));
    }
    // Start is called before the first frame update
}
