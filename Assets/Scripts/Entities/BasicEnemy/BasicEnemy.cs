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

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        selfRidgidBody.rotation = angle;
        if(self.isVisible)
        {
            tmp = player.transform.position;
        }
        agent.SetDestination(tmp);
    }
}
