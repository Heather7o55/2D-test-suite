using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]         
[RequireComponent(typeof(BoxCollider2D))]  
public class BaseEntity : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public Rigidbody2D selfRidgidBody;
    // Start is called before the first frame update
    public void Setup()
    {
        Debug.Log(gameObject.name);
        currentHealth = maxHealth;
        selfRidgidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        Debug.Log(gameObject.name + " took " + damage + " damage");
        currentHealth -= damage;
        if((float)currentHealth <= (float)maxHealth * 0.25f) {Debug.LogWarning(gameObject.name + "Is low on health");}
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    public void GainHealth(int modifer)
    {
        currentHealth += modifer;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    public void IncreaseMaxHealth(int modifer)
    {
        maxHealth = maxHealth + modifer;
    }
    public void Die()
    {
        Debug.Log(gameObject.name + " died");
        Destroy(gameObject);
    }
}
