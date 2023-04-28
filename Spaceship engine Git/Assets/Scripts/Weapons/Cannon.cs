using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject bulletPref;
    private GameObject bullet;
    public float bullet_speed;
    public float shoot_cd;
    private float cd_timer;
    private float bullet_damage;
    public bool isFire {get; private set; }
    void Start()
    {
        bullet_speed = 60f;
        bullet_damage = 20f;
        shoot_cd = 1f;
        cd_timer = 0f;
        isFire = false;
    }
    public void onFire()
    {
        isFire = true;
    }
    public void offFire()
    {
        isFire = false;
    }
    public void Fire()
    {
        bullet = Instantiate(bulletPref, transform.position, transform.rotation);
        bullet.GetComponent<Bullet>().SetBulletStats(bullet_speed, bullet_damage);
        bullet.transform.localScale *= 2;
        Destroy(bullet, 1);
    }
    void Update()
    {
        if (cd_timer <= 0 && isFire)
        {
            Fire();
            cd_timer = shoot_cd;
        }
        cd_timer -= Time.deltaTime;
    }

}
