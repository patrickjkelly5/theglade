using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
//using Oculus.Platform;

public class EnemyManager : MonoBehaviour
{
//     public PlayerHealth playerHealth;
//     public GameObject[] enemy;
//     public Transform[] spawnPoints;
//     public Transform bossSpawnPoint;
//     public GameObject headLocation;
//     public GameObject spidersContainer;
//     GameObject gun;
//     public GameObject pistol;
//     public GameObject SMG;
//     public GameObject sign;
//     public GameObject bench;

//     public GameObject rightHand;
//     public GameObject leftHand;

//     public GameObject player;
//     RaycastHit hit;

//     public GameObject trees;
//     public GameObject bushes;

//     public GameObject upgradeMenuPoints;
//     public GameObject upgradeMenuAmmo;
//     public GameObject upgradeMenuGrenades;
//     public GameObject upgradeMenuSMG;
//     public GameObject upgradeMenuDoublePistols;
//     public GameObject upgradeMenuLaser;

//     public Collider menuButton;
//     public Collider menuOtherCategory;
//     public Collider menuAmmoCategory;
//     public Collider menuGunsCategory;

//     public Collider menuPistol;
//     public Collider menuSMG;
//     public Collider menuLaser;

//     public Collider menuAmmoCapacity;
//     public Collider menuClipCapacity;
//     public Collider menuBulletDamage;

//     public Collider menuGrenades;
//     public Collider menuHealth;

//     public Collider infoScreenButton;

//     public int attackDamage = 50;

//     public GameObject ammoToken;
//     public GameObject healthToken;

//     public int spiderCount = 0;
//     public int waveCount = 1;
//     public int waveLimit = 6;
//     public int killedSpiders = 0;
//     public float spawnTime = 4f;

//     public GameObject healthBar;

//     public GameObject categoryMenu;
//     public GameObject upgradeMenu;
//     public GameObject pauseUI;
//     public GameObject startMenu;
//     AudioSource clownSong;
//     AudioSource battleSongOne;
//     AudioSource battleSongTwo;

//     public GameObject headshot;
//     public GameObject notice;

//     public UpgradeManager menu;

//     // MENU STUFF
//     public bool startMenuShowing = true;
//     public bool upgradeMenuShowing;
//     public bool isPaused;

//     bool introFade;
//     bool introFadeOut;
//     bool battleFade;
//     bool battleFadeOut;
//     bool roundEndFade;

//     bool fadeInSongOne;
//     bool fadeInSongTwo;

//     public Gun primaryGun;
//     public Gun secondaryGun;

//     float timeBetweenSounds = 5.0f;
//  float lastSound;

//     public GameObject spiderContainer;
//     bool spiderSound;

//     public GameObject controllerLeft;
//     public GameObject controllerRight;

//     int index;

//     private void Update()
//     {

//         if (Time.time - lastSound >= timeBetweenSounds)
//         {
//             spiderSound = false;

//             GameObject[] spiders = GameObject.FindGameObjectsWithTag("Pickup");

//             if (spiders.Length > 0)
//             {

//                 foreach (GameObject spider in spiders)
//                 {
//                     if (!spider.GetComponent<EnemyAttack>().isSinking)
//                     {
//                         if (Random.Range(1, 10) > 5 && !spiderSound)
//                         {
//                             if (Random.Range(1, 10) > 5) spider.GetComponent<EnemyAttack>().spiderHiss.Play();
//                             else spider.GetComponent<EnemyAttack>().spiderClick.Play();
//                             spiderSound = true;
//                             lastSound = Time.time;
//                         }
//                     }
//                 }

//             }
//         }

//         // FADE IN INTRO MUSIC
//         if (introFade)
//         {
//             clownSong.volume += .02f* Time.deltaTime;

//             if (clownSong.volume >= .8)
//             {
//                 introFade = false;
//             }
//         }

//         // FADE OUT INTRO MUSIC
//         if (introFadeOut)
//         {
//             clownSong.volume -= .1f* Time.deltaTime;

//             if (clownSong.volume <= 0)
//             {
//                 introFade = false;
//                 clownSong.Stop();
//             }
//         }

//         if (roundEndFade)
//         {
//             if (battleSongOne.isPlaying)
//             {
//                 if (battleSongOne.volume > .2) battleSongOne.volume -= .13f* Time.deltaTime;
//     else roundEndFade = false;
//             }
//             else if (battleSongTwo.isPlaying)
//             {
//                 battleSongTwo.volume -= .13f* Time.deltaTime;
//                 if (battleSongTwo.volume <= .2) roundEndFade = false;
//             }
//         }

//         if (fadeInSongOne)
//         {

//             // FADE OUT SONG TWO
//             if (battleSongTwo.volume > 0) battleSongTwo.volume -= .13f* Time.deltaTime;
//    else battleSongTwo.Stop();

//             if (!battleSongOne.isPlaying)
//             {
//                 battleSongOne.volume = 0;
//                 battleSongOne.Play();
//             }

//             // FADE IN SONG ONE
//             battleSongOne.volume += .13f* Time.deltaTime;

//             // STOP FADE
//             if (battleSongOne.volume >= .8) fadeInSongOne = false;
//         }

//         if (fadeInSongTwo)
//         {

//             // FADE OUT SONG ONE
//             if (battleSongOne.volume > 0) battleSongOne.volume -= .13f * Time.deltaTime;
//    else battleSongOne.Stop();

//             if (!battleSongTwo.isPlaying)
//             {
//                 battleSongTwo.volume = 0;
//                 battleSongTwo.Play();
//             }

//             // FADE IN SONG TWO
//             battleSongTwo.volume += .13f * Time.deltaTime;

//             // STOP FADE
//             if (battleSongTwo.volume >= .8) fadeInSongTwo = false;
//         }

//     }

//     private void Start()
//     {

//         menu = playerHealth.GetComponent<UpgradeManager>();
//         AudioSource[] audioSources = GetComponents<AudioSource>();

//         clownSong = audioSources[0];
//         battleSongOne = audioSources[1];
//         battleSongTwo = audioSources[2];

//         // FADE IN INTRO MUSIC
//         clownSong.volume = 0;
//         clownSong.Play();
//         introFade = true;
//     }

//     public void HidePauseMenu()
//     {
//         sign.SetActive(false);
//         pauseUI.SetActive(false);
//         isPaused = false;
//         spidersContainer.SetActive(true);

//         playerHealth.UMShowing = false;

//         if (upgradeMenuShowing)
//         {
//             // SHOW UPGRADE MENU
//             sign.SetActive(true);
//             upgradeMenu.SetActive(true);
//         }
//         else
//         if (startMenuShowing)
//         {
//             // SHOW START BUTTON
//             sign.SetActive(true);
//             startMenu.SetActive(true);
//         }
//         else
//         {
//             if (spiderCount < waveLimit)
//             {
//                 InvokeRepeating("Spawn", spawnTime, spawnTime);
//             }
//             playerHealth.canShoot = true;
//         }
//     }

//     public void ShowPauseMenu()
//     {

//         // SHOW PAUSE UI
//         upgradeMenu.SetActive(false);
//         sign.SetActive(true);
//         startMenu.SetActive(false);
//         pauseUI.SetActive(true);

//         // UPDATE PAUSE SCREEN STATS
//         GameObject.Find("PointsText").GetComponent<Text>().text = "<b>POINTS:</b> " + playerHealth.points;
//         GameObject.Find("RoundText").GetComponent<Text>().text = "<b>ROUND:</b> " + waveCount;
//         GameObject.Find("HealthText").GetComponent<Text>().text = "<b>HEALTH:</b> " + ((float)playerHealth.currentHealth / (float)playerHealth.startingHealth) * 100 + "%";

//         GameObject.Find("GrenadesText").GetComponent<Text>().text = "<b>GRENADES:</b> " + playerHealth.grenades;

//         // UPDATE PAUSE SCREEN AMMO
//         GameObject.Find("AmmoText").GetComponent<Text>().text = "<b>AMMO:</b> " + playerHealth.ammo;

//         isPaused = true;
//         playerHealth.canShoot = false;

//         CancelInvoke("Spawn");

//         spidersContainer.SetActive(false);

//         // CHECK FOR DEAD SPIDERS AND DESTROY THEM ON PAUSE
//         Component[] comps = spidersContainer.GetComponentsInChildren(typeof(Transform), true);
//         foreach (Component c in comps)
//         {
//             if (c.name == "spider(Clone)" && c.GetComponent<EnemyHealth>().currentHealth <= 0)
//             {
//                 Destroy(c.gameObject);
//             }
//         }

//     }

//     public void StopGame()
//     {
//         CancelInvoke("Spawn");
//         // Destroy all tokens
//         GameObject[] tokens = GameObject.FindGameObjectsWithTag("Token");
//         foreach (GameObject t in tokens) Destroy(t);

//         playerHealth.canShoot = false;

//         if (primaryGun.currentGun == "pistol")
//         {
//             primaryGun.pistolFlash.SetActive(false);
//             primaryGun.pistolFlashRay.SetActive(false);

//             secondaryGun.pistolFlash.SetActive(false);
//             secondaryGun.pistolFlashRay.SetActive(false);
//             playerHealth.GetComponents<AudioSource>()[3].Stop();
//         }
//         else if (primaryGun.currentGun == "SMG")
//         {
//             primaryGun.pistol.SetActive(true);

//             playerHealth.GetComponents<AudioSource>()[3].Stop();
//             primaryGun.SMGFlash.SetActive(false);
//             primaryGun.SMGFlashRay.SetActive(false);
//             primaryGun.SMG.SetActive(false);
//         }
//         else if (primaryGun.currentGun == "laser")
//         {
//             primaryGun.pistol.SetActive(true);
//             primaryGun.laserGun.SetActive(false);
//             primaryGun.laserFlash.SetActive(false);
//             primaryGun.laserFlashRay.SetActive(false);
//             playerHealth.GetComponents<AudioSource>()[3].Stop();
//         }

//         primaryGun.transform.parent = null;
//         primaryGun.transform.localEulerAngles = new Vector3(0, -90, 90);
//         primaryGun.transform.localPosition = new Vector3(32.93f, .65f, 42.93f);
//         primaryGun.GetComponent<Gun>().handChosen = false;
//         controllerLeft.SetActive(true);
//         controllerRight.SetActive(true);

//         bench.SetActive(true);

//         roundEndFade = true;

//     }

//     public void endWave()
//     {

//         primaryGun.ammoClip = 0;
//         secondaryGun.ammoClip = 0;

//         if (primaryGun.currentGun == "pistol")
//         {
//             playerHealth.stick.SetActive(false);
//             playerHealth.meleeStick = false;

//             primaryGun.pistol.SetActive(true);
//             primaryGun.pistolPointerRay.SetActive(true);
//             primaryGun.pistolFlash.SetActive(false);
//             primaryGun.pistolFlashRay.SetActive(false);

//             primaryGun.SMG.SetActive(false);
//             primaryGun.laserGun.SetActive(false);

//             secondaryGun.pistolFlash.SetActive(false);
//             secondaryGun.pistolFlashRay.SetActive(false);

//             playerHealth.UpdateAmmo();
//             upgradeMenuAmmo.GetComponent<Text>().text = "<b>AMMO:</b> " + playerHealth.ammo;
//         }
//         else if (primaryGun.currentGun == "SMG")
//         {
//             playerHealth.stick.SetActive(false);
//             playerHealth.meleeStick = false;

//             primaryGun.pistol.SetActive(false);
//             primaryGun.laserGun.SetActive(false);

//             primaryGun.SMG.SetActive(true);
//             primaryGun.SMGPointerRay.SetActive(true);
//             playerHealth.GetComponents<AudioSource>()[3].Stop();
//             primaryGun.SMGFlash.SetActive(false);
//             primaryGun.SMGFlashRay.SetActive(false);

//             secondaryGun.SMGFlash.SetActive(false);
//             secondaryGun.SMGFlashRay.SetActive(false);

//             playerHealth.UpdateAmmo();
//             upgradeMenuAmmo.GetComponent<Text>().text = "<b>AMMO:</b> " + playerHealth.ammo;
//         }
//         else if (primaryGun.currentGun == "laser")
//         {

//             playerHealth.stick.SetActive(false);
//             playerHealth.meleeStick = false;

//             primaryGun.pistol.SetActive(false);
//             primaryGun.SMG.SetActive(false);

//             primaryGun.laserGun.SetActive(true);
//             primaryGun.laserFlash.SetActive(false);
//             primaryGun.laserFlashRay.SetActive(false);
//             primaryGun.laserPointerRay.SetActive(true);

//             secondaryGun.laserFlash.SetActive(false);
//             secondaryGun.laserFlashRay.SetActive(false);

//             playerHealth.UpdateAmmo();
//             upgradeMenuAmmo.GetComponent<Text>().text = "<b>AMMO:</b> " + playerHealth.ammo;
//         }

//         upgradeMenuShowing = true;
//         playerHealth.canShoot = false;
//         playerHealth.currentHealth = 100;

//         // CHECK SDK TYPE
//         if (playerHealth.SDKType == "oculus") playerHealth.healthFlash.SetActive(false);
//         else playerHealth.steamHealthFlash.SetActive(false);

//         sign.SetActive(true);
//         upgradeMenu.SetActive(true);
//         categoryMenu.transform.Find("CategoryMenuTitle").gameObject.GetComponent<Text>().text = "ROUND " + waveCount + " COMPLETE";

//         TemporarilyDisableButtons();

//         roundEndFade = true;

//         // UPDATE UPGRADE MENU STATS
//         upgradeMenuPoints.GetComponent<Text>().text = "<b>POINTS:</b> " + playerHealth.points;
//         upgradeMenuGrenades.GetComponent<Text>().text = "<b>GRENADES:</b> " + playerHealth.grenades;

//         // Destroy all tokens
//         GameObject[] tokens = GameObject.FindGameObjectsWithTag("Token");
//         foreach (GameObject t in tokens) Destroy(t);

//     }

//     public void TemporarilyDisableButtons()
//     {

//         menuButton.enabled = false;
//         menuOtherCategory.enabled = false;
//         menuAmmoCategory.enabled = false;
//         menuGunsCategory.enabled = false;
//         menuPistol.enabled = false;
//         menuSMG.enabled = false;
//         menuLaser.enabled = false;
//         menuAmmoCapacity.enabled = false;
//         menuClipCapacity.enabled = false;
//         menuBulletDamage.enabled = false;
//         menuGrenades.enabled = false;
//         menuHealth.enabled = false;
//         infoScreenButton.enabled = false;

//       Invoke("ReenableButtons", 1);
// }

//     void ReenableButtons()
//     {

//         menuButton.enabled = true;
//         menuOtherCategory.enabled = true;
//         menuAmmoCategory.enabled = true;
//         menuGunsCategory.enabled = true;
//         menuPistol.enabled = true;
//         menuSMG.enabled = true;
//         menuLaser.enabled = true;
//         menuAmmoCapacity.enabled = true;
//         menuClipCapacity.enabled = true;
//         menuBulletDamage.enabled = true;
//         menuGrenades.enabled = true;
//         menuHealth.enabled = true;
//         infoScreenButton.enabled = true;

//     }

//     public void startNextRound()
//     {

//         spiderCount = 0;
//         killedSpiders = 0;
//         waveCount += 1;
//         waveLimit += 4;
//         spawnTime -= 0.3f;

//         playerHealth.FillAmmoClips();

//         if (spawnTime < 1f) spawnTime = 1f;
//         playerHealth.currentHealth = playerHealth.startingHealth;
//         spiderCount = 0;

//         InvokeRepeating("Spawn", spawnTime, spawnTime);

//         if (!battleSongOne.isPlaying && !battleSongTwo.isPlaying)
//         {
//             // FADE IN SONG ONE
//             battleSongOne.volume = 0;
//             battleSongOne.Play();
//             battleSongOne.volume += .13f* Time.deltaTime;
//         }
//         else if (battleSongOne.isPlaying) fadeInSongTwo = true;
//         else if (battleSongTwo.isPlaying) fadeInSongOne = true;

//         sign.SetActive(false);
//         upgradeMenu.SetActive(false);
//         upgradeMenuShowing = false;
//     }

//     public void StartNewGame()
//     {

//         InvokeRepeating("Spawn", spawnTime, spawnTime);

//         spiderCount = 0;
//         killedSpiders = 0;
//         waveCount = 1;
//         waveLimit = 6;
//         spawnTime = 4f;
//         playerHealth.isDead = false;
//         playerHealth.startingHealth = 100;
//         playerHealth.currentHealth = 100;
//         playerHealth.baseAmmo = 60;
//         playerHealth.baseClipSize = 8;
//         playerHealth.ammo = 60;
//         playerHealth.points = 0;
//         playerHealth.score = 0;
//         playerHealth.grenades = 0;
//         playerHealth.ammoCapacityMultiplier = 1;
//         playerHealth.bulletDamageMultiplier = 1;
//         playerHealth.clipCapacityMultiplier = 1;

//         // reset associated button text and descriptions

//         menu.healthTitle.GetComponent<Text>().text = "<b>2X HEALTH</b>\n2000";
//         menu.healthBG.GetComponent<UpgradeItem>().itemPrice = 2000;

//         menu.ammoCapacityBtnText.GetComponent<Text>().text = "BUY - 1200 PTS";
//         menu.ammoCapacityDescription.GetComponent<Text>().text = "1.5X AMMO CAPACITY";
//         menu.ammoCapacityBtnBG.GetComponent<UpgradeItem>().itemPrice = 1200;

//         menu.clipCapacityBtnText.GetComponent<Text>().text = "BUY - 2000 PTS";
//         menu.clipCapacityDescription.GetComponent<Text>().text = "2X CLIP CAPACITY";
//         menu.clipCapacityBtnBG.GetComponent<UpgradeItem>().itemPrice = 2000;

//         menu.bulletDamageBtnText.GetComponent<Text>().text = "BUY - 2000 PTS";
//         menu.bulletDamageDescription.GetComponent<Text>().text = "1.5X BULLET DAMAGE";
//         menu.bulletDamageBtnBG.GetComponent<UpgradeItem>().itemPrice = 2000;

//         primaryGun.currentGun = "pistol";
//         startMenuShowing = false;
//         startMenu.SetActive(false);
//         sign.SetActive(false);
//         bench.SetActive(false);

//         upgradeMenuDoublePistols.SetActive(true);
//         upgradeMenuSMG.SetActive(true);
//         upgradeMenuLaser.SetActive(true);

//         primaryGun.currentGun = "pistol";
//         primaryGun.FillClip();

//         primaryGun.pistol.SetActive(true);
//         primaryGun.SMG.SetActive(false);
//         primaryGun.laserGun.SetActive(false);

//         introFadeOut = true;

//         if (!battleSongOne.isPlaying && !battleSongTwo.isPlaying)
//         {
//             // FADE IN SONG ONE
//             battleSongOne.volume = 0;
//             battleSongOne.Play();
//             battleSongOne.volume += .13f* Time.deltaTime;
//             fadeInSongOne = true;
//         }
//         else if (battleSongOne.isPlaying) fadeInSongTwo = true;
//         else if (battleSongTwo.isPlaying) fadeInSongOne = true;

//     }

//     void Spawn()
//     {

//         if (spiderCount >= waveLimit) CancelInvoke("Spawn");
//         else
//         {

//             // SPAWN SPIDER

//             float maxScale = .3f + (waveCount * .075f);

//             if (maxScale > 1.3f) maxScale = 1.3f;

//             float spiderScale = Random.Range(0.1f, maxScale);

//             int spawnPointIndex = Random.Range(0, spawnPoints.Length);

//             // TURN OFF REAR SPAWN POINT IF OCULUS AND NOT ROOMSCALE
//             if (playerHealth.SDKType == "oculus" && !playerHealth.roomScaleOn)
//             {
//                 spawnPointIndex = Random.Range(0, spawnPoints.Length - 1);
//             }

//             Vector3 spiderSpawnPosition = new Vector3(spawnPoints[spawnPointIndex].position.x, -12.3f * spiderScale, spawnPoints[spawnPointIndex].position.z);

//             if (spiderScale < 0.45f) index = 0;
//             else if (spiderScale >= 0.45f && spiderScale < 0.75f) index = 1;
//             else if (spiderScale >= 0.75f) index = 2;

//             GameObject spider = Instantiate(enemy[index], spiderSpawnPosition, spawnPoints[spawnPointIndex].rotation);

//             if (spiderScale < 0.3f)
//             {
//                 spider.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 4f;
//                 spider.GetComponent<EnemyAttack>().attackDamage = 15;

//                 spider.GetComponent<EnemyHealth>().startingHealth = (int)Mathf.Round(spiderScale * 10) * 15;
//                 spider.GetComponent<EnemyHealth>().currentHealth = (int)Mathf.Round(spiderScale * 10) * 15;

//             }
//             else if (spiderScale >= 0.3f && spiderScale < 0.6)
//             {
//                 spider.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 3.5f;
//                 spider.GetComponent<EnemyAttack>().attackDamage = 20;

//                 spider.GetComponent<EnemyHealth>().startingHealth = (int)Mathf.Round(spiderScale * 10) * 15;
//                 spider.GetComponent<EnemyHealth>().currentHealth = (int)Mathf.Round(spiderScale * 10) * 15;

//             }
//             else if (spiderScale >= 0.6f)
//             {
//                 spider.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 3f;
//                 spider.GetComponent<EnemyAttack>().attackDamage = 25;

//                 spider.GetComponent<EnemyHealth>().startingHealth = (int)Mathf.Round(spiderScale * 10) * 15;
//                 spider.GetComponent<EnemyHealth>().currentHealth = (int)Mathf.Round(spiderScale * 10) * 15;
//             }

//             spider.transform.SetParent(spidersContainer.transform);
//             spider.transform.localScale = new Vector3(spiderScale, spiderScale, spiderScale);

//             if (Random.Range(1, 15) == 1) spider.GetComponent<EnemyAttack>().hasToken = true;

//             spider.GetComponent<EnemyAttack>().isRising = true;

//             spiderCount += 1;

//         }

//     }

//     public void inflictDamage(GameObject spider, string type, GameObject weapon, bool headshot = false)
//     {

//         if (type == "gunshot")
//         {
//             spider.GetComponent<Rigidbody>().velocity = weapon.transform.forward * 20;
//             attackDamage = Mathf.RoundToInt(10 * playerHealth.bulletDamageMultiplier);
//             if (headshot) attackDamage *= 2;
//         }
//         else if (type == "laser")
//         {
//             spider.GetComponent<Rigidbody>().velocity = weapon.transform.forward * 15;
//             attackDamage = Mathf.RoundToInt(80 * playerHealth.bulletDamageMultiplier);

//             if (spider.GetComponent<EnemyHealth>().currentHealth <= attackDamage) attackDamage = spider.GetComponent<EnemyHealth>().currentHealth;

//         }
//         else if (type == "SMG")
//         {
//             spider.GetComponent<Rigidbody>().velocity = weapon.transform.forward * 5;
//             attackDamage = Mathf.RoundToInt(10 * playerHealth.bulletDamageMultiplier);
//             if (headshot) attackDamage *= 2;
//         }
//         else if (type == "melee")
//         {
//             spider.GetComponent<Rigidbody>().velocity = (spider.transform.position - weapon.transform.position) * 5;
//             attackDamage = Mathf.RoundToInt(15 * playerHealth.bulletDamageMultiplier);
//         }
//         else if (type == "grenade")
//         {
//             spider.GetComponent<Rigidbody>().velocity = (spider.transform.position - weapon.transform.position) * 25;
//             attackDamage = 100;

//             if (spider.GetComponent<EnemyHealth>().currentHealth <= attackDamage) attackDamage = spider.GetComponent<EnemyHealth>().currentHealth;
//         }

//         EnemyAttack enemyIsDead = spider.GetComponent<EnemyAttack>();

//         if (!enemyIsDead.isDead)
//         {
//             showPoints(spider, "+" + attackDamage + " PTS");
//             GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().points += attackDamage;
//             GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().score += attackDamage;
//         }

//         spider.GetComponent<EnemyMovement>().enabled = true;

//         if (enemyIsDead != null)
//         {
//             bool isDead = enemyIsDead.isDead;

//             if (!isDead)
//             {

//                 spider.GetComponent<EnemyHealth>().currentHealth -= attackDamage;

//                 if (spider.GetComponent<EnemyHealth>().currentHealth <= 0)
//                 {

//                     if (type == "melee") spider.GetComponent<EnemyAttack>().killedByMelee = true;

//                     // ENEMY IS DEAD
//                     enemyIsDead.isDead = true;
//                     killedSpiders += 1;
//                     Animator anim = spider.GetComponent<Animator>();
//                     anim.SetTrigger("Dead");

//                     //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().points += spider.GetComponent<EnemyHealth>().pointValue;
//                     //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().score += spider.GetComponent<EnemyHealth>().pointValue;

//                     // HIDE HEALTH BAR
//                     //Component[] comps = spider.GetComponentsInChildren(typeof(Transform), true);
//                     //foreach (Component c in comps)
//                     //{
//                     //    if (c.name == "HealthBar(Clone)") Destroy(c.gameObject);
//                     //}

//                     checkForToken(spider);

//                     if (this.gameObject != null)
//                     {
//                         if (killedSpiders == waveLimit) endWave();
//                     }
//                 }
//                 //else showHealthBar(spider);
//             }
//         }
//     }



//     void checkForToken(GameObject spider)
//     {

//         EnemyAttack enemyIsDead = spider.GetComponent<EnemyAttack>();

//         if (enemyIsDead.hasToken)
//         {

//             Vector3 tokenPosition = new Vector3(spider.transform.position.x, spider.transform.position.y + 0.5f, spider.transform.position.z);

//             if (enemyIsDead.killedByMelee) {
//                 // KILLED BY MELEE, SPAWN THE TOKEN CLOSER TO THE PLAYER

//                 tokenPosition = player.transform.position + player.transform.forward * 1.2f;
//                 tokenPosition = new Vector3(tokenPosition.x, .8f, tokenPosition.z);
//             }
            

//             int num = Random.Range(1, 10);

//             if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().currentHealth == 100)
//             {
//                 // PLAYER HAS FULL HEALTH, GENERATE TRIPLE SHOT ONLY
//                 GameObject tst = Instantiate(ammoToken, tokenPosition, Quaternion.identity);
//                 Destroy(tst, 10);
//             }
//             else
//             {
//                 // RANDOMLY CHOOSE BETWEEN HEALTH AND TRIPLE SHOT
//                 if (num > 7)
//                 {
//                     GameObject tst = Instantiate(ammoToken, tokenPosition, Quaternion.identity);
//                     Destroy(tst, 10);
//                 }
//                 else
//                 {
//                     GameObject ht = Instantiate(healthToken, tokenPosition, Quaternion.identity);
//                     Destroy(ht, 10);
//                 }

//             }

//         }
//     }

//     void showPoints(GameObject spider, string textNotice)
//     {
//         Vector3 noticePosition = new Vector3(spider.transform.position.x, (spider.transform.lossyScale.y * 2.3f) + spider.transform.position.y, spider.transform.position.z);
//         GameObject tempNotice = Instantiate(notice, noticePosition, Quaternion.LookRotation(transform.position - GameObject.FindGameObjectWithTag("Player").transform.position));
//         tempNotice.GetComponentInChildren<Text>().text = textNotice;

//         float distance = Vector3.Distance(tempNotice.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
//         float scale = .004f + (distance * .0006f);
//         tempNotice.transform.localScale = new Vector3(scale, scale, scale);

//         tempNotice.transform.SetParent(spider.transform);

//         Destroy(tempNotice, .5f);


//     }

//     void showNotice(GameObject spider, string textNotice)
//     {

//         bool noticeExists = false;

//         Component[] comps = spider.GetComponentsInChildren(typeof(Transform), true);
//         foreach (Component c in comps)
//         {
//             if (c.name == "Notice(Clone)") {
//                 // NOTICE ALREADY EXISTS, CHANGE TEXT ONLY
//                 noticeExists = true;
//                 c.GetComponentInChildren<Text>().text = textNotice;
//             }
//         }

//         if (!noticeExists)
//         {
//             // NOTICE DOESN'T EXIST, CREATE NEW
//             Vector3 noticePosition = new Vector3(spider.transform.position.x, (spider.transform.lossyScale.y * 2.3f) + spider.transform.position.y, spider.transform.position.z);

//             GameObject tempNotice = Instantiate(notice, noticePosition, Quaternion.LookRotation(transform.position - GameObject.FindGameObjectWithTag("Player").transform.position));

//             tempNotice.transform.SetParent(spider.transform);
//             spider.GetComponent<EnemyHealth>().notice = tempNotice;
//         }

//         spider.GetComponent<EnemyHealth>().notice.GetComponent<HealthBarAngle>().lastDestroyCall = Time.time;

//         StartCoroutine(HideNotice(spider.GetComponent<EnemyHealth>().notice, spider, .3F));
//     }

//     void showHealthBar(GameObject spider)
//     {

//         bool healthBarExists = false;

//         Component[] comps = spider.GetComponentsInChildren(typeof(Transform), true);
//         foreach (Component c in comps)
//         {
//             if (c.name == "HealthBar(Clone)") { healthBarExists = true; }
//         }

//         if (!healthBarExists)
//         {
//             Vector3 healthBarPosition = new Vector3(spider.transform.position.x, (spider.transform.lossyScale.y * 2.3f) + spider.transform.position.y, spider.transform.position.z);

//             GameObject tempHealthBar = Instantiate(healthBar, healthBarPosition, Quaternion.LookRotation(transform.position - GameObject.FindGameObjectWithTag("Player").transform.position));

//             tempHealthBar.transform.SetParent(spider.transform);
//             tempHealthBar.transform.localScale = new Vector3((float)spider.GetComponent<EnemyHealth>().currentHealth / (float)spider.GetComponent<EnemyHealth>().startingHealth * 5, 0.3f, 0.01f);
//             spider.GetComponent<EnemyHealth>().healthBar = tempHealthBar;
//         } else
//         {
//             spider.GetComponent<EnemyHealth>().healthBar.transform.localScale = new Vector3((float)spider.GetComponent<EnemyHealth>().currentHealth / (float)spider.GetComponent<EnemyHealth>().startingHealth * 5, 0.3f, 0.01f);
//         }

//         spider.GetComponent<EnemyHealth>().healthBar.GetComponent<HealthBarAngle>().lastDestroyCall = Time.time;

//         StartCoroutine(HideHealthBar(spider.GetComponent<EnemyHealth>().healthBar, spider, 2f));
//     }

//     IEnumerator HideHealthBar(GameObject tHealthbar, GameObject spider, float delayTime)
//     {
//         yield
//         return new WaitForSeconds(delayTime);

//         if (Time.time - tHealthbar.GetComponent<HealthBarAngle>().lastDestroyCall > 2f)
//         {
//             Destroy(tHealthbar);
//         }

//     }

//     IEnumerator HideNotice(GameObject tNotice, GameObject spider, float delayTime)
//     {
//         yield
//         return new WaitForSeconds(delayTime);

//         if (Time.time - tNotice.GetComponent<HealthBarAngle>().lastDestroyCall > delayTime)
//         {
//             Destroy(tNotice);
//         }

//     }

}