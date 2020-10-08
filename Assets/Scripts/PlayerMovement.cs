using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
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
    
    public float health;
    public float amount = 1f;

    public Text healthText;
    public GameObject damagePic;
    public GameObject deadScreen;

    public GameObject player;
    public GameObject checkpoint;

    public GameObject dropSahdow;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        cantMove = false;

        damagePic.SetActive(false);
        pressedJump = true;
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            pressedJump = false;
            time = 0;
        }

        if (cantMove == false){ 
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //Jump
        if (Input.GetButtonDown("Jump") && isGrounded && pressedJump == false)
        {
            velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);
                pressedJump = true;
        }

        if (pressedJump == false && isGrounded == false){
                time += Time.deltaTime;
                if (time <= maxTime){
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

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

            //Dash
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash == true)
            {
                DashEffect.Play();
                canDash = false;
                StartCoroutine(Dash());
                StartCoroutine(DashRecharge());
            }
        }

        //Controls camera bob while moving and jumping
        if (velocity.x <= 0 && velocity.z <=0)
        {
            camAnim.SetBool("IsMoving", false);
        } else
        {
            camAnim.SetBool("IsMoving", true);
        }

        healthText.text = "Health: " + health;

        if (health <= 0){
            StartCoroutine(respawnTimer());
            health = 5;
        }
        RaycastHit ray;

        if (Physics.Raycast(transform.position, -transform.up, out ray))
        {
            if (ray.collider != null){
                 dropSahdow.transform.position = ray.point;
            }
        }
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
                Destroy(other.gameObject);
            }
            
            if (consume != null && consume.isAmmo == true){
                if (mouse.usePistol == true)
                {
                    mouse.currAmmoP += consume.amount;
                    Destroy(other.gameObject);
                }

                if (mouse.useShotgun == true){
                    mouse.currAmmoS += consume.amount;
                    Destroy(other.gameObject);
                }

                if (mouse.useMachineGun == true){
                    mouse.currAmmoM += consume.amount;
                    Destroy(other.gameObject);
                }

                if (mouse.useGrenade == true){
                    mouse.currAmmoG += consume.amount;
                    Destroy(other.gameObject);
                }

                if (mouse.useFirework == true)
                {
                    mouse.currAmmoF= consume.amount;
                    Destroy(other.gameObject);
                }
            }
        }

        if (other.gameObject.tag == "End"){
            StartCoroutine(restartlevel());
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        StartCoroutine(flashHit());
        if (health <= 0f)
        {
            Debug.Log("dead");
        }
    }

    IEnumerator flashHit(){
        damagePic.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        damagePic.SetActive(false);
    }

    void respawn(){
        player.transform.position = checkpoint.transform.position;
        Physics.SyncTransforms();
    }

    IEnumerator respawnTimer(){
        deadScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        deadScreen.SetActive(false);
        respawn();
    }

    IEnumerator restartlevel(){
        deadScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        deadScreen.SetActive(false);
        SceneManager.LoadScene("GameWorld");
    }
}
