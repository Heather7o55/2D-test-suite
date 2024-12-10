using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Rigidbody2D))]         
[RequireComponent(typeof(BoxCollider2D))]       
public class PlayerMovement : MonoBehaviour
{
    float coolDown = 0.4f;
    bool isCooldown = false;
    public float slidespeed = 500f;
    public bool isSliding = false;
    Rigidbody2D rb2d;
    public float moveSpeed = 10f;
    public float sprintModifer = 1.5f;
    private GameObject cameracontrol;
    private CameraController cameraScript;
    private Camera CameraSettings;
    void Start()
    {
        cameracontrol = GameObject.FindWithTag("MainCamera");
        CameraSettings = cameracontrol.GetComponent<Camera>();
        cameraScript = cameracontrol.GetComponent<CameraController>();
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDirection.Normalize();
        if(Input.GetKey(KeyCode.LeftControl) && !isSliding && !isCooldown)
        {
            
            slide();
        }
        else if(Input.GetKey(KeyCode.LeftShift) && !isSliding)
        {
            rb2d.velocity = moveDirection * moveSpeed * sprintModifer;
            if(!moveDirection.Equals(new Vector2(0,0)))
            {
                cameraScript.ZoomCamera(65, 15);
            }
        }
        else if(!isSliding)
        {
            cameraScript.ZoomCamera(64, 15);
            rb2d.velocity = moveDirection * moveSpeed;
        }
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = CameraSettings.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
        Vector2 direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        rb2d.MoveRotation(angle);
    }
    private void slide()
    {
        isCooldown = true;
        cameraScript.ZoomCamera(60, 15);
        isSliding = true;
        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));   // For vertical movement (up/down)
        moveDirection.Normalize();
        rb2d.AddForce(moveDirection * slidespeed);
        StartCoroutine("stopSlide");
    }
    IEnumerator stopSlide()
    {
        
        yield return new WaitForSeconds(coolDown);
        isSliding = false;
        StartCoroutine("startCooldown");
    }
    IEnumerator startCooldown()
    {
        Debug.Log("cooldown on");
        yield return new WaitForSeconds(coolDown);
        isCooldown = false;
    }
}
