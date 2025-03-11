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
    private CameraController cameraController;
    private GunController gun;
    void Start()
    {
        gun = GetComponent<GunController>();
        anim = GetComponentInChildren<Animator>();
        cameraController = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
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
                cameraController.CameraShake(0.05f, 0.2f);
                anim.Play("MuzzleFlash");
                gun.CreateBullet(0f, 30f, 1, bulletSpawnPoint, transform, bulletPrefab);
                audioSource.clip = pistol;
                gun.StartGunCooldown(0.3f);
                audioSource.pitch = Random.Range(0.9f, 1.1f);
                break;
            case Gun.Rifle:
                cameraController.CameraShake(0.05f, 0.2f);
                anim.Play("MuzzleFlash");
                gun.CreateBullet(0f, 30f, 1, bulletSpawnPoint, transform, bulletPrefab);
                audioSource.clip = rifle;
                gun.StartGunCooldown(0.12f);
                break;
            case Gun.Shotgun:
                cameraController.CameraShake(0.2f, 0.3f);
                anim.Play("MuzzleFlash");
                gun.Shotgun(bulletSpawnPoint, transform, bulletPrefab, pellets);
                audioSource.clip = shotgun;
                gun.StartGunCooldown(0.5f);
                audioSource.pitch = Random.Range(0.8f, 1.2f);
                break;
            case Gun.Sniper:
                cameraController.CameraShake(0.2f, 0.3f);
                anim.Play("MuzzleFlash");
                gun.CreateBullet(0f, 50f, 3, bulletSpawnPoint, transform, bulletPrefab);
                audioSource.clip = sniper;
                gun.StartGunCooldown(0.75f);
                audioSource.pitch = 1;
                break;
        }
        
        audioSource.Play();
    }
   
    
}
