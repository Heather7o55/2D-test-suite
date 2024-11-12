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
    private int camerafov = 164;
    Rigidbody2D rb2d;
    public float moveSpeed = 10f;
    public float sprintModifer = 1.5f;
    private SpriteRenderer sprite;
    public Sprite[] sprites;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        camera.fieldOfView = camerafov;
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDirection.Normalize();
        // Assign velocity directly to the Rigidbody
        if(isSliding == true)
        {
            ZoomCamera(160, 35);
            sprite.sprite = sprites[1];
        }
        if(sliding() == true && isSliding == false && isCooldown == false)
        {
            slide();
            
        }
        else if(sprinting() == true && isSliding == false)
        {
            
            rb2d.velocity = moveDirection * moveSpeed * sprintModifer;
            ZoomCamera(165, 15);
            sprite.sprite = sprites[0];
        }
        else if(isSliding == false)
        {
            
            ZoomCamera(164, 15);
            rb2d.velocity = moveDirection * moveSpeed;
            sprite.sprite = sprites[0];
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
    void ZoomCamera(float target, float zoomSpeed)
    {
        camera.fieldOfView = Mathf.MoveTowards(camera.fieldOfView, target, zoomSpeed * Time.deltaTime);
    }
}
