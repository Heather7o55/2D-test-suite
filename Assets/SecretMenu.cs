using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SecretMenu : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    // Update is called once per frame
    void Update()
    {
        switch(dropdown.value)
        {
            case 0:
                PlayerShooting.activeGun = GunController.Gun.Pistol;
                Debug.Log("Pistol");
                break;
            case 1:
                PlayerShooting.activeGun = GunController.Gun.Rifle;
                Debug.Log("Rifle");
                break;
            case 2:
                PlayerShooting.activeGun = GunController.Gun.Shotgun;
                Debug.Log("Shotgun");
                break;
        }
    }
}
