using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelselector : MonoBehaviour
{

    public GameObject butt1;
    public GameObject butt2;
    public GameObject butt3;
    public GameObject butt4;

    public int get1;
    public int get2;
    public int get3;
    public int get4;

    // Start is called before the first frame update
    void Start()
    {
        get1 = PlayerPrefs.GetInt("LevOne");
        get2 = PlayerPrefs.GetInt("LevTwo");
        get3 = PlayerPrefs.GetInt("LevThree");
        get4 = PlayerPrefs.GetInt("LevFour");
    }

    // Update is called once per frame
    void Update()
    {
        if (get1 == 0){
            butt1.SetActive(false);
        } else if (get1 == 1){
            butt1.SetActive(true);
        }
        if (get2 == 0)
        {
            butt2.SetActive(false);
        }
        else if (get2 == 1)
        {
            butt2.SetActive(true);
        }
        if (get3 == 0)
        {
            butt3.SetActive(false);
        }
        else if (get3 == 1)
        {
            butt3.SetActive(true);
        }
        if (get4 == 0)
        {
            butt4.SetActive(false);
        }
        else if (get4 == 1)
        {
            butt4.SetActive(true);
        }
    }
}
