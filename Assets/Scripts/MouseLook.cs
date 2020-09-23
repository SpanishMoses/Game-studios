using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    //hit scan help from https://www.youtube.com/watch?v=THnivyG0Mvo&ab_channel=Brackeys
    //shake code from https://www.youtube.com/watch?v=kzHHAdvVkto
    //shotgun code from https://www.youtube.com/watch?v=1gPLfY93JHk&ab_channel=FPSBuilders

    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    public GameObject cam;
    Vector3 camInitialPosition;
    public float dist = 100f;
    public float damage;
    public float reloadTime;

    //pistol ammo
    public float currAmmoP;
    public float maxAmmoP;

    //Shotgun Ammo
    public float currAmmoS;
    public float maxAmmoS;

    public bool usePistol;
    public bool useShotgun;

    public bool shootReady;

    public Text ammoText;

    public GameObject blood;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        shootReady = true;
        usePistol = true;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        if (Input.GetKey(KeyCode.Alpha1)){
            Debug.Log("usePistol");
            usePistol = true;
            useShotgun = false;
            damage = 1;
            reloadTime = 0.5f;
            //ammoText.text = currAmmoP + "/" + maxAmmoP;
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            Debug.Log("useshotgun");
            useShotgun = true;
            usePistol = false;
            damage = 4;
            reloadTime = 1f;
            //ammoText.text = currAmmoS + "/" + maxAmmoS;
        }

        if (Input.GetKey(KeyCode.Mouse0) && usePistol == true && shootReady == true && currAmmoP <= maxAmmoP && currAmmoP > 0){
            ShootPistol();
            StartCoroutine(reload());
        }

        if (Input.GetKey(KeyCode.Mouse0) && useShotgun == true && shootReady == true && currAmmoS <= maxAmmoS && currAmmoS > 0)
        {
            shootShotgun();
            StartCoroutine(reload());
        }

        if (Input.GetKey(KeyCode.Mouse0) && shootReady == true && currAmmoP == 0)
        {
            Debug.Log("no ammo");
        }

        //ammoText.text = currAmmoP + "/" + maxAmmoP;

        if (usePistol == true){
            ammoText.text = currAmmoP + "/" + maxAmmoP;
        }

        if (useShotgun == true){
            ammoText.text = currAmmoS + "/" + maxAmmoS;
        }
    }

    void ShootPistol(){
        RaycastHit hit;
        currAmmoP--;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, dist))
        {
            Debug.Log(hit.transform.name);
            EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
            if (enemyHealth != null){
                enemyHealth.TakeDamage(damage);
                Instantiate(blood, hit.point, Quaternion.identity);
            }
        }
    }

    void shootShotgun()
    {
        RaycastHit sHit;
        RaycastHit sHit2;
        RaycastHit sHit3;
        RaycastHit sHit4;
        currAmmoS--;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out sHit, dist))
        {
            Debug.Log(sHit.transform.name);
            EnemyHealth enemyHealth = sHit.transform.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Instantiate(blood, sHit.point, Quaternion.identity);
            }
        }

        if (Physics.Raycast(cam.transform.position, cam.transform.forward + new Vector3(-.2f, 0f, 0f), out sHit2, dist))
        {
            Debug.Log(sHit2.transform.name);
            EnemyHealth enemyHealth = sHit2.transform.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Instantiate(blood, sHit2.point, Quaternion.identity);
            }
        }

        if (Physics.Raycast(cam.transform.position, cam.transform.forward + new Vector3(0f, .1f, 0f), out sHit3, dist))
        {
            Debug.Log(sHit3.transform.name);
            EnemyHealth enemyHealth = sHit3.transform.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Instantiate(blood, sHit3.point, Quaternion.identity);
            }
        }

        if (Physics.Raycast(cam.transform.position, cam.transform.forward + new Vector3(0f, -.1f, 0f), out sHit4, dist))
        {
            Debug.Log(sHit4.transform.name);
            EnemyHealth enemyHealth = sHit4.transform.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Instantiate(blood, sHit4.point, Quaternion.identity);
            }
        }
    }

    IEnumerator reload(){
        shootReady = false;
        yield return new WaitForSeconds(0.5f);
        shootReady = true;
    }
}
