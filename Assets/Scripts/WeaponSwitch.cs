using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public MouseLook mouse;
    public Animator anim;

    public bool switchWep;
    public bool justSwitched;
    public float switchWepTime;
    public float maxSwitch;

    public AudioSource switchNoise;

    public bool endSwitch;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        justSwitched = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Alpha6))
        {
            justSwitched = true;
        }

        /*if (mouse.currWeapon == 1 && mouse.currWeapon == 2 && mouse.currWeapon == 3 && mouse.currWeapon == 4 && mouse.currWeapon == 5 && mouse.currWeapon == 6){
            justSwitched = true;
        }*/

        if (justSwitched == true){
            switchWep = true;
            justSwitched = false;
        }

        

        /*if ( justSwitched == true)
        {
            switchWep = true;
            justSwitched = false;
        }

        if (justSwitched == true)
        {
            switchWep = true;
            justSwitched = false;
        }

        if (justSwitched == true)
        {
            switchWep = true;
            justSwitched = false;
        }
        if (justSwitched == true)
        {
            switchWep = true;
            justSwitched = false;
        }

        if (justSwitched == true)
        {
            switchWep = true;
            justSwitched = false;
        }*/

        if (switchWep == true){
            switchWepTime += Time.deltaTime;
            if (switchWepTime >= maxSwitch){
                switchWep = false;
                anim.SetTrigger("WeaponSwitch");
                switchWepTime = 0;
                endSwitch = true;
            }
        }
    }

    public void playSound(){
        switchNoise.Play();
    }
}
