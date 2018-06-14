using System;
using UnityEngine;
using VRTK;

public class EnemyHealth : MonoBehaviour
{
    public int pointValue = 10;
    public int startingHealth = 100;
    public int currentHealth;
    public Gun primaryGun;
    public Gun secondaryGun;
    public PlayerHealth player;
    Vector3 velocity;
    public GameObject healthBar;
    public GameObject notice;

    void Awake ()
    {
        currentHealth = startingHealth;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Pistol" || other.name == "SMG" || other.name == "LaserGun" || other.name == "Stick")
        {

            // CHECK SDK TYPE
            if (player.SDKType == "oculus")
            {

                if (other.gameObject.GetComponentInParent<Gun>().hand == "right")
                {
                    velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
                } else
                {
                    velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
                }

            } else
            {
                velocity = other.gameObject.GetComponentInParent<Gun>().GetVelocity();
            }

            if (Math.Abs(velocity.x - 0) > 1 || Math.Abs(velocity.y - 0) > 1 || Math.Abs(velocity.z - 0) > 1)
            {
                GameObject.Find("EnemyManager").GetComponent<EnemyManager>().inflictDamage(gameObject, "melee", other.gameObject);
                other.gameObject.GetComponentInParent<Gun>().Rumble();

            }

        }
    }

}
