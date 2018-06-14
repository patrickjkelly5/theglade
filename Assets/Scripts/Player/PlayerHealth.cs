using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using VRTK;

public class PlayerHealth : MonoBehaviour
{

    public string SDKType;
    public string platformType;

    public GameObject oculusCamera;
    public GameObject oculusAvatar;
    public GameObject VRTKPrefab;
    public GameObject oculusVRPrefab;
    public GameObject steamVRPrefab;
    public float newGameTimer;

    public bool roomScaleOn;
    public GameObject roomscaleButton;

    public UpgradeManager menu;

    // OCULUS
    [SerializeField]
    OculusHapticsController leftControllerHaptics;
    [SerializeField]
    OculusHapticsController rightControllerHaptics;

    // STEAM
    public GameObject leftController;
    public GameObject rightController;

    public GameObject stick;
    public Collider stickCollider;
    public GameObject stickText;
    public bool stickTextAlreadyShown;
    public bool meleeStick;

    RaycastHit hit;

    public int startingHealth = 100;
    public int currentHealth;

    public float lastHealthChange;

    public float lastflash;

    public int baseAmmo;
    public int baseClipSize;

    public float ammoCapacityMultiplier;
    public int clipCapacityMultiplier;
    public float bulletDamageMultiplier;
    public float healthMultiplier;

    public int score;
    public int points;
    public int ammo;
    public int grenades;
    public bool isDead;

    public string chosenHand;
    public GameObject chosenController;

    // OCULUS
    public GameObject healthFlash;
    public Image healthFlashColor;

    // STEAM
    public GameObject steamHealthFlash;
    public Image steamHealthFlashColor;

    public GameObject headLocation;
    public GameObject startMenu;
    public EnemyManager manager;

    public GameObject leftContAvatar;
    public GameObject rightContAvatar;

    public Text startMenuTitle1;
    public Text startMenuTitle2;
    public Text startMenuTitle3;
    public Text startMenuTitle4;

    public Gun primaryGun;
    public Gun secondaryGun;
    public GameObject secondaryGunGO;

    public GameObject upgradeMenuPoints;
    public GameObject upgradeMenuAmmo;
    public GameObject upgradeMenuGrenades;
    public GameObject upgradeMenuSubtitle;
    public GameObject upgradeMenu;

    public GameObject upgradeMenuSMG;
    public GameObject upgradeMenuDoublePistols;
    public GameObject upgradeMenuLaser;

    public GameObject startGameButton;
    public GameObject startGameButtonBackground;
    public GameObject startGameButtonTitle;

    public GameObject infoScreen;
    public GameObject infoScreenButton;
    public GameObject infoScreenButtonBackground;
    public GameObject infoScreenButtonTitle;
    public GameObject infoScreenTitle;
    public GameObject infoScreenSubtitle;

    public bool tutorialBool;
    public string currentTut;

    public GameObject trackingSpace;

    public GameObject GUI;
    public GameObject sign;

    public bool canShoot;

    AudioSource[] playerSounds;
    AudioSource outOfAmmo;
    AudioSource chaChing;
    AudioSource beepSound;
    public AudioSource reload;

    public GameObject leftInitialControllerVive;
    public GameObject rightInitialControllerVive;

    public bool UMShowing;

    public bool startTutorialTimer;

    public float lastAttack;

    public Tutorial tutorial;
    float tutTimer;

    Color grey = new Color32(255, 255, 255, 47);
    Color white = new Color32(255, 255, 255, 255);

    public string headsetType;

    private void Start()
    {
        // IF EXPORTING FOR OCULUS STORE, SET SDKTYPE MANUALLY IN PLAYERHEALTH
        // if (SDKType == "oculus")
        // {
        //      oculusAvatar.SetActive(true);
        //      oculusCamera.SetActive(true);
        //   } else
        //   {
        //VRTKPrefab.SetActive(true);
        //   }

        menu = GetComponent<UpgradeManager>();
        playerSounds = GetComponents<AudioSource>();

        chaChing = playerSounds[0];
        outOfAmmo = playerSounds[1];
        beepSound = playerSounds[4];
        reload = playerSounds[6];

        lastflash = Time.time;

        tutorial = GetComponent<Tutorial>();

    }

    public void UpdateAmmo()
    {
        // UPDATE USER AMMO
        if (ammo < baseAmmo * ammoCapacityMultiplier) ammo = (int)(baseAmmo * ammoCapacityMultiplier);
    }

    public void FillAmmoClips()
    {
        // FILL AMMO CLIPS
        UpdateAmmo();
        primaryGun.ammoClipLimit = baseClipSize * clipCapacityMultiplier;
        secondaryGun.ammoClipLimit = baseClipSize * clipCapacityMultiplier;
        primaryGun.ammoClip = baseClipSize * clipCapacityMultiplier;
        secondaryGun.ammoClip = baseClipSize * clipCapacityMultiplier;

        if (secondaryGun.gameObject.activeSelf) ammo -= (baseClipSize * clipCapacityMultiplier) * 2; // Both Guns
        else ammo -= baseClipSize * clipCapacityMultiplier; // Single Gun

    }

    public void AllowTutorialStart()
    {
        startTutorialTimer = false;
    }

    public void InvokeTokenTimer(string type, string colliderName)
    {
        Debug.Log("Invoke Token Timer");

        if (type == "HeartToken(Clone)")
        {

            float multiplier = 2f;
            if (startingHealth == 200) multiplier = 1f;
            else if (startingHealth == 300) multiplier = .66f;

            // HEALTH
            if (currentHealth + 30 > startingHealth) currentHealth = startingHealth;
            else currentHealth += 30;

            // CHECK SDK TYPE
            if (SDKType == "oculus") healthFlashColor.color = new Color32(137, 23, 29, (byte)Mathf.RoundToInt((startingHealth - currentHealth) * multiplier));
            else steamHealthFlashColor.color = new Color32(137, 23, 29, (byte)Mathf.RoundToInt((startingHealth - currentHealth) * multiplier));

            lastHealthChange = Time.time;
        }
        else
        {

            // AMMO
            ammo += 30;

            if (colliderName == "Stick")
            {
                primaryGun.Reload();
                if (secondaryGunGO.activeSelf) secondaryGun.Reload();
            }
            
        }

    }

    // animate the game object from -1 to +1 and back
    public float minimum = 0.0F;
    public float maximum = 1.0F;

    // starting value for the Lerp
    static float t = 1.0f;

    public Light directionalLight;

    void UpdateLighting()
    {

        directionalLight.intensity = Mathf.Lerp(minimum, maximum, t);

        t -= 0.005f * Time.deltaTime;

    }

    void UpdateHealth()
    {

        if (currentHealth < startingHealth)
        {
            if ((Time.time - lastHealthChange) > .3f && Time.time - lastAttack > 3) {
                currentHealth += 1;

                float multiplier = 2f;
                if (startingHealth == 200) multiplier = 1f;
                else if (startingHealth == 300) multiplier = .66f;

                // CHECK SDK TYPE
                if (SDKType == "oculus") healthFlashColor.color = new Color32(137, 23, 29, (byte)Mathf.RoundToInt((startingHealth - currentHealth) * multiplier));
                else steamHealthFlashColor.color = new Color32(137, 23, 29, (byte)Mathf.RoundToInt((startingHealth - currentHealth) * multiplier));

                lastHealthChange = Time.time;

                if (currentHealth >= startingHealth)
                {
                    currentHealth = startingHealth;

                    // CHECK SDK TYPE
                    if (SDKType == "oculus") healthFlash.SetActive(false);
                    else steamHealthFlash.SetActive(false);

                }
            }
        }
    }

    IEnumerator LeftVibration(float length, float strength)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(leftController), strength);
            VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(rightController), strength);
            yield
            return null;
        }
    }

    IEnumerator RightVibration(float length, float strength)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(rightController), strength);
            yield
            return null;
        }
    }

    void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount)
    {

        currentHealth -= amount;

        float multiplier = 2f;
        if (startingHealth == 200) multiplier = 1f;
        else if (startingHealth == 300) multiplier = .66f;

        // CHECK SDK TYPE
        if (SDKType == "oculus")
        {
            leftControllerHaptics.Vibrate(VibrationForce.Hard);
            rightControllerHaptics.Vibrate(VibrationForce.Hard);

            healthFlash.SetActive(true);
            healthFlashColor.color = new Color32(137, 23, 29, (byte)Mathf.RoundToInt((startingHealth - currentHealth) * multiplier));
        }
        else
        {
            StartCoroutine(LeftVibration(.1f, .7f));

            steamHealthFlash.SetActive(true);
            steamHealthFlashColor.color = new Color32(137, 23, 29, (byte)Mathf.RoundToInt((startingHealth - currentHealth) * multiplier));
        }

        lastHealthChange = Time.time;
        lastAttack = Time.time;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
            manager.StopGame();
            newGameTimer = Time.time;
        }
    }

    void hideHealthFlash()
    {

        // CHECK SDK TYPE
        if (SDKType == "oculus") healthFlash.SetActive(false);
        else steamHealthFlash.SetActive(false);

    }

    void Death()
    {
        startingHealth = 100;
        currentHealth = 100;
        primaryGun.ShowControllers();
        stick.SetActive(false);
        stickText.SetActive(false);
        meleeStick = false;

        canShoot = false;

        hideHealthFlash();

        upgradeMenuSMG.SetActive(true);
        primaryGun.SMG.SetActive(false);

        primaryGun.ammoClipLimit = 8;
        secondaryGun.ammoClipLimit = 8;

        secondaryGun.gameObject.SetActive(false);
        secondaryGun.gameObject.transform.parent = gameObject.transform;

        primaryGun.pistol.SetActive(true);

         if (SDKType == "oculus")
         {
            leftContAvatar.GetComponent<RightControllerGrabGun>().handInZone = false;
            leftContAvatar.GetComponent<RightControllerGrabGun>().handInZone = false;
            rightContAvatar.GetComponent<RightControllerGrabGun>().handInZone = false;
            rightContAvatar.GetComponent<RightControllerGrabGun>().handInZone = false;
        } else
           {
            leftController.GetComponent<RightControllerGrabGun>().handInZone = false;
            leftController.GetComponent<RightControllerGrabGun>().handInZone = false;
            rightController.GetComponent<RightControllerGrabGun>().handInZone = false;
            rightController.GetComponent<RightControllerGrabGun>().handInZone = false;
        }

        isDead = true;

        GUI.SetActive(true);
        sign.SetActive(true);
        startMenu.SetActive(true);
        startMenuTitle1.text = "SORRY";
        startMenuTitle2.text = "YOU DIED";
        startMenuTitle3.text = "PLEASE TRY AGAIN";



        // Destroy all spiders
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Pickup");
        foreach (GameObject enemy in enemies) Destroy(enemy);

        manager.GetComponent<EnemyManager>().startMenuShowing = true;

    }

    void AllowGunShots()
    {
        canShoot = true;
    }

    void BuyItem(GameObject item)
    {

        manager.TemporarilyDisableButtons();

        int itemPrice = item.GetComponent<UpgradeItem>().itemPrice;

        if (points >= itemPrice)
        {

            chaChing.Play();
            points -= itemPrice;
            upgradeMenuPoints.GetComponent<Text>().text = "<b>POINTS:</b> " + points;

            if (!item.GetComponent<UpgradeItem>().userHasPurchased)
            {

                // show explanation UI
                infoScreen.SetActive(true);
                upgradeMenu.SetActive(false);

                if (item.name == "GrenadesBG")
                {
                    infoScreenTitle.GetComponent<Text>().text = "GRENADES";
                    infoScreenSubtitle.GetComponent<Text>().text = "GREAT FOR KILLING CROWDS OF YOUR ENEMIES AT ONCE!";
                    item.GetComponent<UpgradeItem>().userHasPurchased = true;
                }
                else if (item.name == "HealthBG")
                {
                    infoScreenTitle.GetComponent<Text>().text = "HEALTH";
                    if (healthMultiplier == 1) infoScreenSubtitle.GetComponent<Text>().text = "YOU NOW HAVE TWICE AS MUCH HEALTH";
                    else infoScreenSubtitle.GetComponent<Text>().text = "YOU NOW HAVE THREE TIMES AS MUCH HEALTH";
                }
                else if (item.name == "AmmoBackground")
                {
                    // THIS ONE IS NOT BEING USED RIGHT NOW - THIS IS THE OLD AMMO OPTION
                    //infoScreenTitle.GetComponent<Text>().text = "EXTRA AMMO";
                    //infoScreenSubtitle.GetComponent<Text>().text = "YOU GET MORE AMMO EACH ROUND, BUT IT NEVER HURTS TO STOCK UP";
                }
                else if (item.name == "SMGBG")
                {
                    infoScreenTitle.GetComponent<Text>().text = "SUBMACHINE GUNS";
                    infoScreenSubtitle.GetComponent<Text>().text = "YOU'LL KILL YOUR ENEMIES FASTER, BUT YOU'LL ALSO USE UP YOUR AMMO MUCH FASTER";
                }
                else if (item.name == "LaserBG")
                {
                    infoScreenTitle.GetComponent<Text>().text = "LASER GUNS";
                    infoScreenSubtitle.GetComponent<Text>().text = "BLAST YOUR ENEMIES AWAY WITH THESE POWERFUL LASER WEAPONS";
                }
                else if (item.name == "PistolsBG")
                {
                    infoScreenTitle.GetComponent<Text>().text = "DOUBLE PISTOLS";
                    infoScreenSubtitle.GetComponent<Text>().text = "EVERYONE KNOWS TWO IS ALWAYS BETTER THAN ONE";
                }
                else if (item.name == "AmmoCapacityBtnBG")
                {
                    infoScreenTitle.GetComponent<Text>().text = "AMMO CAPACITY";
                    if (ammoCapacityMultiplier == 1) infoScreenSubtitle.GetComponent<Text>().text = "YOU NOW RECEIVE 1.5X AS MANY BULLETS PER ROUND";
                    else if (ammoCapacityMultiplier == 1.5) infoScreenSubtitle.GetComponent<Text>().text = "YOU NOW RECEIVE TWICE AS MANY BULLETS PER ROUND";
                    else infoScreenSubtitle.GetComponent<Text>().text = "YOU NOW RECEIVE THREE TIMES AS MANY BULLETS PER ROUND";
                }
                else if (item.name == "ClipCapacityBtnBG")
                {
                    infoScreenTitle.GetComponent<Text>().text = "CLIP CAPACITY";
                    if (clipCapacityMultiplier == 1) infoScreenSubtitle.GetComponent<Text>().text = "YOU CAN NOW SHOOT TWICE AS MANY BULLETS BEFORE RELOADING";
                    else infoScreenSubtitle.GetComponent<Text>().text = "YOU CAN NOW SHOOT THREE TIMES AS MANY BULLETS BEFORE RELOADING";
                }
                else if (item.name == "BulletDamageBtnBG")
                {
                    infoScreenTitle.GetComponent<Text>().text = "BULLET DAMAGE";
                    if (bulletDamageMultiplier == 1) infoScreenSubtitle.GetComponent<Text>().text = "YOUR BULLETS NOW DO 1.5X THE AMOUNT OF DAMAGE";
                    else infoScreenSubtitle.GetComponent<Text>().text = "YOUR BULLETS NOW DO DOUBLE DAMAGE";
                }

            }

            if (item.name == "GrenadesBG")
            {
                // GIVE USER 4 MORE GRENADES
                grenades += 4;
                upgradeMenuGrenades.GetComponent<Text>().text = "<b>GRENADES:</b> " + grenades;

            }
            else if (item.name == "HealthBG")
            {
                if (healthMultiplier == 1)
                {
                    healthMultiplier = 2;
                    startingHealth = 200;
                    currentHealth = 200;

                    menu.healthBG.GetComponent<UpgradeItem>().itemPrice = 3000;
                    menu.healthTitle.GetComponent<Text>().text = "<b>3X HEALTH</b>\n3000";
                }
                else
                {
                    healthMultiplier = 3;
                    startingHealth = 300;
                    currentHealth = 300;
                    menu.health.SetActive(false);
                }
            }
            else if (item.name == "AmmoBackground")
            {

                // THIS IS THE OLD AMMO OPTION WHERE THEY JUST BUY MORE AMMO

                // GIVE USER MORE AMMO
                //if (primaryGun.currentGun == "SMG") ammo += 100;
                //else ammo += 40; // AMMO ISSUE **************************

                //UpdateAmmo();

                //upgradeMenuAmmo.GetComponent<Text>().text = "<b>AMMO:</b> " + ammo;

            }
            else if (item.name == "PistolsBG")
            {
                secondaryGunGO.SetActive(true);
                // GIVE USER THE SMG
                menu.pistols.SetActive(false);
                menu.lasers.SetActive(true);
                menu.SMGS.SetActive(true);

                secondaryGun.pistol.SetActive(true);
                primaryGun.pistol.SetActive(true);

                primaryGun.SMG.SetActive(false);
                secondaryGun.SMG.SetActive(false);
                primaryGun.laserGun.SetActive(false);
                secondaryGun.laserGun.SetActive(false);

                // ATTACH GUN TO SECONDARY HAND
                if (chosenHand == "left")
                {
                    secondaryGun.ChangeGun("right", "pistol");
                    rightContAvatar.SetActive(false);
                }
                else
                {
                    secondaryGun.ChangeGun("left", "pistol");
                    leftContAvatar.SetActive(false);
                }

                secondaryGun.transform.localEulerAngles = new Vector3(0, 0, 0);
                secondaryGun.transform.localPosition = new Vector3(0, 0, 0);

                FillAmmoClips();

                upgradeMenuAmmo.GetComponent<Text>().text = "<b>AMMO:</b> " + ammo;

            }
            else if (item.name == "SMGBG")
            {
                // GIVE USER THE SMG
                menu.SMGS.SetActive(false);
                menu.lasers.SetActive(true);

                secondaryGunGO.SetActive(true);

                // ATTACH GUN TO SECONDARY HAND
                if (chosenHand == "left")
                {
                    secondaryGun.ChangeGun("right", "SMG");
                    rightContAvatar.SetActive(false);
                }
                else
                {
                    secondaryGun.ChangeGun("left", "SMG");
                    leftContAvatar.SetActive(false);
                }

                secondaryGun.transform.localEulerAngles = new Vector3(0, 0, 0);
                secondaryGun.transform.localPosition = new Vector3(0, 0, 0);

                primaryGun.SMG.SetActive(true);
                primaryGun.SMGPointerRay.SetActive(true);
                primaryGun.currentGun = "SMG";
                primaryGun.pistol.SetActive(false);
                primaryGun.laserGun.SetActive(false);

                secondaryGun.SMG.SetActive(true);
                secondaryGun.currentGun = "SMG";
                secondaryGun.pistol.SetActive(false);
                secondaryGun.laserGun.SetActive(false);

                baseAmmo = 100;
                baseClipSize = 24;

                FillAmmoClips();

                upgradeMenuAmmo.GetComponent<Text>().text = "<b>AMMO:</b> " + ammo;

            }
            else if (item.name == "LaserBG")
            {
                // GIVE USER THE LASER GUN
                menu.lasers.SetActive(false);

                secondaryGunGO.SetActive(true);

                // ATTACH GUN TO SECONDARY HAND
                if (chosenHand == "left")
                {
                    secondaryGun.ChangeGun("right", "laser");
                    rightContAvatar.SetActive(false);
                }
                else
                {
                    secondaryGun.ChangeGun("left", "laser");
                    leftContAvatar.SetActive(false);
                }

                secondaryGun.transform.localEulerAngles = new Vector3(0, 0, 0);
                secondaryGun.transform.localPosition = new Vector3(0, 0, 0);

                primaryGun.laserGun.SetActive(true);
                primaryGun.laserPointerRay.SetActive(true);
                primaryGun.currentGun = "laser";
                primaryGun.SMG.SetActive(false);
                primaryGun.pistol.SetActive(false);

                secondaryGun.laserGun.SetActive(true);
                secondaryGun.currentGun = "laser";
                secondaryGun.SMG.SetActive(false);
                secondaryGun.pistol.SetActive(false);

                baseClipSize = 12;

                FillAmmoClips();

                upgradeMenuAmmo.GetComponent<Text>().text = "<b>AMMO:</b> " + ammo;

            }
            else if (item.name == "AmmoCapacityBtnBG")
            {
                if (ammoCapacityMultiplier == 1)
                {
                    ammoCapacityMultiplier = 1.5F;
                    menu.ammoCapacityBtnBG.GetComponent<UpgradeItem>().itemPrice = 2000;
                    menu.ammoCapacityDescription.GetComponent<Text>().text = "2X AMMO CAPACITY";
                    menu.ammoCapacityBtnText.GetComponent<Text>().text = "BUY - 2000 PTS";

                    //FillAmmoClips();
                }
                else if (ammoCapacityMultiplier == 1.5)
                {
                    ammoCapacityMultiplier = 2;
                    menu.ammoCapacityBtnBG.GetComponent<UpgradeItem>().itemPrice = 3000;
                    menu.ammoCapacityDescription.GetComponent<Text>().text = "3X AMMO CAPACITY";
                    menu.ammoCapacityBtnText.GetComponent<Text>().text = "BUY - 3000 PTS";

                    //FillAmmoClips();
                }
                else
                {
                    ammoCapacityMultiplier = 3;
                    menu.ammoCapacity.SetActive(false);

                    //FillAmmoClips();
                }
            }
            else if (item.name == "ClipCapacityBtnBG")
            {
                if (clipCapacityMultiplier == 1)
                {
                    clipCapacityMultiplier = 2;
                    menu.clipCapacityBtnBG.GetComponent<UpgradeItem>().itemPrice = 3000;
                    menu.clipCapacityDescription.GetComponent<Text>().text = "3X CLIP CAPACITY";
                    menu.clipCapacityBtnText.GetComponent<Text>().text = "BUY - 3000 PTS";

                    FillAmmoClips();
                }
                else
                {
                    clipCapacityMultiplier = 3;
                    menu.clipCapacity.SetActive(false);

                    FillAmmoClips();
                }
            }
            else if (item.name == "BulletDamageBtnBG")
            {
                if (bulletDamageMultiplier == 1)
                {
                    bulletDamageMultiplier = 1.5F;
                    menu.bulletDamageBtnBG.GetComponent<UpgradeItem>().itemPrice = 3000;
                    menu.bulletDamageDescription.GetComponent<Text>().text = "2X BULLET DAMAGE";
                    menu.bulletDamageBtnText.GetComponent<Text>().text = "BUY - 3000 PTS";
                }
                else
                {
                    bulletDamageMultiplier = 2;
                    menu.bulletDamage.SetActive(false);
                }
            }

        }
        else
        {
            upgradeMenuSubtitle.GetComponent<Text>().text = "YOU DON'T HAVE ENOUGH POINTS TO BUY THIS ITEM YET";
            upgradeMenuPoints.GetComponent<Text>().color = new Color32(255, 0, 0, 255);
            outOfAmmo.Play();
            Invoke("ResetMenuPointsColor", 2);
        }

    }

    void ResetMenuPointsColor()
    {
        upgradeMenuPoints.GetComponent<Text>().color = white;
        upgradeMenuSubtitle.GetComponent<Text>().text = "USE YOUR POINTS TO BUY UPGRADES";
    }

    public void ShowTut(string tut)
    {
        tutorialBool = true;
        currentTut = tut;
        tutTimer = Time.time;

        if (tut == "Start")
        {
            tutorial.completeTut.SetActive(true);
            tutorial.completeTut.GetComponentInChildren<Text>().text = "PRESS ANY BUTTON TO START";
        }
        else if (tut == "Trigger")
        {
            tutorial.trigger.SetActive(true);
            tutorial.completeTut.SetActive(false);
        }
        else if (tut == "Reload")
        {
            tutorial.reload.SetActive(true);
            tutorial.trigger.SetActive(false);
        }
        else if (tut == "Grenade")
        {
            tutorial.grenade.SetActive(true);
            tutorial.reload.SetActive(false);
        }
        else if (tut == "Joystick")
        {
            tutorial.joystick.SetActive(true);
            tutorial.grenade.SetActive(false);
        }
        else if (tut == "Complete")
        {
            tutorial.completeTut.GetComponentInChildren<Text>().text = "PRESS ANY BUTTON TO COMPLETE";
            tutorial.completeTut.SetActive(true);

            if (headsetType == "Vive") tutorial.grenade.SetActive(false);
            else tutorial.joystick.SetActive(false);

        }
    }

    private void FixedUpdate()
    {
        //UpdateLighting();
        if (!manager.isPaused) UpdateHealth();

        if (tutorialBool)
        {

            Debug.Log("TUTORIAL");

            if ((Time.time - lastflash) > .5f) {

                    if (currentTut == "Joystick")
                    {
                        if (tutorial.joystickDot.activeSelf)
                        {
                            tutorial.joystickDot.SetActive(false);
                            tutorial.joystickLine.SetActive(false);
                        }
                        else
                        {
                            tutorial.joystickDot.SetActive(true);
                            tutorial.joystickLine.SetActive(true);
                        }
                        lastflash = Time.time;
                    }
                    else if (currentTut == "Grenade")
                    {
                        if (tutorial.grenadeDot.activeSelf)
                        {
                            tutorial.grenadeDot.SetActive(false);
                            tutorial.grenadeLine.SetActive(false);
                        }
                        else
                        {
                            tutorial.grenadeDot.SetActive(true);
                            tutorial.grenadeLine.SetActive(true);
                        }
                        lastflash = Time.time;
                    }
                    else if (currentTut == "Trigger")
                    {
                        if (tutorial.triggerDot.activeSelf)
                        {
                            tutorial.triggerDot.SetActive(false);
                            tutorial.triggerLine.SetActive(false);
                        }
                        else
                        {
                            tutorial.triggerDot.SetActive(true);
                            tutorial.triggerLine.SetActive(true);
                        }
                        lastflash = Time.time;
                    }
                    else if (currentTut == "Reload")
                    {
                        if (tutorial.reloadDot.activeSelf)
                        {
                            tutorial.reloadDot.SetActive(false);
                            tutorial.reloadLine.SetActive(false);
                        }
                        else
                        {
                            tutorial.reloadDot.SetActive(true);
                            tutorial.reloadLine.SetActive(true);
                        }
                        lastflash = Time.time;
                    }
            }
        }

    }

    void CheckTutorialStateOculus()
    {
        if (tutorialBool)
        {

            Debug.Log("TUTORIAL");

            if (currentTut == "Complete")
            {
                if (OVRInput.GetDown(OVRInput.Button.Any))
                {
                    tutorialBool = false;
                    tutorial.completeTut.SetActive(false);
                    currentTut = "";
                }
            }
            else if (currentTut == "Start")
            {
                if (OVRInput.GetDown(OVRInput.Button.Any) && !startTutorialTimer)
                {
                    ShowTut("Trigger");
                }
            }
            else if (currentTut == "Joystick")
            {

                if (chosenHand == "right")
                {
                    if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickLeft) || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstickRight)) ShowTut("Complete");
                }
                else
                {
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft) || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight)) ShowTut("Complete");
                }
            }
            else if (currentTut == "Grenade")
            {
                if (chosenHand == "right")
                {
                    if (OVRInput.GetDown(OVRInput.Button.One)) ShowTut("Joystick");
                }
                else
                {
                    if (OVRInput.GetDown(OVRInput.Button.Three)) ShowTut("Joystick");
                }
            }
            else if (currentTut == "Trigger" && !startTutorialTimer)
            {
                if (chosenHand == "right")
                {
                    if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)) ShowTut("Reload");
                }
                else
                {
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) ShowTut("Reload");
                }
            }
            else if (currentTut == "Reload")
            {
                if (chosenHand == "right")
                {
                    if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger)) ShowTut("Grenade");
                }
                else
                {
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger)) ShowTut("Grenade");
                }
            }
        }
    }

    void CheckTutorialStateSteam()
    {

        if (tutorialBool)
        {

            if (currentTut == "Complete")
            {
                if (anyButtonDown && (Time.time - tutTimer > .5f))
                {
                    // END TUTORIAL
                    tutorialBool = false;
                    tutorial.completeTut.SetActive(false);
                    currentTut = "";
                }
            }
            else if (currentTut == "Start")
            {
                if (anyButtonDown && !startTutorialTimer)
                {
                    ShowTut("Trigger");
                }
            }
            else if (currentTut == "Joystick")
            {
                // NEED TO DECIDE IF I'M GOING TO REMOVE THE JOYSTICK FUNCTION OR NOT ***************************
                // IF NOT, I NEED TO FIGURE OUT THE VRTK CONTROL FOR OCULUS JOYSTICK LEFT AND RIGHT
                // CAN I TARGET OCULUS SDK FOR OCULUS ON STEAM?
            }
            else if (currentTut == "Grenade")
            {
                // STILL NEED TO DECIDE THE JOYSTICK THING
                if (chosenController.GetComponent<VRTK_ControllerEvents>().buttonOnePressed) ShowTut("Joystick");
                else if (chosenController.GetComponent<VRTK_ControllerEvents>().touchpadPressed) ShowTut("Complete");
            }
            else if (currentTut == "Trigger" && (Time.time - tutTimer > .5f))
            {
                if (triggerDown) ShowTut("Reload");
            }
            else if (currentTut == "Reload")
            {
                if (chosenController.GetComponent<VRTK_ControllerEvents>().gripPressed) ShowTut("Grenade");
            }

        }
    }

    bool triggerDown;
    bool triggerDownBool;
    bool gripDown;
    bool gripDownBool;
    bool touchpadDown;
    bool touchpadDownBool;
    bool anyButtonDown;
    bool anyButtonDownBool;

    bool headsetTypeFound;

    private void Update()
    {

        // CHECK SDK TYPE
        if (SDKType == "oculus") CheckTutorialStateOculus();
        else CheckTutorialStateSteam();

        if (!headsetTypeFound)
        {
            headsetType = VRTK_DeviceFinder.GetHeadsetType(true).ToString();

            if (headsetType == "OculusRift" || OVRManager.isHmdPresent)
            {
                SDKType = "oculus";
                headsetTypeFound = true;
                oculusAvatar.SetActive(true);
                oculusCamera.SetActive(true);
                roomscaleButton.SetActive(true);
            }
            else
            {

                SDKType = "steam";
                headsetTypeFound = true;
                VRTKPrefab.SetActive(true);
                roomscaleButton.SetActive(false);

            }

        }

        if (oculusVRPrefab.activeSelf) oculusVRPrefab.SetActive(false); // this seems inefficient...

        if (chosenController){

            gripDown = chosenController.GetComponent<VRTK_ControllerEvents>().gripPressed;

            touchpadDown = (chosenController.GetComponent<VRTK_ControllerEvents>().buttonOnePressed
            || chosenController.GetComponent<VRTK_ControllerEvents>().touchpadPressed);

            // TRIGGER DOWN
            if (chosenController.GetComponent<VRTK_ControllerEvents>().triggerPressed && !triggerDownBool)
            {
                triggerDownBool = true;
                triggerDown = true;
            }
            else if (chosenController.GetComponent<VRTK_ControllerEvents>().triggerPressed && triggerDownBool) triggerDown = false;
            else if (!chosenController.GetComponent<VRTK_ControllerEvents>().triggerPressed && triggerDownBool) triggerDownBool = false;

            // ANY BUTTON DOWN
            if (chosenController.GetComponent<VRTK_ControllerEvents>().AnyButtonPressed() && !anyButtonDownBool)
            {
                anyButtonDownBool = true;
                anyButtonDown = true;
            }
            else if (chosenController.GetComponent<VRTK_ControllerEvents>().AnyButtonPressed() && anyButtonDownBool) anyButtonDown = false;
            else if (!chosenController.GetComponent<VRTK_ControllerEvents>().AnyButtonPressed() && anyButtonDownBool) anyButtonDownBool = false;



        }


        // CHECK SDK TYPE
        if (SDKType == "oculus")
        {
            // CHECK FOR UNIVERSAL MENU SHOWING
            if (!OVRManager.hasVrFocus && !UMShowing && (canShoot || upgradeMenu.activeSelf || startMenu.activeSelf))
            {
                UMShowing = true;
                manager.ShowPauseMenu();
            }
            else if (OVRManager.hasVrFocus && UMShowing)
            {
                manager.HidePauseMenu();
            }
        }
        else
        {
            // CHECK IF HEADSET HAS FOCUS FOR STEAM ** ??
        }

        GetComponent<CapsuleCollider>().center = new Vector3(headLocation.transform.localPosition.x, -0.75f, headLocation.transform.localPosition.z);

        // ITEM INFO SCREEN BUTTON
        if (infoScreen.activeSelf)
        {

            Vector3 fwd = primaryGun.transform.TransformDirection(Vector3.forward);
            Physics.Raycast(primaryGun.transform.position, fwd, out hit, 40);

            if (hit.collider != null && hit.collider.name == "InfoScreenButton")
            {

                infoScreenButtonBackground.GetComponent<Image>().color = white;
                infoScreenButtonTitle.GetComponent<Text>().color = new Color32(38, 20, 6, 190);
                if (primaryGun.triggerIsDown)
                {
                    manager.TemporarilyDisableButtons();
                    upgradeMenu.SetActive(true);
                    infoScreen.SetActive(false);
                    beepSound.Play();
                }
            }
            else
            {
                infoScreenButtonBackground.GetComponent<Image>().color = grey;
                infoScreenButtonTitle.GetComponent<Text>().color = white;
            }
        }

        // START NEW GAME BUTTON
        if (startGameButton.activeSelf)
        {
            Vector3 fwd = primaryGun.transform.TransformDirection(Vector3.forward);
            Physics.Raycast(primaryGun.transform.position, fwd, out hit, 40);

            if (hit.collider != null && hit.collider.name == "StartGameButton")
            {

                startGameButtonBackground.GetComponent<Image>().color = white;
                if (primaryGun.triggerIsDown)
                {
                    manager.StartNewGame();
                    primaryGun.pistolPointerRay.SetActive(false);
                    primaryGun.SMGPointerRay.SetActive(false); // unessecary?
                    primaryGun.laserPointerRay.SetActive(false); // unessecary?
                    Invoke("AllowGunShots", 1);
                    beepSound.Play();

                }
            }
            else
            {
                startGameButtonBackground.GetComponent<Image>().color = new Color32(255, 255, 255, 200);
            }
        }

        if (manager.upgradeMenuShowing)
        {
            Vector3 fwd = primaryGun.transform.TransformDirection(Vector3.forward);
            Physics.Raycast(primaryGun.transform.position, fwd, out hit, 40);

            // AMMO CATEGORY
            if (hit.collider != null && hit.collider.name == "AmmoCategory")
            {
                menu.ammoCategoryBG.GetComponent<Image>().color = white;
                if (primaryGun.triggerIsDown) {
                    menu.ChooseCategory(menu.ammoMenu);
                    menu.startRoundBtnTitle.GetComponent<Text>().text = "MAIN MENU";
                    manager.TemporarilyDisableButtons();
                    beepSound.Play();
                }
            }
            else menu.ammoCategoryBG.GetComponent<Image>().color = grey;

            // OTHER CATEGORY
            if (hit.collider != null && hit.collider.name == "OtherCategory")
            {
                menu.otherCategoryBG.GetComponent<Image>().color = white;
                if (primaryGun.triggerIsDown)
                {
                    menu.ChooseCategory(menu.otherMenu);
                    menu.startRoundBtnTitle.GetComponent<Text>().text = "MAIN MENU";
                    manager.TemporarilyDisableButtons();
                    beepSound.Play();
                }
            }
            else menu.otherCategoryBG.GetComponent<Image>().color = grey;

            // GUNS CATEGORY
            if (hit.collider != null && hit.collider.name == "GunsCategory")
            {
                menu.gunsCategoryBG.GetComponent<Image>().color = white;
                if (primaryGun.triggerIsDown)
                {
                    menu.ChooseCategory(menu.gunMenu);
                    menu.startRoundBtnTitle.GetComponent<Text>().text = "MAIN MENU";
                    manager.TemporarilyDisableButtons();
                    beepSound.Play();
                }
            }
            else menu.gunsCategoryBG.GetComponent<Image>().color = grey;


            // GUNS --------------------------------------------------------------

            // PISTOL
            if (hit.collider != null && hit.collider.name == "Pistols")
            {
                menu.pistolsBG.GetComponent<Image>().color = white;
                if (primaryGun.triggerIsDown) BuyItem(menu.pistolsBG);
            }
            else menu.pistolsBG.GetComponent<Image>().color = grey;

            // SMG
            if (hit.collider != null && hit.collider.name == "SMG")
            {
                menu.SMGSBG.GetComponent<Image>().color = white;
                if (primaryGun.triggerIsDown) BuyItem(menu.SMGSBG);
            }
            else menu.SMGSBG.GetComponent<Image>().color = grey;

            // LASER
            if (hit.collider != null && hit.collider.name == "Laser")
            {
                menu.lasersBG.GetComponent<Image>().color = white;
                if (primaryGun.triggerIsDown) BuyItem(menu.lasersBG);
            }
            else menu.lasersBG.GetComponent<Image>().color = grey;

            //--------------------------------------------------------------

            // GRENADES
            if (hit.collider != null && hit.collider.name == "Grenades")
            {
                menu.grenadesBG.GetComponent<Image>().color = white;
                if (primaryGun.triggerIsDown) BuyItem(menu.grenadesBG);
            }
            else menu.grenadesBG.GetComponent<Image>().color = grey;

            // HEALTH
            if (hit.collider != null && hit.collider.name == "Health")
            {
                menu.healthBG.GetComponent<Image>().color = white;
                if (primaryGun.triggerIsDown) BuyItem(menu.healthBG);
            }
            else menu.healthBG.GetComponent<Image>().color = grey;

            // AMMO CAPACITY
            if (hit.collider != null && hit.collider.name == "AmmoCapacity")
            {
                menu.ammoCapacityBtnBG.GetComponent<Image>().color = white;
                menu.ammoCapacityBtnText.GetComponent<Text>().color = new Color32(38, 20, 6, 190);

                if (primaryGun.triggerIsDown) BuyItem(menu.ammoCapacityBtnBG);
            }
            else {
                menu.ammoCapacityBtnBG.GetComponent<Image>().color = grey;
                menu.ammoCapacityBtnText.GetComponent<Text>().color = white;
            }

            // CLIP CAPACITY
            if (hit.collider != null && hit.collider.name == "ClipCapacity")
            {
                menu.clipCapacityBtnBG.GetComponent<Image>().color = white;
                menu.clipCapacityBtnText.GetComponent<Text>().color = new Color32(38, 20, 6, 190);

                if (primaryGun.triggerIsDown) BuyItem(menu.clipCapacityBtnBG);
            }
            else
            {
                menu.clipCapacityBtnBG.GetComponent<Image>().color = grey;
                menu.clipCapacityBtnText.GetComponent<Text>().color = white;
            }

            // BULLET DAMAGE
            if (hit.collider != null && hit.collider.name == "BulletDamage")
            {
                menu.bulletDamageBtnBG.GetComponent<Image>().color = white;
                menu.bulletDamageBtnText.GetComponent<Text>().color = new Color32(38, 20, 6, 190);

                if (primaryGun.triggerIsDown) BuyItem(menu.bulletDamageBtnBG);
            }
            else
            {
                menu.bulletDamageBtnBG.GetComponent<Image>().color = grey;
                menu.bulletDamageBtnText.GetComponent<Text>().color = white;
            }

            // START NEW ROUND BUTTON
            if (hit.collider != null && hit.collider.name == "StartRoundBtn")
            {
                menu.startRoundBtnBG.GetComponent<Image>().color = white;
                menu.startRoundBtnTitle.GetComponent<Text>().color = new Color32(38, 20, 6, 190);
                if (primaryGun.triggerIsDown)
                {
                    if (menu.categoryMenu.activeSelf)
                    {
                        manager.startNextRound();
                        primaryGun.pistolPointerRay.SetActive(false);
                        primaryGun.SMGPointerRay.SetActive(false);
                        primaryGun.laserPointerRay.SetActive(false);
                        Invoke("AllowGunShots", 1);
                        beepSound.Play();
                    } else
                    {
                        menu.ChooseCategory(menu.categoryMenu);
                        menu.startRoundBtnTitle.GetComponent<Text>().text = "NEXT ROUND";
                        manager.TemporarilyDisableButtons();
                        beepSound.Play();
                    }
                }
            }
            else
            {
                menu.startRoundBtnBG.GetComponent<Image>().color = grey;
                menu.startRoundBtnTitle.GetComponent<Text>().color = white;
            }

        }


    }

}