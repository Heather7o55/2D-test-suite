using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Rigidbody2D))]         //Tell Unity to add theses components to the gameobject this code is attached to.
[RequireComponent(typeof(BoxCollider2D))]       //We will still need to tweak some of the settings.
public class Moveable : MonoBehaviour
{
    float coolDown = 0.4f;
    
    public float slidespeed = 500f;
    public bool is_sliding = false;
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
        float moveInputX = Input.GetAxisRaw("Horizontal"); // For horizontal movement (left/right)
        float moveInputY = Input.GetAxisRaw("Vertical");   // For vertical movement (up/down)

        // Normalise the vector so that we don't move faster when moving diagonally.
        Vector2 moveDirection = new Vector2(moveInputX, moveInputY);
        moveDirection.Normalize();

        
        // Assign velocity directly to the Rigidbody
        if(sliding() == true &&  is_sliding == false)
            {
                
                slide();
                camera.fieldOfView = 162;
            }
        if(sprinting() == true)
        {
            if(is_sliding == false)
            {
                rb2d.velocity = moveDirection * moveSpeed * sprintModifer;
                camera.fieldOfView = 167;
            }
            
        }
        else if(is_sliding == false)
        {
            camera.fieldOfView = 164;
            rb2d.velocity = moveDirection * moveSpeed;
        }
        if(Input.GetJoystickNames().Length > 0)
        {
            
        }
        if(Input.GetKey(KeyCode.LeftControl) == false)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
            Vector2 direction = mousePosition - transform.position;
            float angle = Vector2.SignedAngle(Vector2.right, direction);
            rb2d.MoveRotation(angle);
        }
        
    }
    static bool sprinting()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.JoystickButton1))
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
        if(Input.GetKeyDown(KeyCode.LeftControl))
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
        is_sliding = true;
        if(Input.GetAxisRaw("Horizontal") > 0)
        {
            rb2d.AddForce(Vector2.right * slidespeed);
        }
        if(Input.GetAxisRaw("Horizontal") < 0)
        {
            rb2d.AddForce(Vector2.left * slidespeed);
        }
        if(Input.GetAxisRaw("Vertical") > 0)
        {
            rb2d.AddForce(Vector2.up * slidespeed);
        }
        if(Input.GetAxisRaw("Vertical") < 0)
        {
            rb2d.AddForce(Vector2.down * slidespeed);
        }
        StartCoroutine("stopSlide");
    }
    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(coolDown);
        is_sliding = false;
    }
    IEnumerator startCooldown()
    {
        Debug.Log("cooldown on");
        yield return new WaitForSeconds(1f + coolDown);
        is_sliding = false;
    }
}
