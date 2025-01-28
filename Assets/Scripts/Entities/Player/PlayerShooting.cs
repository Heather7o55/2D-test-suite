using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    GameObject[] bullets;
    public GameObject bulletPrefab;
    public Transform firePointRotation;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 20f;
    public Animator anim;
    public enum Gun
    {
        Pistol,
        Rifle,
        Shotgun,
        Sniper
    }
    public static Gun activeGun = Gun.Pistol;
    bool canShoot = true;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if(UIManager.isPaused) return;
        if(Input.GetButton("Fire1") && canShoot)
        {
            Shoot();
        }   
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, firePointRotation.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePointRotation.right * bulletSpeed;
        Destroy(bullet, 10f);
        switch(activeGun)
        {
            case Gun.Pistol:
                anim.Play("MuzzleFlash");
                StartCoroutine(GunCoolDown(0.3f));
                break;
            case Gun.Rifle:
                anim.Play("MuzzleFlash");
                StartCoroutine(GunCoolDown(0.1f));
                break;
            case Gun.Shotgun:
                anim.Play("MuzzleFlash");
                StartCoroutine(GunCoolDown(0.5f));
                break;
            case Gun.Sniper:
                anim.Play("MuzzleFlash");
                StartCoroutine(GunCoolDown(0.75f));
                break;
        }
    }
    void CreateBullets(int ammount, int speed)
    {
        for(int i = 0; i < ammount; i++)
        {
            bullets[i] = Instantiate(bulletPrefab, new Vector3(bulletSpawnPoint.position.x, Random.Range(bulletSpawnPoint.position.y - 0.5f, bulletSpawnPoint.position.y + 0.5f), bulletSpawnPoint.position.z), firePointRotation.rotation);
            bullets[i].GetComponent<Rigidbody2D>();
        }
    }
    IEnumerator GunCoolDown(float timer)
    {
        canShoot = false;
        yield return new WaitForSeconds(timer);
        canShoot = true;
    }
}
