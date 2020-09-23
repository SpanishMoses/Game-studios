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

    public float defaultSpeed = 12f;
    public float speed = 12f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f; 
    public LayerMask groundMask;
    public float jumpHight = 3f;

    [SerializeField] private float dashForce = 50f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float timeBetweenDashes = 1f;
    private bool canDash = true;

    Vector3 velocity;
    bool isGrounded;
    public bool cantMove;

    public float health;
    public float amount = 1f;

    public Text healthText;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        /*if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("GameWorld")){
            cantMove = true;
        } else
        {
            cantMove = false;
        }*/

        cantMove = false;
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (cantMove == false){ 
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

            //Dash
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash == true)
            {
                canDash = false;
                StartCoroutine(Dash());
                StartCoroutine(DashRecharge());
            }
        }

        healthText.text = "Health: " + health;
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
        if (other.gameObject.tag == "Consumable"){
            Consumables consume = other.transform.GetComponent<Consumables>();
            if (consume != null && consume.isHealth == true){
                health += consume.amount;
                Destroy(other.gameObject);
            }
            
            if (consume != null && consume.isAmmo == true){
                mouse.currAmmo += consume.amount;
                Destroy(other.gameObject);
            }
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Debug.Log("dead");
        }
    }
}
