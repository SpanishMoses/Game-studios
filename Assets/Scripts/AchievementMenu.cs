using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;

public class AchievementMenu : MonoBehaviour
{

    public Image achievement1;
    public Image achievement2;
    public Image achievement3;
    public Image achievement4;
    public Image achievement5;
    public Image achievement6;
    public Image achievement7;
    public Image achievement8;
    public Image achievement9;
    public Image achievement10;
    public Image achievement11;
    public Image achievement12;
    public Image achievement13;
    public Image achievement14;
    public Image achievement15;
    public Image achievement16;
    public Image achievement17;
    public Image achievement18;
    public Image achievement19;
    public Image achievement20;
    public Image achievement21;
    //start from here now
    public Image achievement22;
    public Image achievement23;
    public Image achievement24;

    public Sprite complete1;
    public Sprite complete2;
    public Sprite complete3;
    public Sprite complete4;
    public Sprite complete5;
    public Sprite complete6;
    public Sprite complete7;
    public Sprite complete8;
    public Sprite complete9;
    public Sprite complete10;
    public Sprite complete11;
    public Sprite complete12;
    public Sprite complete13;
    public Sprite complete14;
    public Sprite complete15;
    public Sprite complete16;
    public Sprite complete17;
    public Sprite complete18;
    public Sprite complete19;
    public Sprite complete20;
    public Sprite complete21;
    //start from here now
    public Sprite complete22;
    public Sprite complete23;
    public Sprite complete24;

    public int get1;
    public int get2;
    public int get3;
    public int get4;
    public int get5;
    public int get6;
    public int get7;
    public int get8;
    public int get9;
    public int get10;
    public int get11;
    public int get12;
    public int get13;
    public int get14;
    public int get15;
    public int get16;
    public int get17;
    public int get18;
    public int get19;
    public int get20;
    public int get21;
    //start from here now
    public int get22;
    public int get23;
    public int get24;

    // Start is called before the first frame update
    void Start()
    {
        get1 = PlayerPrefs.GetInt("ACH_1");
        get2 = PlayerPrefs.GetInt("ACH_2");
        get3 = PlayerPrefs.GetInt("ACH_3");
        get4 = PlayerPrefs.GetInt("ACH_4");
        get5 = PlayerPrefs.GetInt("ACH_5");
        get6 = PlayerPrefs.GetInt("ACH_6");
        get7 = PlayerPrefs.GetInt("ACH_7");
        get8 = PlayerPrefs.GetInt("ACH_8");
        get9 = PlayerPrefs.GetInt("ACH_9");
        get10 = PlayerPrefs.GetInt("ACH_10");
        get11 = PlayerPrefs.GetInt("ACH_11");
        get12 = PlayerPrefs.GetInt("ACH_12");
        get13 = PlayerPrefs.GetInt("ACH_13");
        get14 = PlayerPrefs.GetInt("ACH_14");
        get15 = PlayerPrefs.GetInt("ACH_15");
        get16 = PlayerPrefs.GetInt("ACH_16");
        get17 = PlayerPrefs.GetInt("ACH_17");
        get18 = PlayerPrefs.GetInt("ACH_18");
        get19 = PlayerPrefs.GetInt("ACH_19");
        get20 = PlayerPrefs.GetInt("ACH_20");
        get21 = PlayerPrefs.GetInt("ACH_21");
        //start from here now
        get22 = PlayerPrefs.GetInt("ACH_22");
        get23 = PlayerPrefs.GetInt("ACH_23");
        get24 = PlayerPrefs.GetInt("ACH_24");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (get1 == 1){
            achievement1.sprite = complete1;
        }
        if (get2 == 1)
        {
            achievement2.sprite = complete2;
        }
        if (get3 == 1)
        {
            achievement3.sprite = complete3;
        }
        if (get4 == 1)
        {
            achievement4.sprite = complete4;
        }
        if (get5 == 1)
        {
            achievement5.sprite = complete5;
        }
        if (get6 == 1)
        {
            achievement6.sprite = complete6;
        }
        if (get7 == 1){
            achievement7.sprite = complete7;
        }
        if (get8 == 1)
        {
            achievement8.sprite = complete8;
        }
        if (get9 == 1)
        {
            achievement9.sprite = complete9;
        }
        if (get10 == 1)
        {
            achievement10.sprite = complete10;
        }
        if (get11 == 1)
        {
            achievement11.sprite = complete11;
        }
        if (get12 == 1)
        {
            achievement12.sprite = complete12;
        }
        if (get13 == 1)
        {
            achievement13.sprite = complete13;
        }
        if (get14 == 1)
        {
            achievement14.sprite = complete14;
        }
        if (get15 == 1)
        {
            achievement15.sprite = complete15;
        }
        if (get16 == 1)
        {
            achievement16.sprite = complete16;
        }
        if (get17 == 1)
        {
            achievement17.sprite = complete17;
        }
        if (get18 == 1)
        {
            achievement18.sprite = complete18;
        }
        if (get19 == 1)
        {
            achievement19.sprite = complete19;
        }
        if (get20 == 1)
        {
            achievement20.sprite = complete20;
        }
        if (get21 == 1){
            achievement21.sprite = complete21;
        }
        if (get22 == 1){
            achievement22.sprite = complete22;
        }
        if (get23 == 1)
        {
            achievement23.sprite = complete23;
        }
        if (get24 == 1)
        {
            achievement24.sprite = complete24;
        }
    }
}
