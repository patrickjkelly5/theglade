

using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.7f;
    public int attackDamage = 10;
    public bool isDead;
    public bool isSinking;
    public bool isRising;
    public float sinkSpeed = 2.5f;
    public bool hasToken;
    public bool killedByMelee;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    public bool playerInRange;
    public AudioSource spiderHiss;
    public AudioSource spiderClick;

    float timeBetweenSounds = 4.0f;
    float lastSound;

    public AudioClip deathClip;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");

        playerHealth = player.GetComponent <PlayerHealth> ();

        anim = GetComponent <Animator> ();

        spiderHiss = GetComponents<AudioSource>()[0];
        spiderClick = GetComponents<AudioSource>()[1];

        lastSound = Time.time;

        spiderHiss.Play();

    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
            anim.SetTrigger("InRange");
            spiderHiss.Play();
        }
    }

    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false; // I hid this to prevent the distance bug
            anim.SetTrigger("PlayerOutOfRange"); // I removed this so that the spider can still attack even if out of range
        }
    }

    void Update ()
    {

        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead");
        }

        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }

        if (isRising)
        {

            if (transform.position.y < 0)
            {
                transform.Translate(Vector3.up * sinkSpeed * Time.deltaTime);
            } else
            {
                isRising = false;
                GetComponent<EnemyMovement>().enabled = true;
                GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
                GetComponent<Rigidbody>().isKinematic = false;
                anim.SetTrigger("Restart");
            }
        }
    }

    public void StartSinking()
    {   
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        spiderHiss.Stop();
        spiderClick.Stop();

        Destroy(gameObject, 2f);
    }

    void Attack ()
    {
        if(playerHealth.currentHealth > 0)
        {

            if (Time.time - lastSound >= timeBetweenSounds)
            {
                if (Random.Range(1, 10) > 7) spiderHiss.Play();

                lastSound = Time.time;
            }

            playerHealth.TakeDamage (attackDamage);
        }
    }
}
