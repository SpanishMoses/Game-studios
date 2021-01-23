using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getdeath : MonoBehaviour
{

    public EnemyHealth health;
    public GameObject check;
    public GameObject parent;

    public void StartWispDeath()
    {
        if (health.partOfArray == false)
        {
            Destroy(check);
            Destroy(parent);
            Destroy(gameObject);
            Instantiate(health.soul, transform.position, Quaternion.identity);
            Instantiate(health.deathPart, transform.position, Quaternion.identity);
            if (health.splatBlood == true)
            {
                RaycastHit ray;
                if (Physics.Raycast(transform.position, -transform.up, out ray, health.layer))
                {
                    if (ray.collider != null)
                    {
                        Instantiate(health.bloodSplat, ray.point + new Vector3(0, 0.2f, 0), Quaternion.LookRotation(-ray.normal));
                    }
                }
            }
            if (health.isBear == true)
            {
                health.achieve.bearKills++;
            }
            if (health.isUnicorn == true)
            {
                health.achieve.unicornKills++;
            }
            if (health.isDrone == true)
            {
                health.achieve.droneKills++;
            }
            if (health.isExplode == true)
            {
                health.achieve.explodeKills++;
            }
            if (health.isPanda == true)
            {
                health.achieve.pandaKills++;
            }
        }
        if (health.partOfArray == true)
        {
            Destroy(check);
            Destroy(parent);
            health.enemySpawn.deductEnemy(health.amountTaken);
            Instantiate(health.soul, transform.position, Quaternion.identity);
            Instantiate(health.deathPart, transform.position, Quaternion.identity);
            Destroy(gameObject);
            if (health.splatBlood == true)
            {
                RaycastHit ray;
                if (Physics.Raycast(transform.position, -transform.up, out ray))
                {
                    if (ray.collider != null)
                    {
                        Instantiate(health.bloodSplat, ray.point + new Vector3(0, 0.2f, 0), Quaternion.LookRotation(-ray.normal));
                    }
                }
            }
            if (health.isBear == true)
            {
                health.achieve.bearKills++;
            }
            if (health.isUnicorn == true)
            {
                health.achieve.unicornKills++;
            }
            if (health.isDrone == true)
            {
                health.achieve.droneKills++;
            }
            if (health.isExplode == true)
            {
                health.achieve.explodeKills++;
            }
            if (health.isPanda == true)
            {
                health.achieve.pandaKills++;
            }
        }
        if (health.specialDrop == true)
        {
            health.enemySpawn.deductEnemy(health.amountTaken);
            Instantiate(health.weapon, transform.position, Quaternion.identity);
            Instantiate(health.soul, transform.position, Quaternion.identity);
            Instantiate(health.deathPart, transform.position, Quaternion.identity);
            Destroy(gameObject);
            if (health.splatBlood == true)
            {
                RaycastHit ray;
                if (Physics.Raycast(transform.position, -transform.up, out ray))
                {
                    if (ray.collider != null)
                    {
                        Instantiate(health.bloodSplat, ray.point + new Vector3(0, 0.05f, 0), Quaternion.LookRotation(-ray.normal));
                    }
                }
            }
            if (health.isBear == true)
            {
                health.achieve.bearKills++;
            }
            if (health.isUnicorn == true)
            {
                health.achieve.unicornKills++;
            }
            if (health.isDrone == true)
            {
                health.achieve.droneKills++;
            }
            if (health.isExplode == true)
            {
                health.achieve.explodeKills++;
            }
            if (health.isPanda == true)
            {
                health.achieve.pandaKills++;
            }
        }
    }
}
