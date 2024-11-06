using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Rigidbody2D))]         //Tell Unity to add theses components to the gameobject this code is attached to.
[RequireComponent(typeof(BoxCollider2D))]       //We will still need to tweak some of the settings.
public class PlayerMovement : MonoBehaviour
{
    float coolDown = 0.4f;
    bool isCooldown = false;
    
    public float slidespeed = 500f;
    public bool isSliding = false;
    public Camera camera;
    Rigidbody2D rb2d;
    public float moveSpeed = 5f;
    public float sprintModifer = 2.5f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        camera.fieldOfView = 164;
    }

    void Update()
    {
        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDirection.Normalize();
        // Assign velocity directly to the Rigidbody
        if(sliding() == true && isSliding == false && isCooldown == false)
        {
            slide();
            camera.fieldOfView = 162;
        }
        else if(sprinting() == true && isSliding == false)
        {
            
            rb2d.velocity = moveDirection * moveSpeed * sprintModifer;
            camera.fieldOfView = 167;
        }
        else if(isSliding == false)
        {
            camera.fieldOfView = 164;
            rb2d.velocity = moveDirection * moveSpeed;
        }
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
        Vector2 direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        rb2d.MoveRotation(angle);
    }
    static bool sprinting()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    static bool sliding()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void slide()
    {
        isCooldown = true;
        isSliding = true;
        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));   // For vertical movement (up/down)
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
