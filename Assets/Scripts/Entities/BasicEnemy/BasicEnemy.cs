using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : BaseEntity
{
    Renderer self;
    Vector3 tmp;
    NavMeshAgent agent;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 3;
        Setup();
        tmp = transform.position;
        self = GetComponent<Renderer>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        player = GameObject.FindWithTag("Player");
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player")) col.gameObject.GetComponent<BaseEntity>()?.ModifyHealth(-1);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        selfRigidBody.rotation = angle;
        if(self.isVisible)
        {
            tmp = player.transform.position;
        }
        agent.SetDestination(tmp);
    }
    bool canSeePlayer()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
        if(ray.collider == null) return false;
        else return ray.collider.CompareTag("Player");
    }
}
