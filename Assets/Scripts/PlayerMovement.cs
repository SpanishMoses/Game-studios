﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //knockback help from https://answers.unity.com/questions/242648/force-on-character-controller-knockback.html

    public UnityEngine.CharacterController controller;

    public static PlayerMovement instance;

    public MouseLook mouse;

    private Rigidbody rb;

    public Animator camAnim;

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

    public Text healthText;
    public GameObject damagePic;
    public GameObject gainHealthPic;
    public GameObject gainAmmoPic;
    public GameObject deadScreen;
    public GameObject deadText;

    public GameObject player;
    public GameObject checkpoint;

    public GameObject dropSahdow;
    public GameObject resumeButt;
    public GameObject quitButt;

    public PointManager point;

    float mass = 3.0F; // defines the character mass
    Vector3 impact = Vector3.zero;

    public int hurtNum; 

    public AudioClip hurt1;
    public AudioClip hurt2;
    public AudioClip dashNoise;

    public AudioSource sound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        cantMove = false;
        health = PlayerPrefs.GetInt("Curr_Health", 50);
        damagePic.SetActive(false);
        pressedJump = true;
        point = GameObject.FindGameObjectWithTag("PointManager").GetComponent<PointManager>();
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

        //Jump
        if (Input.GetButtonDown("Jump") && isGrounded && pressedJump == false && onLadder == false)
        {
            velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);
                pressedJump = true;
        }

        if (pressedJump == false && isGrounded == false){
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
            health = 50;
            point.totalPoints -= 50;
            isDead = false;
            freezeMouse = false;
            deadText.SetActive(false);
            mouse.unpaused = false;
        }

        if (Input.GetKey(KeyCode.Escape)){
            mouse.unpaused = false;
            Time.timeScale = 0f;
            paused = true;
            freezeMouse = true;
            resumeButt.SetActive(true);
            quitButt.SetActive(true);
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
        Time.timeScale = 1f;
        
        
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
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Checkpoint"){
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
                    StartCoroutine(flashAmmo());
                
            }
        }

        if (other.gameObject.tag == "End"){
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("GameWorld"))
            {
                SceneManager.LoadScene("LevelOneLoadScreen");
            }
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelOne"))
            {
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("LevelTwoLoadScreen");
            }
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("LevelTwo")){
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("main menu");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ladder"){
            onLadder = false;
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
        if (health <= 0f)
        {
            mouse.unpaused = false;
            Time.timeScale = 0f;
            isDead = true;
            freezeMouse = true;
            deadText.SetActive(true);
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
}
