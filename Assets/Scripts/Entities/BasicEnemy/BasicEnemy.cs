using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : BaseEntity
{
    Renderer self;
    public GameObject[] tmp;
    NavMeshAgent agent;
    GameObject player;
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        tmp = GameObject.FindGameObjectsWithTag("Goal");
        maxHealth = 3;
        Setup();
        self = GetComponent<Renderer>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        player = GameObject.FindWithTag("Player");
        direction = tmp[Random.Range(0,tmp.Length)].transform.position;
        direction = new Vector3(UnityEngine.Random.Range((float)(direction.x - 1.3), (float)(direction.x + 1.3)), direction.y, direction.z);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player")) col.gameObject.GetComponent<BaseEntity>()?.ModifyHealth(-1);
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(direction);
    }
    bool canSeePlayer()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
        if(ray.collider == null) return false;
        else return ray.collider.CompareTag("Player");
    }
}
