using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTorsoRotation : MonoBehaviour
{
    GameObject hand;
    // Start is called before the first frame update
    void Start()
    {
        hand = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(!UIManager.isPaused) UpdatePlayerRotation();
    }
    private void UpdatePlayerRotation()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
        Vector2 direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        hand.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
