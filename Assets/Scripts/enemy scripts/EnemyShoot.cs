using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bullet;
    EnemyMove mov;
    public GameObject parent;
    public float damage = 1f;

    // Start is called before the first frame update
    void Start()
    {
        mov = GetComponent<EnemyMove>();
        StartCoroutine(shoot());
    }

    // Update is called once per frame
    void Update()
    {
        /*if (mov.locActive == true){
            
        }*/
    }

    IEnumerator shoot(){
        yield return new WaitForSeconds(6);
        Instantiate(bullet, parent.transform.position, Quaternion.identity);
        StartCoroutine(shoot());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player"){
            //Destroy(gameObject);
            Debug.Log("YETT");
            /*PlayerMovement playHealth = other.transform.GetComponent<PlayerMovement>();
            if (playHealth != null){
                playHealth.TakeDamage(damage);
            }*/
        }
    }
}
