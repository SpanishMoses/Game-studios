using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bullet;
    EnemyMove mov;
    public GameObject parent;

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
}
