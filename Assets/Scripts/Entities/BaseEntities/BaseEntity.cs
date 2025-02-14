using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]         
[RequireComponent(typeof(BoxCollider2D))]  
public class BaseEntity : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public Rigidbody2D selfRigidBody;
    // Start is called before the first frame update
    public void Setup()
    {
        // Debug.Log(gameObject.name);
        currentHealth = maxHealth;
        selfRigidBody = GetComponent<Rigidbody2D>();
    }
    // ModifyHeath allows you to increase or decrease heath in one function by sending a positive or negative value
    public void ModifyHealth(int modifier)
    {
        currentHealth += modifier;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        if(currentHealth <= 0) Die();
    }
    // ModifyMaxHeath allows you to increase or decrease "maxhealth" in one function by sending a positive or negative value
    public void ModifyMaxHealth(int modifier)
    {
        maxHealth += modifier;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }
    public void Die()
    {
        // Debug.Log(gameObject.name + " died");
        Destroy(gameObject);
    }
}
