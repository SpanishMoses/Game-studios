using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bullet;
    EnemyMove mov;
    public GameObject parent;
    public float damage = 1f;

    public float shootTime;
    public float timeBetweenShots;

    // Start is called before the first frame update
    void Start()
    {
        mov = GetComponent<EnemyMove>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mov.locActive == true || mov.backUp == true)
        {
            shootTime += Time.deltaTime;
            if (shootTime >= timeBetweenShots){
                shootTime = 0;
                Instantiate(bullet, parent.transform.position, Quaternion.identity);
            }
        }
    }

    private void FixedUpdate()
    {
        
    }

    /*IEnumerator shoot(){
        yield return new WaitForSeconds(6);
        
        StartCoroutine(shoot());
    }*/

    

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
