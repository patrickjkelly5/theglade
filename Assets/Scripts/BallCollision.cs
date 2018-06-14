using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallCollision : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Pickup"))
        {
            GameObject spider = other.gameObject;
         //   GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>().inflictDamage(spider, gameObject);
        }
    }


}