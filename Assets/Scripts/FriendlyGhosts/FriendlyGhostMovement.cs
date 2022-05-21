using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FriendlyGhostMovement : MonoBehaviour
{
    GameObject player;
    NavMeshAgent agent;
    Animator anim;
    public float distanceToPlayer;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();    
        agent.enabled = false;
    }

    void Update()
    {
        if (player == null || !agent.enabled) return;
        
        agent.SetDestination(player.transform.position);
        agent.stoppingDistance = distanceToPlayer;
        Animating();
    }
    void Animating()
    {
        if (agent.velocity.magnitude != 0) anim.SetBool("isWalking", true);
        else anim.SetBool("isWalking", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            agent.enabled = true;
        }
    }
}
