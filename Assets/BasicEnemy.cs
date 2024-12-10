using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    Renderer self;
    Vector3 tmp;
    NavMeshAgent agent;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
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
        if(self.isVisible)
        {
            tmp = player.transform.position;
        }
        agent.SetDestination(tmp);
    }

   
}
