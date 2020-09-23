using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    //hit scan help from https://www.youtube.com/watch?v=THnivyG0Mvo&ab_channel=Brackeys

    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    public float dist = 100f;
    public float damage = 1f;

    public float currAmmo;
    public float maxAmmo;

    public bool shootReady;

    public Text ammoText;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        shootReady = true;
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

        if (Input.GetKey(KeyCode.Mouse0) && shootReady == true && currAmmo <= maxAmmo && currAmmo > 0){
            Shoot();
            StartCoroutine(reload());
        }

        if (Input.GetKey(KeyCode.Mouse0) && shootReady == true && currAmmo == 0)
        {
            Debug.Log("no ammo");
        }

        ammoText.text = currAmmo + "/" + maxAmmo;
    }

    void Shoot(){
        RaycastHit hit;
        currAmmo--;
        if (Physics.Raycast(playerBody.position, playerBody.transform.forward, out hit, dist))
        {
            Debug.Log(hit.transform.name);
            EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
            if (enemyHealth != null){
                enemyHealth.TakeDamage(damage);
            }
        }
    }

    IEnumerator reload(){
        shootReady = false;
        yield return new WaitForSeconds(0.5f);
        shootReady = true;
    }
}
