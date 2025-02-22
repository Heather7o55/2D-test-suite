using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GunController;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject batHitbox;
    public Transform firePointRotation;
    public Transform bulletSpawnPoint;
    public Animator anim;
    public int pellets = 5;
    public AudioClip pistol;
    public AudioClip rifle;
    public AudioClip sniper;
    public AudioClip shotgun;
    public AudioSource audioSource;
    

    public static Weapon activeGun = Weapon.Pistol;
    bool canShoot = true;
    bool batActive = false;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        batHitbox.SetActive(batActive);
        if(UIManager.isPaused) return;
        if(Input.GetButton("Fire1") && canShoot)
            Shoot();
    }
    void Shoot()
    {
       
        switch(activeGun)
        {
            case Weapon.Pistol:
                anim.Play("MuzzleFlash");
                CreateBullet(0f, 20f, 1, bulletSpawnPoint, firePointRotation, bulletPrefab);
                audioSource.clip = pistol;
                StartCoroutine(GunCoolDown(0.3f));
                break;
            case Weapon.Rifle:
                anim.Play("MuzzleFlash");
                CreateBullet(0f, 25f, 1, bulletSpawnPoint, firePointRotation, bulletPrefab);
                audioSource.clip = rifle;
                StartCoroutine(GunCoolDown(0.1f));
                break;
            case Weapon.Shotgun:
                anim.Play("MuzzleFlash");
                Shotgun(bulletSpawnPoint, firePointRotation, bulletPrefab);
                audioSource.clip = shotgun;
                StartCoroutine(GunCoolDown(0.5f));
                break;
            case Weapon.Sniper:
                anim.Play("MuzzleFlash");
                CreateBullet(0f, 50f, 3, bulletSpawnPoint, firePointRotation, bulletPrefab);
                audioSource.clip = sniper;
                StartCoroutine(GunCoolDown(0.75f));
                break;
            case Weapon.Bat:
                StartCoroutine(BatSwingTime(0.5f));
                break;
        }
        audioSource.Play();
    }
   
    void Shotgun(Transform spawnPoint, Transform rotatePoint, GameObject prefab)
    {
        for(int i = 0; i < pellets; i++) CreateBullet(0.35f, 10f, 1, spawnPoint, rotatePoint, prefab);
    }
    IEnumerator GunCoolDown(float timer)
    {
        Debug.Log("cooldown active");
        canShoot = false;
        yield return new WaitForSeconds(timer);
        canShoot = true;
    }
    IEnumerator BatSwingTime(float timer)
    {
        Debug.Log("cooldown active");
        batActive = true;
        yield return new WaitForSeconds(timer);
        batActive = false;
    }

}
