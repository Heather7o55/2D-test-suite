using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;      
public class PlayerController : BaseEntity
{
    float slideCoolDown = 0.4f;
    bool isSlideCooldown = false;
    public float slideSpeed = 500f;
    public bool isSliding = false;
    public float moveSpeed = 10f;
    public float sprintModifier = 1.5f;
    private CameraController cameraScript;
    private Vector2 moveDirection;
    private GameObject HUD;
    public Animator anim;
    void Start()
    {
        maxHealth = 10;
        Setup();
        cameraScript = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
        HUD = GameObject.FindWithTag("HUD");
    }
    void Update()
    {
        // Debug.Log(gameObject.name + "'s velocity is " + selfRigidBody.velocity);
        // Debug.Log("The current <b>timestamp</b> is " + Time.time);
        if(UIManager.isPaused) return;
        UpdateMovement();
        UpdateHUD();
        UpdateCamera();
    }
    private void UpdateHUD()
    {
        HUD.GetComponentInChildren<Animator>().SetFloat("Speed", currentHealth / 10f);
    }
    private void UpdateMovement()
    {
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDirection.Normalize();
        if((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.Space)) && !isSliding && !isSlideCooldown)
            slide();
        else if(Input.GetKey(KeyCode.LeftShift) && !isSliding)
            selfRigidBody.velocity = moveDirection * moveSpeed * sprintModifier;
        else if(!isSliding)
            selfRigidBody.velocity = moveDirection * moveSpeed;
    }
    
    private void slide()
    {
        isSlideCooldown = true;
        isSliding = true;
        moveDirection.Normalize();
        selfRigidBody.AddForce(moveDirection * slideSpeed);
        StartCoroutine("stopSlide");
    }
    private void UpdateCamera()
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