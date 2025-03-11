using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Theoretically this should be a static class, however unity doesn't let me inherit GetComponent, 
Instantiate, or Destroy unless its a monobehaviour */
public class GunController : MonoBehaviour
{
    public bool canShoot = true;
    public enum Gun
    {
        Pistol,
        Rifle,
        Shotgun,
        Sniper
    }
    /* "CreateBullet" takes in the range, aka the spread, the bullet speed, the damage,
    the spawnpoint, the rotation transform of the character its spawning from, and the bullet object it wants to spawn  */
    public void CreateBullet(float range, float speed, int damage, Transform spawnPoint, Transform firePointRotation, GameObject bulletPrefab)
    {  
        GameObject bullet = Instantiate(bulletPrefab, new Vector3(spawnPoint.position.x, 
            Random.Range(spawnPoint.position.y - range, spawnPoint.position.y + range), 
            spawnPoint.position.z), firePointRotation.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePointRotation.right * speed;
        bullet.GetComponent<Bullet>().damage = damage;
        Destroy(bullet, 10f);
    }
    public void StartGunCooldown(float timer)
    {
        StartCoroutine(GunCoolDown(timer));
    }
    public void Shotgun(Transform spawnPoint, Transform rotatePoint, GameObject prefab, int pellets)
    {
        for(int i = 0; i < pellets; i++) CreateBullet(0.35f, 30f, 1, spawnPoint, rotatePoint, prefab);
    }
    private IEnumerator GunCoolDown(float timer)
    {
        canShoot = false;
        yield return new WaitForSeconds(timer);
        canShoot = true;
    }
}
