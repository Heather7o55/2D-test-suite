using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;      
public class PlayerMovement : BaseEntity
{
    float slideCoolDown = 0.4f;
    bool isSlideCooldown = false;
    public float slideSpeed = 500f;
    public bool isSliding = false;
    public float moveSpeed = 10f;
    public float sprintModifier = 1.5f;
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
            slide();
        else if(Input.GetKey(KeyCode.LeftShift) && !isSliding)
            selfRigidBody.velocity = moveDirection * moveSpeed * sprintModifier;
        else if(!isSliding)
            selfRigidBody.velocity = moveDirection * moveSpeed;
        updateCamera();
    }
    
    private void slide()
    {
        isSlideCooldown = true;
        isSliding = true;
        moveDirection.Normalize();
        selfRigidBody.AddForce(moveDirection * slideSpeed);
        StartCoroutine("stopSlide");
    }
    private void updateCamera()
    {
        if(isSliding)
            cameraScript.ZoomCamera(60f, 15f);
        else if(Input.GetKey(KeyCode.LeftShift))
            cameraScript.ZoomCamera(66f, 15f);
        else 
            cameraScript.ZoomCamera(64f, 15f);
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