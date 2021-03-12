using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCount : MonoBehaviour
{
    int Ammo = 6;
    int ammoNumber = 0;

    public bool AmmoLeft;
    [Header("GameObjects")]
    public GameObject PistolMagazine;
    public Image[] ammoimage;
    public GameObject[] RotateTransform;

    
    // Start is called before the first frame update
    void Start()
    {
        Ammo = 6;
        ammoNumber = 0;
        AmmoLeft = true;

        for (int i = 0; i < ammoimage.Length; i++)
        {
            ammoimage[i].enabled = true;
        }
        PistolMagazine.transform.Rotate(0, 0, 0);

    }


    public void AmmoDecrease()
    {
        if(ammoNumber <=Ammo)
        {
            ammoimage[ammoNumber].enabled = false;
            ammoNumber++;
       
            PistolMagazine.transform.rotation = RotateTransform[ammoNumber-1].transform.rotation;

            if(ammoNumber == Ammo)
            {
                AmmoLeft = false;
            }
        }    
    }
    public void Reset()
    {
        ammoNumber = 0;
        PistolMagazine.transform.Rotate(0, 0, 0);
        AmmoLeft = true;
        for (int i = 0; i < ammoimage.Length; i++)
        {
            ammoimage[i].enabled = true;
        }
    }
}
