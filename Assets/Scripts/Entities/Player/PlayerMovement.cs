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
    public float moveSpeed = 10f;
    public float sprintModifer = 1.5f;
    private CameraController cameraScript;
    private Vector2 moveDirection;
    void Start()
    {
        maxHealth = 100;
        Setup();
        cameraScript = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
    }
    void Update()
    {
        // Debug.Log(gameObject.name + "'s velocity is " + selfRidgidBody.velocity);
        // Debug.Log("The current <b>timestamp</b> is " + Time.time);
        if(UIManager.isPaused) return;
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
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
    }
    
    private void slide()
    {
        isSlideCooldown = true;
        isSliding = true;
        moveDirection.Normalize();
        selfRidgidBody.AddForce(moveDirection * slidespeed);
        StartCoroutine("stopSlide");
    }
    private void updateCamera()
    {
        if(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).Equals(new Vector2(0,0)))
        {
            cameraScript.ZoomCamera(64f, 15f);
        }
        else if(isSliding)
        {
            cameraScript.ZoomCamera(60f, 15f);
        }
        else if(Input.GetKey(KeyCode.LeftShift) )
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