using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    //knockback help from https://answers.unity.com/questions/242648/force-on-character-controller-knockback.html
    //footstep noise help from https://www.youtube.com/watch?v=ih8gyGeC7xs&ab_channel=EYEmaginary
    //shake code from https://www.youtube.com/watch?v=kzHHAdvVkto

    public UnityEngine.CharacterController controller;

    public static PlayerMovement instance;

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
        ResetPos();
        curScene = SceneManager.GetActiveScene();
        sceneName = curScene.name;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("Curr_Health", health);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            pressedJump = false;
            time = 0;
        }

        if (cantMove == false || onLadder == false){ 
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

            if (onLadder == false)
            {
                Vector3 move = transform.right * x * speed + transform.forward * z * speed + transform.up * velocity.y;

                controller.Move(move * Time.deltaTime);
                
            }
            if (onLadder == true){
                Vector3 moveUp = transform.up * z;
                controller.Move(transform.up * speed * Time.deltaTime);
            }

           if (controller.isGrounded == true && controller.velocity.magnitude > 2f && moveNoise.isPlaying == false){
                moveNoise.Play();
            }

            if (controller.isGrounded == true && controller.velocity.magnitude < 2f && moveNoise.isPlaying == true)
            {
                moveNoise.Stop();
            }

            //Jump
            if (Input.GetButtonDown("Jump") && isGrounded && pressedJump == false && onLadder == false)
        {
            velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);
                pressedJump = true;
                jumping.Play();
                setPitch();
        }

            if (pressedJump == false && isGrounded == false && isDead == false){
                time += Time.deltaTime;
                if (time <= maxTime && onLadder == false){
                    if (Input.GetButtonDown("Jump") && pressedJump == false)
                    {
                        velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);
                        pressedJump = true;
                        time = 0;
                    } else if ( time >= maxTime){
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

            //Dash
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash == true)
            {
                DashEffect.Play();
                canDash = false;
                sound.clip = dashNoise;
                sound.Play();
                StartCoroutine(Dash());
                StartCoroutine(DashRecharge());
            }
        }

        if (health < 30){
            dangerScreen.SetActive(true);
        }

        if (health >= 30){
            dangerScreen.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.L)){
            GameObject spot = GameObject.FindGameObjectWithTag("MiniCheck");
            player.transform.position = spot.transform.position;
            Physics.SyncTransforms();
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

        if (isDead == true && Input.GetKey(KeyCode.Mouse0) && mouse.unpaused == true){
            Time.timeScale = 1f;
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
        Time.timeScale = 1f;
    }

    public void setPitch(){
        pitch = Random.Range(0.5f, 1.5f);
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
            mouse.shotGunBool = true;
            mouse.currWeapon = 2;
            mouse.useShotgun = true;
            pickups.clip = weaponPickup;
            pickups.Play();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Rifle")
        {
            mouse.machineGunBool = true;
            mouse.currWeapon = 3;
            mouse.useMachineGun = true;
            pickups.clip = weaponPickup;
            pickups.Play();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Knife")
        {
            mouse.knifeBool = true;
            mouse.currWeapon = 4;
            mouse.useKnife = true;
            pickups.clip = weaponPickup;
            pickups.Play();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Grenade")
        {
            mouse.grenadeBool = true;
            mouse.currWeapon = 5;
            mouse.useGrenade = true;
            pickups.clip = weaponPickup;
            pickups.Play();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "FireWork")
        {
            mouse.fireWorkBool = true;
            mouse.currWeapon = 6;
            mouse.useFirework = true;
            pickups.clip = weaponPickup;
            pickups.Play();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Checkpoint"){
            //checkpoint = other.gameObject;
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

        if (other.gameObject.tag == "Consumable"){
            Consumables consume = other.transform.GetComponent<Consumables>();
            if (consume != null && consume.isHealth == true){
                health += consume.amount;
                pickups.clip = healthPickup;
                pickups.Play();
                ShakeIt();
                StartCoroutine(flashGainHealth());
                Destroy(other.gameObject);
            }
            
            if (consume != null && consume.isAmmo == true){
                
                    mouse.currAmmoP += consume.amount;
                    mouse.currAmmoS += consume.amount;
                    mouse.currAmmoM += consume.amount;
                    mouse.currAmmoG += consume.amount;
                    mouse.currAmmoF = consume.amount;
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

            if (consume != null && consume.isRocket == true){
                mouse.currAmmoF += consume.amount;
                Destroy(other.gameObject);
            }

            if (consume != null && consume.isGrenade == true){
                mouse.currAmmoG += consume.amount;
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
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("LevelTwoLoadScreen");
            }
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelTwo")){
                SceneManager.LoadScene("LevelThreeLoadScreen");
            }
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelThree"))
            {
                SceneManager.LoadScene("LevelFourLoadScreen");
            }
        }

        if (other.gameObject.tag == "Final"){
            GameObject spot = GameObject.FindGameObjectWithTag("MiniCheck");
            player.transform.position = spot.transform.position;
            Physics.SyncTransforms();
        }

        if (other.gameObject.tag == "finish"){
            SceneManager.LoadScene("main menu");
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
        if (health <= 0f && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Arena"))
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
        if (health <= 0f && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Arena"))
        {
            
            mouse.unpaused = true;
            mouse.shootEnabled = false;
            damagePic.SetActive(true);
            cantMove = true;
            speed = 0;
            freezeMouse = true;
            canDash = false;
            StartCoroutine(sendBack());
        }
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

    IEnumerator flashAmmo(){
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
        health -= 1;
        respawn();
    }

    IEnumerator restartlevel(){
        deadScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        deadScreen.SetActive(false);
        SceneManager.LoadScene("GameWorld");
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
