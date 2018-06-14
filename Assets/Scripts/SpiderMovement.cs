

using UnityEngine;
using System.Collections;

public class SpiderMovement : MonoBehaviour
{
    public Transform player;
    //EnemyAttack enemyIsDead;
    UnityEngine.AI.NavMeshAgent nav;

    void Awake ()
    {
        //player = GameObject.FindGameObjectWithTag ("Player").transform;
        //enemyIsDead = GetComponent <EnemyAttack> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }

    void Update ()
    {

        if (true){
                // PLAYER IS NOT IN ENEMY'S RANGE
                nav.enabled = true;
                nav.SetDestination(player.transform.position);
            } else {
                // PLAYER IS IN ENEMY'S RANGE
                nav.enabled = false;
            }

        // if (!enemyIsDead.isDead && !player.GetComponent<PlayerHealth>().isDead)
        // {
            
        //   // ENEMY IS ALIVE

        //     if (!enemyIsDead.playerInRange)
        //     {
        //         // PLAYER IS NOT IN ENEMY'S RANGE
        //         nav.enabled = true;
        //         nav.SetDestination(player.transform.position);
        //     }
        //     else
        //     {
        //         // PLAYER IS IN ENEMY'S RANGE
        //         nav.enabled = false;
        //     }

        // }
        // else
        // {

        //     // ENEMY IS DEAD

        //     nav.enabled = false;
        // }
    }
}
