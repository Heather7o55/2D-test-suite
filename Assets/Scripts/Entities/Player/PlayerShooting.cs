using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GunController;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public Animator anim;
    public int pellets = 5;
    public AudioClip pistol;
    public AudioClip rifle;
    public AudioClip sniper;
    public AudioClip shotgun;
    public AudioSource audioSource;
    

    public static Gun activeGun = Gun.Pistol;
    private GunController gun;
    void Start()
    {
        gun = GetComponent<GunController>();
        anim = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if(UIManager.isPaused) return;
        if(Input.GetButton("Fire1") && gun.canShoot)
            Shoot();
    }
    void Shoot()
    {
       
        switch(activeGun)
        {
            case Gun.Pistol:
                anim.Play("MuzzleFlash");
                gun.CreateBullet(0f, 20f, 1, bulletSpawnPoint, transform, bulletPrefab);
                audioSource.clip = pistol;
                gun.StartGunCooldown(0.3f);
                break;
            case Gun.Rifle:
                anim.Play("MuzzleFlash");
                gun.CreateBullet(0f, 25f, 1, bulletSpawnPoint, transform, bulletPrefab);
                audioSource.clip = rifle;
                gun.StartGunCooldown(0.1f);
                break;
            case Gun.Shotgun:
                anim.Play("MuzzleFlash");
                gun.Shotgun(bulletSpawnPoint, transform, bulletPrefab, pellets);
                audioSource.clip = shotgun;
                gun.StartGunCooldown(0.5f);
                break;
            case Gun.Sniper:
                anim.Play("MuzzleFlash");
                gun.CreateBullet(0f, 50f, 3, bulletSpawnPoint, transform, bulletPrefab);
                audioSource.clip = sniper;
                gun.StartGunCooldown(0.75f);
                break;
        }
        audioSource.Play();
    }
   
    
}
