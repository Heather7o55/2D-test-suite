using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BaseEntity
{
    public int damage = 1;
    public int ricochets = 0;
    void Start() {Setup();}
    void OnCollisionEnter2D(Collision2D col)
    {
        if(ricochets > 0) return;
        if(col.gameObject.CompareTag("Enemy"))
            col.gameObject.GetComponent<BaseEntity>()?.ModifyHealth(damage);
        ModifyHealth(1);
    }
}
