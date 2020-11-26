using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeToss : MonoBehaviour
{
    public AudioSource weaponShoot;

    public AudioClip pin;
    public AudioClip grenadeThrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pinGrenade()
    {
        weaponShoot.clip = pin;
        weaponShoot.Play();
    }

    public void throwGrenade()
    {
        weaponShoot.clip = grenadeThrow;
        weaponShoot.Play();
    }
}
