using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public enum Gun
    {
        Pistol,
        Rifle,
        Shotgun,
        Sniper
    }
 public static void CreateBullet(float range, float speed, int damage, Transform spawnPoint, Transform firePointRotation, GameObject bulletPrefab)
    {  
        
        GameObject bullet = Instantiate(bulletPrefab, new Vector3(spawnPoint.position.x, 
            Random.Range(spawnPoint.position.y - range, spawnPoint.position.y + range), 
            spawnPoint.position.z), firePointRotation.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePointRotation.right * speed;
        bullet.GetComponent<Bullet>().damage = damage;
        Destroy(bullet, 10f);
    }
}
