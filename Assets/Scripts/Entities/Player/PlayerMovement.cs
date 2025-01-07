using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;      
public class PlayerMovement : BaseEntity
{
    float slideCoolDown = 0.4f;
    bool isSlideCooldown = false;

    public float slidespeed = 500f;
    public bool isSliding = false;

    GameObject hand;

    public float moveSpeed = 10f;
    public float sprintModifer = 1.5f;

    private CameraController cameraScript;

    void Start()
    {
        maxHealth = 100;
        Setup();
        cameraScript = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
        hand = transform.GetChild(0).gameObject;
    }
    void Update()
    {
        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDirection.Normalize();
        if((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.Space)) && !isSliding && !isSlideCooldown)
        {
            slide();
        }
        else if(Input.GetKey(KeyCode.LeftShift) && !isSliding)
        {
            selfRidgidBody.velocity = moveDirection * moveSpeed * sprintModifer;
        }
        else if(!isSliding)
        {
            selfRidgidBody.velocity = moveDirection * moveSpeed;
        }
        updateCamera();
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
        Vector2 direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        hand.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        selfRidgidBody.MoveRotation(angle);
    }
    private void slide()
    {
        isSlideCooldown = true;
        isSliding = true;
        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));   // For vertical movement (up/down)
        moveDirection.Normalize();
        selfRidgidBody.AddForce(moveDirection * slidespeed);
        StartCoroutine("stopSlide");
    }
    private void updateCamera()
    {
        if(isSliding)
        {
            cameraScript.ZoomCamera(60f, 15f);
        }
        else if(Input.GetKey(KeyCode.LeftShift) && !new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).Equals(new Vector2(0,0)))
        {
            cameraScript.ZoomCamera(66f, 15f);
        }
        else 
        {
            cameraScript.ZoomCamera(64f, 15f);
        }
    }
    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(slideCoolDown);
        isSliding = false;
        StartCoroutine("startCooldown");
    }
    IEnumerator startCooldown()
    {
        Debug.Log("cooldown on");
        yield return new WaitForSeconds(slideCoolDown);
        isSlideCooldown = false;
    }
}