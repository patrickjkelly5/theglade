using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    RaycastHit hit;
	RaycastHit[] hits;

    //public GameObject pistol;
    public GameObject pistolFlash;
    public GameObject pistolFlashRay;
    //public GameObject pistolPointerRay;

	bool firing = false;

	// Use this for initialization
	void Start () {
		
	}
	
	void Update()
	{

		//bool touchpadClick = OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad);
		bool primaryTrigger = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger);

		// if (touchpadClick){
		// 	cube.transform.localScale += new Vector3(1F, 1F, 1F);
		// }
		// if (primaryTrigger){
		// 	cube.transform.localScale += new Vector3(-1F, -1F, -1F);
		// }

		if (primaryTrigger)
		{

			if (!firing)
			{
				firing = true;

				// // PISTOL
				// if (ammoClip > 0)
				// {

				//     ammoClip -= 1;

					// Vector3 cartridgePosition = new Vector3(transform.position.x, transform.position.y + .04f, transform.position.z);

					// GameObject cartridge = Instantiate(cartridgeEject, cartridgePosition, transform.rotation);
					// cartridge.transform.Rotate(210, -65, 0);
					// Destroy(cartridge, 1f);

					// // CHECK SDK TYPE
					// if (player.SDKType == "oculus") haptic.Vibrate(VibrationForce.Hard);
					// else StartCoroutine(LongVibration(.1f, .7f));

					Vector3 fwd = transform.TransformDirection(Vector3.forward);
					hits = Physics.RaycastAll(transform.position, fwd, 40);
					Physics.Raycast(transform.position, fwd, out hit, 40);

					// slide.transform.localPosition = new Vector3(slide.transform.localPosition.x, slide.transform.localPosition.y, slide.transform.localPosition.z - 1.2f);
					// Invoke("ResetSlide", .2f);

					// firePistol.Play();
					pistolFlash.SetActive(true);
					pistolFlashRay.SetActive(true);
					pistolFlashRay.transform.localScale = new Vector3(0.3f, Vector3.Distance(hit.point, transform.position) * 33.3f, 0.3f);
					pistolFlashRay.transform.localPosition = new Vector3(0, 1.85f, Vector3.Distance(hit.point, transform.position) * 33.3f);
					//InflictBulletDamage();

					// // SHOW CURRENT AMMO
					// outOfAmmoNotice.SetActive(true);
					// outOfAmmoText.text = "<b>" + ammoClip + "</b> / " + player.ammo;
					// CancelInvoke("HideOutOfAmmoNotice");
					// Invoke("HideOutOfAmmoNotice", .5f);
				//}
				// else
				// {
				//     OutOfAmmoFunction(player.primaryGun.pistol);
				// }

			}
			else
			{
				pistolFlash.SetActive(false);
				pistolFlashRay.SetActive(false);
			}

		} else {
		// not firing
			pistolFlash.SetActive(false);
			pistolFlashRay.SetActive(false);
			firing = false;
		}

	}
}
