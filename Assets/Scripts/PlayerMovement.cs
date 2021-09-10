using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Steamworks;

public class PlayerMovement : MonoBehaviour
{
    //knockback help from https://answers.unity.com/questions/242648/force-on-character-controller-knockback.html
    //footstep noise help from https://www.youtube.com/watch?v=ih8gyGeC7xs&ab_channel=EYEmaginary
    //shake code from https://www.youtube.com/watch?v=kzHHAdvVkto
    //stop watch help from https://www.youtube.com/watch?v=T1HBdQSEM-4

    public UnityEngine.CharacterController controller;

    public static PlayerMovement instance;

    public GameObject confetti;

    public MouseLook mouse;

    private Rigidbody rb;

    public Animator camAnim;

    public GameObject cam;

    public float defaultSpeed = 12f;
    public float speed = 12f;
    public float gravity = -9.81f;

    public float time;
    public float maxTime;
    public bool pressedJump;

    public Transform groundCheck;
    public float groundDistance = 0.4f; 
    public LayerMask groundMask;
    public float jumpHight = 3f;

    [SerializeField] private float dashForce = 50f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float timeBetweenDashes = 1f;
    private bool canDash = true;
    [SerializeField] ParticleSystem DashEffect;

    Vector3 velocity;
    bool isGrounded;
    public bool cantMove;
    public bool canRespawn;
    public bool isDead;
    public bool onLadder;
    public bool paused;
    public bool freezeMouse;

    public int health;
    public int maxHealth;
    public int amount = 1;

    public TMP_Text healthText;
    public GameObject damagePic;
    public GameObject gainHealthPic;
    public GameObject gainAmmoPic;
    public GameObject deadScreen;
    public GameObject deadText;
    public GameObject dangerScreen;
    public GameObject checkText;

    public GameObject keyText;
    public TMP_Text keysRequiredText;

    public GameObject player;
    public GameObject checkpoint;
    public GameObject miniCheckPoint;

    public GameObject dropSahdow;
    public GameObject resumeButt;
    public GameObject quitButt;
    public GameObject settingsButt;
    public GameObject respawnButt;

    public int KeyAmount;

    public PointManager point;

    public AudioClip ammoPickup;
    public AudioClip healthPickup;
    public AudioClip weaponPickup;
    public AudioClip keyPickup;
    public AudioSource pickups;
    public AudioClip death;
    public AudioClip checkNoise;
    public AudioClip presPickup;

    float mass = 3.0F; // defines the character mass
    Vector3 impact = Vector3.zero;

    //reloading player things
    public float pointX;
    public float pointY;
    public float pointZ;
    public Scene curScene;
    public string sceneName;
    public int priorHealth;
    public int priorPistolAmmo;
    public int priorShotGunAmmo;
    public int priorMachineGunAmmo;
    public int priorGrenadeAmmo;
    public int priorFireWorkAmmo;
    public int priorPoints;

    public int hurtNum;

    public int chooseNewNoise;

    public AudioClip hurt1;
    public AudioClip hurt2;
    public AudioClip dashNoise;
    public AudioClip jumpNoise;
    public AudioClip moving1;
    public AudioClip moving2;
    public AudioClip moving3;
    public AudioClip moving4;
    public AudioClip moving5;
    public AudioClip moving6;

    public AudioSource sound;
    public AudioSource moveNoise;
    public AudioSource jumping;
    public float pitch;

    public Transform healthPos;
    Vector3 ghostInitialPosition;
    float shakeTime = 0.05f;
    float shakeAmount = 5f;
    public GameObject ghost;


    public int jumpNum;
    public int medic;
    public int munitions;
    public int deathCounter;
    public int noDeathCounter;
    public float distance;

    public bool noHit;

    public int presentsFound;

    public float timer;
    public float seconds;
    public float minutes;
    public float hours;
    public GameObject timeText;
    public TextMeshProUGUI timerText;

    public bool showTimer;

    public bool isDeadArena;

    public bool canInstaKill;
    public bool dontLoseAmmo;

    public float duration;
    public float duration2;
    public float duration3;
    public GameObject speedImage;
    public Image fillImageSpeed;
    public GameObject instaImage;
    public Image fillImageInsta;
    public GameObject infiniteImage;
    public Image fillImageInfinite;

    public int levelOneDone;
    public int levelTwoDone;
    public int levelThreeDone;
    public int levelFourDone;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        cantMove = false;
        health = PlayerPrefs.GetInt("Curr_Health", 50);
        damagePic.SetActive(false);
        pressedJump = true;
        jumping.clip = jumpNoise;
        point = GameObject.FindGameObjectWithTag("PointManager").GetComponent<PointManager>();
        //moveNoise.clip = moving;
        newWalk();

        //player.transform.position = new Vector3(pointX, pointY, pointZ);
        //ResetPos();
        curScene = SceneManager.GetActiveScene();
        sceneName = curScene.name;
        /*pointX = PlayerPrefs.GetFloat("CheckPointX");
        pointY = PlayerPrefs.GetFloat("CheckPointY");
        pointZ = PlayerPrefs.GetFloat("CheckPointZ");*/
        pointX = PlayerPrefs.GetFloat("CheckPointX");
        pointY = PlayerPrefs.GetFloat("CheckPointY");
        pointZ = PlayerPrefs.GetFloat("CheckPointZ");
        priorHealth = PlayerPrefs.GetInt("PriorH");
        priorPistolAmmo = PlayerPrefs.GetInt("PriorP");
        priorShotGunAmmo = PlayerPrefs.GetInt("PriorS");
        priorMachineGunAmmo = PlayerPrefs.GetInt("PriorM");
        priorGrenadeAmmo = PlayerPrefs.GetInt("PriorG");
        priorFireWorkAmmo = PlayerPrefs.GetInt("PriorF");
        priorPoints = PlayerPrefs.GetInt("PriorScore");
        health = priorHealth;
        PlayerPrefs.SetInt("Current_Score", priorPoints);
        PlayerPrefs.SetInt("Pistol_Ammo", priorPistolAmmo);
        PlayerPrefs.SetInt("Shotgun_Ammo", priorShotGunAmmo);
        PlayerPrefs.SetInt("Machinegun_Ammo", priorMachineGunAmmo);
        PlayerPrefs.SetInt("Grenade_Ammo", priorGrenadeAmmo);
        PlayerPrefs.SetInt("Firework_Ammo", priorFireWorkAmmo);
        mouse.currAmmoP = priorPistolAmmo;
        mouse.currAmmoS = priorShotGunAmmo;
        mouse.currAmmoM = priorMachineGunAmmo;
        mouse.currAmmoG = priorGrenadeAmmo;
        mouse.currAmmoF = priorFireWorkAmmo;
        point.totalPoints = priorPoints;
        //this line of code may fix weird cursor issue. delete if it doesnt 
        Cursor.lockState = CursorLockMode.Locked;
        //blocking this off
        player.transform.position = new Vector3(pointX, pointY, pointZ);
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Arena"))
        {
            PlayerPrefs.SetInt("Pistol_Ammo", 100);
        }
        Physics.SyncTransforms();

        jumpNum = PlayerPrefs.GetInt("JUMP", 0);
        medic = PlayerPrefs.GetInt("HEALTH", 0);
        munitions = PlayerPrefs.GetInt("AMMO", 0);
        deathCounter = PlayerPrefs.GetInt("DEATH", 0);
        noDeathCounter = PlayerPrefs.GetInt("GOD", 0);
        distance = PlayerPrefs.GetFloat("DIST", 0);
        presentsFound = PlayerPrefs.GetInt("Presents", 0);
        noHit = PlayerPrefs.GetInt("NOHIT", 0) > 0;

        timer = PlayerPrefs.GetFloat("TIMER", 0);
        seconds = PlayerPrefs.GetFloat("SECONDS", 0);
        minutes = PlayerPrefs.GetFloat("MINUTES", 0);
        hours = PlayerPrefs.GetFloat("HOURS", 0);

        levelOneDone = PlayerPrefs.GetInt("LevOne", 0);
        levelTwoDone = PlayerPrefs.GetInt("LevTwo", 0);
        levelThreeDone = PlayerPrefs.GetInt("LevThree", 0);
        levelFourDone = PlayerPrefs.GetInt("LevFour", 0);

        showTimer = PlayerPrefs.GetInt("SHOWME", 0) > 0;
        showTimer = false;



    }

    // Update is called once per frame
    void Update()
    {
        StopWatchCalculations();

        //achivement variable progress
        PlayerPrefs.SetInt("JUMP", jumpNum);
        PlayerPrefs.SetInt("HEALTH", medic);
        PlayerPrefs.SetInt("AMMO", munitions);
        PlayerPrefs.SetInt("DEATH", deathCounter);
        PlayerPrefs.SetFloat("DIST", distance);
        PlayerPrefs.SetInt("GOD", noDeathCounter);
        PlayerPrefs.SetInt("Presents", presentsFound);

        PlayerPrefs.SetFloat("TIMER", timer);
        PlayerPrefs.SetFloat("SECONDS", seconds);
        PlayerPrefs.SetFloat("MINUTES", minutes);
        PlayerPrefs.SetFloat("HOURS", hours);

        int noHitEnable;
        noHitEnable = noHit ? 1 : 0;

        if (noHit == true)
        {
            PlayerPrefs.SetInt("NOHIT", 1);
        }
        else
        {
            PlayerPrefs.SetInt("NOHIT", 0);
        }


        int EnableTime;
        EnableTime = showTimer ? 1 : 0;
        if (showTimer == true){
            PlayerPrefs.SetInt("SHOWME", 1);
            timeText.SetActive(true);
        }
        else{
            PlayerPrefs.SetInt("SHOWME", 0);
            timeText.SetActive(false);
        }

        PlayerPrefs.SetInt("Curr_Health", health);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            pressedJump = false;
            time = 0;
        }

        if (cantMove == false || onLadder == false)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if (onLadder == false)
            {
                Vector3 move = transform.right * x * speed + transform.forward * z * speed + transform.up * velocity.y;

                controller.Move(move * Time.deltaTime);
                distance += 0.5f * Time.deltaTime;
            }
            if (onLadder == true)
            {
                Vector3 moveUp = transform.up * z;
                controller.Move(transform.up * speed * Time.deltaTime);
            }
            //Dash
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash == true && x > 0f || Input.GetKeyDown(KeyCode.LeftShift) && canDash == true && x < 0f || Input.GetKeyDown(KeyCode.LeftShift) && canDash == true && z > 0f || Input.GetKeyDown(KeyCode.LeftShift) && canDash == true && z < 0f)
            {
                DashEffect.Play();
                canDash = false;
                sound.clip = dashNoise;
                sound.Play();
                StartCoroutine(Dash());
                StartCoroutine(DashRecharge());
            }
        }
        //Jump
        if (Input.GetButtonDown("Jump") && isGrounded && pressedJump == false && onLadder == false && isDead == false)
        {
            velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);
            pressedJump = true;
            jumping.Play();
            setPitch();
            jumpNum++;
        }

        if (pressedJump == false && isGrounded == false && isDead == false)
        {
            time += Time.deltaTime;
            if (time <= maxTime && onLadder == false)
            {
                if (Input.GetButtonDown("Jump") && pressedJump == false)
                {
                    velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);
                    pressedJump = true;
                    jumping.Play();
                    setPitch();
                    time = 0;
                }
                else if (time >= maxTime)
                {
                    pressedJump = true;
                    time = 0;
                }
            }
        }
        if (onLadder == false)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded == true && controller.velocity.magnitude > 2f && moveNoise.isPlaying == false) {
                moveNoise.Play();
            }

            if (controller.isGrounded == true && controller.velocity.magnitude < 2f && moveNoise.isPlaying == true)
            {
                moveNoise.Stop();
            }

            
        
            //achievement stuff
            if (jumpNum >= 300){
                if (!SteamManager.Initialized) { return; }
                SteamUserStats.SetAchievement("B_Hopper");
                SteamUserStats.StoreStats();
                PlayerPrefs.SetInt("ACH_12", 1);
            }

            if (medic >= 1000){
                if (!SteamManager.Initialized) { return; }
                SteamUserStats.SetAchievement("Combat_Medic");
                SteamUserStats.StoreStats();
                PlayerPrefs.SetInt("ACH_15", 1);
            }

            if (munitions >= 100){
                if (!SteamManager.Initialized) { return; }
                SteamUserStats.SetAchievement("Munition_Man");
                SteamUserStats.StoreStats();
                PlayerPrefs.SetInt("ACH_16", 1);
            }

            if (deathCounter == 1){
                if (!SteamManager.Initialized) { return; }
                SteamUserStats.SetAchievement("Immortality_Is_Overrated");
                SteamUserStats.StoreStats();
                PlayerPrefs.SetInt("ACH_19", 1);
            }

            if (deathCounter == 10)
            {
                if (!SteamManager.Initialized) { return; }
                SteamUserStats.SetAchievement("Maybe_Not_So_Easy");
                SteamUserStats.StoreStats();
                PlayerPrefs.SetInt("ACH_20", 1);
            }

            if (distance >= 666){
                if (!SteamManager.Initialized) { return; }
                SteamUserStats.SetAchievement("Explorer");
                SteamUserStats.StoreStats();
                PlayerPrefs.SetInt("ACH_13", 1);
            }

            if (presentsFound == 1){
                if (!SteamManager.Initialized) { return; }
                SteamUserStats.SetAchievement("What's_This");
                SteamUserStats.StoreStats();
                PlayerPrefs.SetInt("ACH_22", 1);
            }

            if (presentsFound == 6)
            {
                if (!SteamManager.Initialized) { return; }
                SteamUserStats.SetAchievement("The_Collector");
                SteamUserStats.StoreStats();
                PlayerPrefs.SetInt("ACH_23", 1);
            }

            
        

        if (fillImageSpeed.fillAmount > 0)
        {
            duration -= Time.deltaTime;
            fillImageSpeed.fillAmount = duration / 20f;
        }

        if (fillImageInsta.fillAmount > 0){
            duration2 -= Time.deltaTime;
            fillImageInsta.fillAmount = duration2 / 20f;
        }

        if (fillImageInfinite.fillAmount > 0){
            duration3 -= Time.deltaTime;
            fillImageInfinite.fillAmount = duration3 / 20f;
        }

        if (health < 30){
            dangerScreen.SetActive(true);
        }

        if (health >= 30){
            dangerScreen.SetActive(false);
        }

        //Controls camera bob while moving and jumping
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            camAnim.SetBool("IsMoving", true);
        } else 
        {
            camAnim.SetBool("IsMoving", false);
        }

        if (isGrounded == false)
        {
            camAnim.SetBool("IsMoving", false);
        }

        healthText.text = "Health: " + health;

        if (health <= 0){
            //StartCoroutine(respawnTimer());
            //health = 5;
        }

        if (health > maxHealth){
            health = maxHealth;
        }
        RaycastHit ray;

        if (Physics.Raycast(transform.position, -transform.up, out ray))
        {
            if (ray.collider != null){
                 dropSahdow.transform.position = ray.point;
            }
        }

        if (isDead == true && Input.GetKey(KeyCode.Mouse0) && mouse.unpaused == true && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("ArenaV2"))
        {
            
            Time.timeScale = 1f;
            
            ResetPos();
            /*health = 50;
            point.totalPoints -= 1000;
            isDead = false;
            freezeMouse = false;
            deadText.SetActive(false);
            mouse.unpaused = false;
            ghost.transform.position = ghostInitialPosition;*/
            SceneManager.LoadScene(sceneName);
        }

        if (isDead == true && isDeadArena == false && Input.GetKey(KeyCode.Mouse0) && mouse.unpaused == true && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Arena"))
        {

            Time.timeScale = 1f;

            ResetPos();
            /*health = 50;
            point.totalPoints -= 1000;
            isDead = false;
            freezeMouse = false;
            deadText.SetActive(false);
            mouse.unpaused = false;
            ghost.transform.position = ghostInitialPosition;*/
            SceneManager.LoadScene(sceneName);
        }

        if (Input.GetKey(KeyCode.Escape) && paused == false){
            mouse.unpaused = false;
            Time.timeScale = 0f;
            paused = true;
            freezeMouse = true;
            //mouse.shootReady = false;
            resumeButt.SetActive(true);
            quitButt.SetActive(true);
            settingsButt.SetActive(true);
            respawnButt.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }

        if (impact.magnitude > 0.2F) controller.Move(impact * Time.deltaTime);
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }

    public void AddImpact(Vector3 dir, float force){
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
        impact += dir.normalized * force / mass;
    }

    public void StopWatchCalculations(){
        timer += Time.deltaTime;
        seconds = (int)(timer % 60);
        minutes = (int)((timer / 60) % 60);
        hours = (int)(timer / 3600);

        timerText.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void ResumeGame(){
        paused = false;
        freezeMouse = false;
        Cursor.lockState = CursorLockMode.Locked;
        resumeButt.SetActive(false);
        quitButt.SetActive(false);
        settingsButt.SetActive(false);
        respawnButt.SetActive(false);
        Time.timeScale = 1f;
        mouse.unpaused = true;
        
    }

    public void respawnTime(){
        SceneManager.LoadScene(sceneName);
        ResetPos();
        Time.timeScale = 1f;
    }

    public void setPitch(){
        pitch = Random.Range(0.75f, 0.90f);
        jumping.pitch = pitch;
    }

    public IEnumerator Dash()
    {
        //rb.AddForce(Camera.main.transform.forward * dashForce, ForceMode.VelocityChange);

        //rb.velocity = Vector3.zero;
        //rb.angularVelocity = Vector3.zero;
        velocity.y = 0f;

        speed = dashForce;

        yield return new WaitForSeconds(dashDuration);

        speed = defaultSpeed;

        velocity.y = 0f;
        //rb.velocity = Vector3.zero;
    }

    public IEnumerator DashRecharge()
    {
        yield return new WaitForSeconds(timeBetweenDashes);
        canDash = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Boss"){
            Vector3 direction = collision.transform.position - transform.position;
            AddImpact(direction, 300f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Get"){
            mouse.shootEnabled = true;
        }

        if (other.gameObject.tag == "Ladder"){
            onLadder = true;
        }

        if (other.gameObject.tag == "LadderTop"){
            onLadder = false;
        }

        if (other.gameObject.tag == "Shotgun"){
            mouse.wep.anim.SetTrigger("WeaponSwitch");
            mouse.shotGunBool = true;
            mouse.currWeapon = 2;
            mouse.useShotgun = true;
            pickups.clip = weaponPickup;
            pickups.Play();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Rifle")
        {
            mouse.wep.anim.SetTrigger("WeaponSwitch");
            mouse.machineGunBool = true;
            mouse.currWeapon = 3;
            mouse.useMachineGun = true;
            pickups.clip = weaponPickup;
            pickups.Play();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Knife")
        {
            mouse.wep.anim.SetTrigger("WeaponSwitch");
            mouse.knifeBool = true;
            mouse.currWeapon = 4;
            mouse.useKnife = true;
            pickups.clip = weaponPickup;
            pickups.Play();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Grenade")
        {
            mouse.wep.anim.SetTrigger("WeaponSwitch");
            mouse.grenadeBool = true;
            mouse.currWeapon = 5;
            mouse.useGrenade = true;
            pickups.clip = weaponPickup;
            pickups.Play();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "FireWork")
        {
            mouse.wep.anim.SetTrigger("WeaponSwitch");
            mouse.fireWorkBool = true;
            mouse.currWeapon = 6;
            mouse.useFirework = true;
            pickups.clip = weaponPickup;
            pickups.Play();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Checkpoint" && other.gameObject.GetComponent<HitCheckPoint>().hit == false){
            //checkpoint = other.gameObject;
            Physics.SyncTransforms();
            StartCoroutine(flashCheck());
            pickups.clip = checkNoise;
            pickups.Play();
            pointX = gameObject.transform.position.x;
            pointY = gameObject.transform.position.y;
            pointZ = gameObject.transform.position.z;
            float positionX = pointX;
            float positionY = pointY;
            float positionZ = pointZ;
            priorHealth = health;
            priorPistolAmmo = mouse.currAmmoP;
            priorShotGunAmmo = mouse.currAmmoS;
            priorMachineGunAmmo = mouse.currAmmoM;
            priorGrenadeAmmo = mouse.currAmmoG;
            priorFireWorkAmmo = mouse.currAmmoF;
            priorPoints = point.totalPoints;
            PlayerPrefs.SetFloat("CheckPointX", positionX);
            PlayerPrefs.SetFloat("CheckPointY", positionY);
            PlayerPrefs.SetFloat("CheckPointZ", positionZ);
            PlayerPrefs.SetInt("PriorH", priorHealth);
            PlayerPrefs.SetInt("PriorP", priorPistolAmmo);
            PlayerPrefs.SetInt("PriorS", priorShotGunAmmo);
            PlayerPrefs.SetInt("PriorM", priorMachineGunAmmo);
            PlayerPrefs.SetInt("PriorG", priorGrenadeAmmo);
            PlayerPrefs.SetInt("PriorF", priorFireWorkAmmo);
            PlayerPrefs.SetInt("PriorScore", priorPoints);
            other.gameObject.GetComponent<HitCheckPoint>().hit = true;
            /*CheckPoints check = other.transform.GetComponent<CheckPoints>();
            check.Save();*/
        }

        if (other.gameObject.tag == "MiniCheck"){
            checkpoint = other.gameObject;
        }

        if (other.gameObject.tag == "Pit"){
            StartCoroutine(respawnTimer());
            Debug.Log("hi");
        }

        if (other.gameObject.tag == "Present"){
            Instantiate(confetti, other.transform.position, Quaternion.identity);
            PresentManager pres = other.transform.GetComponent<PresentManager>();
            if (pres != null && pres.tutorialFound == false && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Tutorial Level"))
            {
                presentsFound++;
                pickups.clip = presPickup;
                pickups.Play();
                pres.tutorialFound = true;
            }
            if (pres != null && pres.level1Found == false && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelOne")){
                presentsFound++;
                pickups.clip = presPickup;
                pickups.Play();
                pres.level1Found = true;
            }
            if (pres != null && pres.level2Found == false && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelTwo"))
            {
                presentsFound++;
                pickups.clip = presPickup;
                pickups.Play();
                pres.level2Found = true;
            }
            if (pres != null && pres.level3Found == false && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelThree"))
            {
                presentsFound++;
                pickups.clip = presPickup;
                pickups.Play();
                pres.level3Found = true;
            }
            if (pres != null && pres.level4Found == false && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelFour"))
            {
                presentsFound++;
                pickups.clip = presPickup;
                pickups.Play();
                pres.level4Found = true;
            }
        }

        if (other.gameObject.tag == "ArenaStart"){
            StartArena start = other.transform.GetComponent<StartArena>();
            if (start != null){
                start.hasPassed = true;
            }
        }

        if (other.gameObject.tag == "Consumable"){
            Consumables consume = other.transform.GetComponent<Consumables>();
            if (consume != null && consume.isHealth == true && health < 100){
                health += consume.amount;
                medic += consume.amount;
                pickups.clip = healthPickup;
                pickups.Play();
                ShakeIt();
                StartCoroutine(flashGainHealth());
                Destroy(other.gameObject);
            }
            
            if (consume != null && consume.isAmmo == true){
                
                    mouse.currAmmoP += consume.amount;
                    mouse.currAmmoS += consume.amount;
                    mouse.currAmmoM += 25;
                munitions++;
                Destroy(other.gameObject);
                pickups.clip = ammoPickup;
                pickups.Play();
                mouse.ShakeIt();
                StartCoroutine(flashAmmo());
                
            }

            if (consume != null && consume.isKey == true){
                KeyAmount += consume.amount;
                pickups.clip = keyPickup;
                pickups.Play();
                Destroy(other.gameObject);
            }

            if (consume != null && consume.isRocket == true && mouse.currAmmoF < 8){
                mouse.currAmmoF += consume.amount;
                pickups.clip = ammoPickup;
                pickups.Play();
                StartCoroutine(flashAmmo());
                Destroy(other.gameObject);
            }

            if (consume != null && consume.isGrenade == true && mouse.currAmmoG < 6){
                mouse.currAmmoG += consume.amount;
                pickups.clip = ammoPickup;
                pickups.Play();
                StartCoroutine(flashAmmo());
                Destroy(other.gameObject);
            }

            if (consume != null && consume.isMax == true){
                mouse.currAmmoP += 100;
                mouse.currAmmoS += 100; 
                mouse.currAmmoM += 100;
                mouse.currAmmoF += 100;
                mouse.currAmmoG += 100;
                pickups.clip = ammoPickup;
                pickups.Play();
                StartCoroutine(flashAmmo());
                Destroy(other.gameObject);
            }

            if (consume != null && consume.isSpeed == true){
                StartCoroutine(GoFast());
                pickups.Play();
                pickups.clip = ammoPickup;
                StartCoroutine(flashAmmo());
                //StartCoroutine(TimerSpeed(20));
                Destroy(other.gameObject);
            }

            if (consume != null && consume.isInstaKill == true){
                StartCoroutine(KILL());
                //StartCoroutine(TimerInsta());
                pickups.clip = ammoPickup;
                pickups.Play();
                StartCoroutine(flashAmmo());
                Destroy(other.gameObject);
            }

            if (consume != null && consume.isInfinite == true){
                StartCoroutine(MAX());
                //StartCoroutine(TimerInfinite());
                pickups.clip = ammoPickup;
                pickups.Play();
                StartCoroutine(flashAmmo());
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.tag == "PistolAmmo"){
            mouse.currAmmoP += 2;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "ShotGunAmmo"){
            mouse.currAmmoS += 2;
            Destroy(other.gameObject);
        }
        
        if (other.gameObject.tag == "MachineGunAmmo"){
            mouse.currAmmoM += 2;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Mus"){
            EnableMusic mus = other.transform.GetComponent<EnableMusic>();
            mus.mus.SetActive(true);
        }

        if (other.gameObject.tag == "End"){
            priorHealth = health;
            priorPistolAmmo = mouse.currAmmoP;
            priorShotGunAmmo = mouse.currAmmoS;
            priorMachineGunAmmo = mouse.currAmmoM;
            priorGrenadeAmmo = mouse.currAmmoG;
            priorFireWorkAmmo = mouse.currAmmoF;
            priorPoints = point.totalPoints;
            PlayerPrefs.SetInt("PriorH", priorHealth);
            PlayerPrefs.SetInt("PriorP", priorPistolAmmo);
            PlayerPrefs.SetInt("PriorS", priorShotGunAmmo);
            PlayerPrefs.SetInt("PriorM", priorMachineGunAmmo);
            PlayerPrefs.SetInt("PriorG", priorGrenadeAmmo);
            PlayerPrefs.SetInt("PriorF", priorFireWorkAmmo);
            PlayerPrefs.SetInt("PriorScore", priorPoints);
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Tutorial Level"))
            {
                SceneManager.LoadScene("LevelOneLoadScreen");
            }
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelOne"))
            {
                if (noHit == false){
                    if (!SteamManager.Initialized) { return; }
                    SteamUserStats.SetAchievement("Clean_Run");
                    SteamUserStats.StoreStats();
                    PlayerPrefs.SetInt("ACH_17", 1);
                }
                PlayerPrefs.SetInt("LevOne", 1);
                SceneManager.LoadScene("LevelTwoLoadScreen");
                //Cursor.lockState = CursorLockMode.None;
                if (!SteamManager.Initialized) { return; }
                SteamUserStats.SetAchievement("Tourist_One");
                SteamUserStats.StoreStats();
                PlayerPrefs.SetInt("ACH_8", 1);
                
            }
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelTwo")){
                if (noHit == false)
                {
                    if (!SteamManager.Initialized) { return; }
                    SteamUserStats.SetAchievement("Clean_Run");
                    SteamUserStats.StoreStats();
                    PlayerPrefs.SetInt("ACH_17", 1);
                }
                PlayerPrefs.SetInt("LevTwo", 1);
                SceneManager.LoadScene("LevelThreeLoadScreen");
                if (!SteamManager.Initialized) { return; }
                SteamUserStats.SetAchievement("Tourist_Two");
                SteamUserStats.StoreStats();
                PlayerPrefs.SetInt("ACH_9", 1);
                
            }
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelThree"))
            {
                if (noHit == false)
                {
                    if (!SteamManager.Initialized) { return; }
                    SteamUserStats.SetAchievement("Clean_Run");
                    SteamUserStats.StoreStats();
                    PlayerPrefs.SetInt("ACH_17", 1);
                }
                PlayerPrefs.SetInt("LevThree", 1);
                SceneManager.LoadScene("LevelFourLoadScreen");             
                if (!SteamManager.Initialized) { return; }
                SteamUserStats.SetAchievement("Tourist_Three");
                SteamUserStats.StoreStats();
                PlayerPrefs.SetInt("ACH_10", 1);
                
            }
        }

        if (other.gameObject.tag == "Final") {
            GameObject spot = GameObject.FindGameObjectWithTag("MiniCheck");
            player.transform.position = spot.transform.position;
            Physics.SyncTransforms();
            BossSpawner bossy = other.transform.GetComponent<BossSpawner>();
            bossy.boss.SetActive(true);
            bossy.boss.GetComponent<Boss>().healthSlide.SetActive(true);
            for (int i = 0; i < bossy.spawners.Length; i++)
            {
                bossy.spawners[i].SetActive(true);
            }
            EnableMusic mus = other.transform.GetComponent<EnableMusic>();
            mus.mus.SetActive(true);
        }

        if (other.gameObject.tag == "finish"){
            if (noHit == false)
            {
                if (!SteamManager.Initialized) { return; }
                SteamUserStats.SetAchievement("Clean_Run");
                SteamUserStats.StoreStats();
                PlayerPrefs.SetInt("ACH_17", 1);
            }
            
            if (!SteamManager.Initialized) { return; }
            SteamUserStats.SetAchievement("Tourist_Four");
            SteamUserStats.StoreStats();
            PlayerPrefs.SetInt("ACH_11", 1);
            
            
            if (noDeathCounter == 0){
                if (!SteamManager.Initialized) { return; }
                SteamUserStats.SetAchievement("Unstoppable");
                SteamUserStats.StoreStats();
                PlayerPrefs.SetInt("ACH_18", 1);
            }
            if (minutes < 15){
                if (!SteamManager.Initialized) { return; }
                SteamUserStats.SetAchievement("Speed_Runner");
                SteamUserStats.StoreStats();
                PlayerPrefs.SetInt("ACH_24", 1);
            }
            PlayerPrefs.SetInt("LevFour", 1);
            SceneManager.LoadScene("credits");
            Cursor.lockState = CursorLockMode.None;
        }

        if (other.gameObject.tag == "Door"){
            Lock door = other.transform.GetComponent<Lock>();
            if (KeyAmount < door.keysRequired){
                keyText.SetActive(true);
                keysRequiredText.text = "Need " + (door.keysRequired -= KeyAmount) + " more to unlock";
            }
            if (KeyAmount >= door.keysRequired && door.unlocked == false){
                door.unlocked = true;
                KeyAmount--;
                pickups.clip = keyPickup;
                pickups.Play();
            }
        }

        if (other.gameObject.tag == "ItemSpawn"){
            ItemSpawners item = other.transform.GetComponent<ItemSpawners>();
            item.beginSpawn = true;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "PistolAmmo"){
            TakeDamage(10);
            Destroy(other.gameObject);
            ShakeIt();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ladder"){
            onLadder = false;
        }
        if (other.gameObject.tag == "Door"){
            keyText.SetActive(false);
        }
    }

    void selectHurtNum(){
        hurtNum = Random.Range(1, 3);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        noHit = true;
        PlayerPrefs.SetInt("Curr_Health", health);
        selectHurtNum();
        if (hurtNum == 1){
            sound.clip = hurt1;
        }
        if (hurtNum == 2){
            sound.clip = hurt2;
        }
        sound.Play();
        StartCoroutine(flashHit());
        if (health <= 0 && isDead == false && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Arena"))
        {
            sound.clip = death;
            sound.Play();
            deathCounter++;
        }

        if (health <= 0 && isDeadArena == false && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Arena"))
        {
            sound.clip = death;
            sound.Play();
            deathCounter++;
        }

        if (health <= 0f && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Arena") || health <= 0f && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("ArenaV2"))
        {
            mouse.unpaused = true;
            //Time.timeScale = 0f;
            mouse.shootEnabled = false;
            damagePic.SetActive(true);
            cantMove = true;
            speed = 0;
            isDead = true;
            freezeMouse = true;
            deadText.SetActive(true);
            canDash = false;
            cam.transform.position = groundCheck.position;
        }
        if (health <= 0f && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Arena") || health <= 0f && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("ArenaV2"))
        {
            isDeadArena = true;
            mouse.unpaused = true;
            mouse.shootEnabled = false;
            damagePic.SetActive(true);
            cantMove = true;
            speed = 0;
            freezeMouse = true;
            canDash = false;
            StartCoroutine(sendBack());
        }

        if (health <= 0 && isDead == false && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("ArenaV2"))
        {
            sound.clip = death;
            sound.Play();
            deathCounter++;
        }

        if (health <= 0 && isDeadArena == false && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("ArenaV2"))
        {
            sound.clip = death;
            sound.Play();
            deathCounter++;
        }

        /*if (health <= 0f && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("ArenaV2"))
        {
            mouse.unpaused = true;
            //Time.timeScale = 0f;
            mouse.shootEnabled = false;
            damagePic.SetActive(true);
            cantMove = true;
            speed = 0;
            isDead = true;
            freezeMouse = true;
            deadText.SetActive(true);
            canDash = false;
            cam.transform.position = groundCheck.position;
        }
        if (health <= 0f && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("ArenaV2"))
        {
            isDeadArena = true;
            mouse.unpaused = true;
            mouse.shootEnabled = false;
            damagePic.SetActive(true);
            cantMove = true;
            speed = 0;
            freezeMouse = true;
            canDash = false;
            StartCoroutine(sendBack());
        }*/

        /*if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Arena")){
            ArenaManager arena = GameObject.FindGameObjectWithTag("rank").GetComponent<ArenaManager>();
            if (arena.round > 20){
                amount += 10;
            }
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("ArenaV2"))
        {
            ArenaManager arena = GameObject.FindGameObjectWithTag("rank").GetComponent<ArenaManager>();
            if (arena.round > 20)
            {
                amount += 10;
            }
        }*/
    }

    IEnumerator sendBack(){
        yield return new WaitForSeconds(2);
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("main menu");
    }

    void ResetPos(){

        /*pointX = PlayerPrefs.GetFloat("CheckPointX");
        pointY = PlayerPrefs.GetFloat("CheckPointY");
        pointZ = PlayerPrefs.GetFloat("CheckPointZ");*/
        pointX = PlayerPrefs.GetFloat("CheckPointX");
        pointY = PlayerPrefs.GetFloat("CheckPointY");
        pointZ = PlayerPrefs.GetFloat("CheckPointZ");
        priorHealth = PlayerPrefs.GetInt("PriorH");
        priorPistolAmmo = PlayerPrefs.GetInt("PriorP");
        priorShotGunAmmo = PlayerPrefs.GetInt("PriorS");
        priorMachineGunAmmo = PlayerPrefs.GetInt("PriorM");
        priorGrenadeAmmo = PlayerPrefs.GetInt("PriorG");
        priorFireWorkAmmo = PlayerPrefs.GetInt("PriorF");
        priorPoints = PlayerPrefs.GetInt("PriorScore");
        health = priorHealth;
        PlayerPrefs.SetInt("Current_Score", priorPoints);
        PlayerPrefs.SetInt("Pistol_Ammo", priorPistolAmmo);
        PlayerPrefs.SetInt("Shotgun_Ammo", priorShotGunAmmo);
        PlayerPrefs.SetInt("Machinegun_Ammo", priorMachineGunAmmo);
        PlayerPrefs.SetInt("Grenade_Ammo", priorGrenadeAmmo);
        PlayerPrefs.SetInt("Firework_Ammo", priorFireWorkAmmo);
        mouse.currAmmoP = priorPistolAmmo;
        mouse.currAmmoS = priorShotGunAmmo;
        mouse.currAmmoM = priorMachineGunAmmo;
        mouse.currAmmoG = priorGrenadeAmmo;
        mouse.currAmmoF = priorFireWorkAmmo;
        point.totalPoints = priorPoints;
        player.transform.position = new Vector3(pointX, pointY, pointZ);
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Arena"))
        {
            PlayerPrefs.SetInt("Pistol_Ammo", 100);
        }

    }

    IEnumerator flashHit(){
        damagePic.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        damagePic.SetActive(false);
    }

    IEnumerator flashGainHealth(){
        gainHealthPic.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        gainHealthPic.SetActive(false);
    }

    IEnumerator flashCheck(){
        checkText.SetActive(true);
        yield return new WaitForSeconds(1f);
        checkText.SetActive(false);
    }

    public IEnumerator flashAmmo(){
        gainAmmoPic.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        gainAmmoPic.SetActive(false);
    }

    void respawn(){
        player.transform.position = checkpoint.transform.position;
        player.transform.rotation = checkpoint.transform.rotation;
        Physics.SyncTransforms();
    }

    IEnumerator respawnTimer(){
        deadScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        deadScreen.SetActive(false);
        point.totalPoints -= 50;
        TakeDamage(2);
        respawn();
    }

    IEnumerator restartlevel(){
        deadScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        deadScreen.SetActive(false);
        SceneManager.LoadScene("GameWorld");
    }
    
    IEnumerator GoFast(){
        speed = 18f;
        defaultSpeed = 18f;
        fillImageSpeed.fillAmount = 1;
        duration = 20;
        yield return new WaitForSeconds(20f);
        speed = 12f;
        defaultSpeed = 12f;
    }

    IEnumerator KILL(){
        canInstaKill = true;
        fillImageInsta.fillAmount = 1;
        duration2 = 20;
        yield return new WaitForSeconds(20f);
        canInstaKill = false;
    }

    IEnumerator MAX(){
        dontLoseAmmo = true;
        fillImageInfinite.fillAmount = 1;
        duration3 = 20;
        yield return new WaitForSeconds(20f);
        dontLoseAmmo = false;
    }


    public void ShakeIt()
    {
        //ghostInitialPosition = ghost.transform.position;
        InvokeRepeating("StartGhostShaking", 0f, 0.005f);
        Invoke("StopGhostShaking", shakeTime);
    }

    void StartGhostShaking()
    {
        float ghostShakingOffsetX = Random.value * amount * 2 - shakeAmount;
        float ghostShakingOffsetY = Random.value * amount * 2 - shakeAmount;
        Vector3 ghostIntermadiatePosition = ghost.transform.position;
        ghostIntermadiatePosition.x += ghostShakingOffsetX;
        ghostIntermadiatePosition.y += ghostShakingOffsetY;
        ghost.transform.position = ghostIntermadiatePosition;
    }

    void StopGhostShaking()
    {
        CancelInvoke("StartGhostShaking");
        ghost.transform.position = healthPos.position;
    }

    public void newWalk(){
        chooseNewNoise = Random.Range(1, 6);

        if (chooseNewNoise == 1){
            moveNoise.clip = moving1;
        }
        if (chooseNewNoise == 2)
        {
            moveNoise.clip = moving2;
        }
        if (chooseNewNoise == 3)
        {
            moveNoise.clip = moving3;
        }
        if (chooseNewNoise == 4)
        {
            moveNoise.clip = moving4;
        }
        if (chooseNewNoise == 5)
        {
            moveNoise.clip = moving5;
        }
        if (chooseNewNoise == 6)
        {
            moveNoise.clip = moving6;
        }
    }
}
