using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MouseLook : MonoBehaviour
{
    //hit scan help from https://www.youtube.com/watch?v=THnivyG0Mvo&ab_channel=Brackeys
    //shake code from https://www.youtube.com/watch?v=kzHHAdvVkto
    //shotgun code from https://www.youtube.com/watch?v=1gPLfY93JHk&ab_channel=FPSBuilders
    //grenade throw from https://www.youtube.com/watch?v=sglRyWQh79g&ab_channel=FPSBuilders
    //help from https://www.youtube.com/watch?v=wZ2UUOC17AY&ab_channel=Dave%2FGameDevelopment
    // player prefs help from https://www.youtube.com/watch?v=ETXPdH4QHKA&ab_channel=GameDevSpecialist

    public bool shootEnabled;
    public GameObject weapons;
    public GameObject crossHair;

    public float mouseSensitivity = 100f;

    public Transform playerBody;

    public Animator anim;
    public Animator camAnim;
    public Animator crosshairAnim;

    float xRotation = 0f;

    public int currWeapon;

    public GameObject cam;
    Vector3 camInitialPosition;
    public float dist;
    public float damage;
    public float reloadTime;

    //pistol ammo
    public float currAmmoP;
    public float maxAmmoP;

    //Shotgun Ammo
    public float currAmmoS;
    public float maxAmmoS;

    //machineGun Ammo
    public float currAmmoM;
    public float maxAmmoM;

    //grenade ammo
    public float currAmmoG;
    public float maxAmmoG;

    //firework ammo
    public float currAmmoF;
    public float maxAmmoF;

    public bool usePistol;
    public bool useShotgun;
    public bool useMachineGun;
    public bool useKnife;
    public bool useGrenade;
    public bool useFirework;

    //floats for machine gun
    public float fireRate = 15f;
    private float nextTimeToFire = 0f;

    public bool shootReady;

    public Text ammoText;

    public GameObject blood;
    public GameObject impact;
    public GameObject shell;

    public GameObject grenade;
    public GameObject firework;

    public PlayerMovement play;

    public bool shotGunBool;

    public LayerMask layers;

    /*PlayerPrefs.SetInt("EnableShotgun", (shotGunBool ? 1: 0));
    public int value;
    value = shotGunBool ? 1 : 0;*/

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Start is called before the first frame update
    void Start()
    {
        shootReady = true;
        usePistol = true;
        useShotgun = false;
        useMachineGun = false;
        useKnife = false;
        useGrenade = false;
        useFirework = false;
        damage = 2;
        reloadTime = 0.3f;
        dist = 50f;
        currWeapon = 1;
        if ((SceneManager.GetActiveScene() == SceneManager.GetSceneByName("GameWorld")))
        {
            shootEnabled = false;
        } else {
            shootEnabled = true;
        }

        shotGunBool = PlayerPrefs.GetInt("EnableShotgun", 0) > 0;


    }

    // Update is called once per frame
    void Update()
    {
        if (play.freezeMouse == false)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }

        

        if (shootEnabled == false){
            weapons.SetActive(false);
            crossHair.SetActive(false);
            ammoText.enabled = false;
        }

        if (shootEnabled == true)
        {
            weapons.SetActive(true);
            crossHair.SetActive(true);
            ammoText.enabled = true;

            float w = Input.GetAxis("Mouse ScrollWheel");
            if (w < 0f){
                currWeapon = currWeapon + 1;
            } else if (w > 0f){
                currWeapon = currWeapon - 1;
            }

            if (currWeapon < 1){
                currWeapon = 6;
            }

            if (currWeapon > 6){
                currWeapon = 1;
            }

            if (Input.GetKey(KeyCode.Alpha1) || currWeapon == 1)
            {
                Debug.Log("usePistol");
                anim.SetTrigger("Switch_Pistol");
                usePistol = true;
                useShotgun = false;
                useMachineGun = false;
                useKnife = false;
                useGrenade = false;
                useFirework = false;
                damage = 2;
                reloadTime = 0.3f;
                dist = 50f;
                currWeapon = 1;
                //ammoText.text = currAmmoP + "/" + maxAmmoP;
            }

            if (Input.GetKey(KeyCode.Alpha2) && PlayerPrefs.GetInt("EnableShotgun") != 0 || currWeapon == 2)
            {
                Debug.Log("useshotgun");
                anim.SetTrigger("Switch_Shotgun");
                useShotgun = true;
                usePistol = false;
                useMachineGun = false;
                useKnife = false;
                useGrenade = false;
                useFirework = false;
                damage = 4;
                reloadTime = 1f;
                dist = 30f;
                currWeapon = 2;
                //ammoText.text = currAmmoS + "/" + maxAmmoS;
            }

            if (Input.GetKey(KeyCode.Alpha3) || currWeapon == 3)
            {
                Debug.Log("usemachinegun");
                anim.SetTrigger("Switch_Rifle");
                useShotgun = false;
                usePistol = false;
                useMachineGun = true;
                useKnife = false;
                useGrenade = false;
                useFirework = false;
                damage = 1;
                reloadTime = 1f;
                dist = 50f;
                currWeapon = 3;
                //ammoText.text = currAmmoS + "/" + maxAmmoS;
            }

            if (Input.GetKey(KeyCode.Alpha4) || currWeapon == 4)
            {
                Debug.Log("useknife");
                useShotgun = false;
                usePistol = false;
                useMachineGun = false;
                useKnife = true;
                useGrenade = false;
                useFirework = false;
                damage = 3;
                reloadTime = 0.25f;
                dist = 5;
                currWeapon = 4;
                //ammoText.text = currAmmoS + "/" + maxAmmoS;
            }

            if (Input.GetKey(KeyCode.Alpha5) || currWeapon == 5)
            {
                Debug.Log("usegrenade");
                useShotgun = false;
                usePistol = false;
                useMachineGun = false;
                useKnife = false;
                useGrenade = true;
                useFirework = false;
                damage = 3;
                reloadTime = 1f;
                dist = 20f;
                currWeapon = 5;
                //ammoText.text = currAmmoS + "/" + maxAmmoS;
            }

            if (Input.GetKey(KeyCode.Alpha6) || currWeapon == 6)
            {
                Debug.Log("usefirework");
                useShotgun = false;
                usePistol = false;
                useMachineGun = false;
                useKnife = false;
                useGrenade = false;
                useFirework = true;
                damage = 3;
                reloadTime = 4f;
                dist = 20f;
                currWeapon = 6;
                //ammoText.text = currAmmoS + "/" + maxAmmoS;
            }

            if (Input.GetKey(KeyCode.Mouse0) && usePistol == true && shootReady == true && currAmmoP <= maxAmmoP && currAmmoP > 0)
            {
                ShootPistol();
                anim.SetTrigger("PistolShoot");
                crosshairAnim.SetTrigger("Expand");
                camAnim.SetTrigger("camShake1");
                StartCoroutine(reload());
            }

            if (Input.GetKey(KeyCode.Mouse0) && useShotgun == true && shootReady == true && currAmmoS <= maxAmmoS && currAmmoS > 0)
            {
                shootShotgun();
                anim.SetTrigger("ShotgunShoot");
                crosshairAnim.SetTrigger("Expand");
                camAnim.SetTrigger("camShake2");
                Instantiate(shell, transform.position, transform.rotation);
                StartCoroutine(reload());
            }

            if (Input.GetKey(KeyCode.Mouse0) && useMachineGun == true && shootReady == true && currAmmoM <= maxAmmoM && currAmmoM > 0 && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                anim.SetTrigger("RifleShoot");
                crosshairAnim.SetTrigger("Expand");
                camAnim.SetTrigger("camShake3");
                ShootMachineGun();
            }

            if (Input.GetKey(KeyCode.Mouse0) && useKnife == true && shootReady == true)
            {
                StabKnife();
                StartCoroutine(reload());
            }

            if (Input.GetKey(KeyCode.Mouse0) && useGrenade == true && shootReady == true && currAmmoG <= maxAmmoG && currAmmoG > 0)
            {
                ThrowGrenade();
                StartCoroutine(reload());
            }

            if (Input.GetKey(KeyCode.Mouse0) && useFirework == true && shootReady == true && currAmmoF <= maxAmmoF && currAmmoF > 0)
            {
                ShootFireworkk();
                StartCoroutine(reload());
            }

            if (Input.GetKey(KeyCode.Mouse0) && shootReady == true && currAmmoP == 0)
            {
                Debug.Log("no ammo");
            }

            //ammo text scripts
            //used for changing the text to accomadate what type of gun you're using
            if (usePistol == true)
            {
                ammoText.text = currAmmoP + "/" + maxAmmoP;
            }

            if (useShotgun == true)
            {
                ammoText.text = currAmmoS + "/" + maxAmmoS;
            }

            if (useMachineGun == true)
            {
                ammoText.text = currAmmoM + "/" + maxAmmoM;
            }

            if (useKnife == true)
            {
                ammoText.text = "Stab away";
            }

            if (useGrenade == true)
            {
                ammoText.text = currAmmoG + "/" + maxAmmoG;
            }

            if (useFirework == true)
            {
                ammoText.text = currAmmoF + "/" + maxAmmoF;
            }

            if (currAmmoP >= maxAmmoP)
            {
                currAmmoP = maxAmmoP;
            }

            if (currAmmoS >= maxAmmoS)
            {
                currAmmoS = maxAmmoS;
            }

            if (currAmmoM >= maxAmmoM)
            {
                currAmmoM = maxAmmoM;
            }

            if (currAmmoG >= maxAmmoG)
            {
                currAmmoG = maxAmmoG;
            }

            if (currAmmoF >= maxAmmoF)
            {
                currAmmoF = maxAmmoF;
            }
        }
        int shotGunEnable;
        shotGunEnable = shotGunBool? 1 : 0; 

        if (shotGunBool == true){
            PlayerPrefs.SetInt("EnableShotgun", 1);
        } else {
            PlayerPrefs.SetInt("EnableShotgun", 0);
        }
        

        if (Input.GetKey(KeyCode.M)){
            PlayerPrefs.DeleteAll();
        }

    }

    void ShootPistol(){
        RaycastHit hit;
        currAmmoP--;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, dist, layers))
        {
            Debug.Log(hit.transform.name);
            Instantiate(impact, hit.point, Quaternion.identity);
            EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
            if (enemyHealth != null){
                enemyHealth.TakeDamage(damage);
                Instantiate(blood, hit.point, Quaternion.identity);
            }
            PointGiver point = hit.transform.GetComponent<PointGiver>();
            if (point != null){
                point.GivePoint(point.targetPoints);
                Debug.Log("Got some points");
            }
            BossDrone bossDrone = hit.transform.GetComponent<BossDrone>();
            if (bossDrone != null){
                bossDrone.TakeDamage(damage);
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
        RaycastHit sHit5;
        currAmmoS--;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out sHit, dist, layers))
        {
            Debug.Log(sHit.transform.name);
            Instantiate(impact, sHit.point, Quaternion.identity);
            EnemyHealth enemyHealth = sHit.transform.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Instantiate(blood, sHit.point, Quaternion.identity);
            }
            PointGiver point = sHit.transform.GetComponent<PointGiver>();
            if (point != null)
            {
                point.GivePoint(point.targetPoints);
            }
            BossDrone bossDrone = sHit.transform.GetComponent<BossDrone>();
            if (bossDrone != null)
            {
                bossDrone.TakeDamage(damage);
                Instantiate(blood, sHit.point, Quaternion.identity);
            }
        }

        if (Physics.Raycast(cam.transform.position, cam.transform.forward + new Vector3(-.3f, 0f, 0f), out sHit2, dist, layers))
        {
            Debug.Log(sHit2.transform.name);
            Instantiate(impact, sHit2.point, Quaternion.identity);
            EnemyHealth enemyHealth = sHit2.transform.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Instantiate(blood, sHit2.point, Quaternion.identity);
            }
            PointGiver point = sHit2.transform.GetComponent<PointGiver>();
            if (point != null)
            {
                point.GivePoint(point.targetPoints);
            }
            BossDrone bossDrone = sHit2.transform.GetComponent<BossDrone>();
            if (bossDrone != null)
            {
                bossDrone.TakeDamage(damage);
                Instantiate(blood, sHit2.point, Quaternion.identity);
            }
        }

        if (Physics.Raycast(cam.transform.position, cam.transform.forward + new Vector3(0f, .3f, 0f), out sHit3, dist, layers))
        {
            Debug.Log(sHit3.transform.name);
            Instantiate(impact, sHit3.point, Quaternion.identity);
            EnemyHealth enemyHealth = sHit3.transform.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Instantiate(blood, sHit3.point, Quaternion.identity);
            }
            PointGiver point = sHit3.transform.GetComponent<PointGiver>();
            if (point != null)
            {
                point.GivePoint(point.targetPoints);
            }
            BossDrone bossDrone = sHit3.transform.GetComponent<BossDrone>();
            if (bossDrone != null)
            {
                bossDrone.TakeDamage(damage);
                Instantiate(blood, sHit3.point, Quaternion.identity);
            }
        }

        if (Physics.Raycast(cam.transform.position, cam.transform.forward + new Vector3(0f, -.3f, 0f), out sHit4, dist, layers))
        {
            Debug.Log(sHit4.transform.name);
            Instantiate(impact, sHit4.point, Quaternion.identity);
            EnemyHealth enemyHealth = sHit4.transform.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Instantiate(blood, sHit4.point, Quaternion.identity);
            }
            PointGiver point = sHit4.transform.GetComponent<PointGiver>();
            if (point != null)
            {
                point.GivePoint(point.targetPoints);
            }
            BossDrone bossDrone = sHit4.transform.GetComponent<BossDrone>();
            if (bossDrone != null)
            {
                bossDrone.TakeDamage(damage);
                Instantiate(blood, sHit4.point, Quaternion.identity);
            }
        }

        if (Physics.Raycast(cam.transform.position, cam.transform.forward + new Vector3(.3f, 0f, 0f), out sHit5, dist, layers))
        {
            Debug.Log(sHit5.transform.name);
            Instantiate(impact, sHit5.point, Quaternion.identity);
            EnemyHealth enemyHealth = sHit2.transform.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Instantiate(blood, sHit5.point, Quaternion.identity);
            }
            PointGiver point = sHit5.transform.GetComponent<PointGiver>();
            if (point != null)
            {
                point.GivePoint(point.targetPoints);
            }
            BossDrone bossDrone = sHit5.transform.GetComponent<BossDrone>();
            if (bossDrone != null)
            {
                bossDrone.TakeDamage(damage);
                Instantiate(blood, sHit5.point, Quaternion.identity);
            }
        }
    }

    void ShootMachineGun(){
        RaycastHit mHit;
        currAmmoM--;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out mHit, dist, layers))
        {
            Debug.Log(mHit.transform.name);
            Instantiate(impact, mHit.point, Quaternion.identity);
            EnemyHealth enemyHealth = mHit.transform.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Instantiate(blood, mHit.point, Quaternion.identity);
            }
            PointGiver point = mHit.transform.GetComponent<PointGiver>();
            if (point != null)
            {
                point.GivePoint(point.targetPoints);
            }
            BossDrone bossDrone = mHit.transform.GetComponent<BossDrone>();
            if (bossDrone != null)
            {
                bossDrone.TakeDamage(damage);
                Instantiate(blood, mHit.point, Quaternion.identity);
            }
        }
    }

    void StabKnife()
    {
        RaycastHit kHit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out kHit, dist, layers))
        {
            Debug.Log(kHit.transform.name);
            Instantiate(impact, kHit.point, Quaternion.identity);
            EnemyHealth enemyHealth = kHit.transform.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Instantiate(blood, kHit.point, Quaternion.identity);
            }
            PointGiver point = kHit.transform.GetComponent<PointGiver>();
            if (point != null)
            {
                point.GivePoint(point.targetPoints);
            }
            BossDrone bossDrone = kHit.transform.GetComponent<BossDrone>();
            if (bossDrone != null)
            {
                bossDrone.TakeDamage(damage);
                Instantiate(blood, kHit.point, Quaternion.identity);
            }
        }
    }

    void ThrowGrenade(){
        StartCoroutine(BeginThrow());
    }

    void ShootFireworkk(){
        currAmmoF--;
        RaycastHit fHit;
        Vector3 targetPoint;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out fHit, layers)){
            targetPoint = fHit.point;
            Vector3 direction = targetPoint - cam.transform.position;
            GameObject bullet = Instantiate(firework, cam.transform.position, Quaternion.identity);
            bullet.transform.forward = direction.normalized;
            bullet.GetComponent<Rigidbody>().AddForce(direction.normalized * 20f, ForceMode.Impulse);
        }      
    }

    IEnumerator reload(){
        shootReady = false;
        yield return new WaitForSeconds(reloadTime);
        shootReady = true;
    }

    IEnumerator BeginThrow(){
        yield return new WaitForSeconds(0.5f);
        currAmmoG--;
        GameObject grenadeInstance = Instantiate(grenade, cam.transform.position, cam.transform.rotation);
        grenadeInstance.GetComponent<Rigidbody>().AddForce(cam.transform.forward * dist, ForceMode.Impulse);
    }

    
}
