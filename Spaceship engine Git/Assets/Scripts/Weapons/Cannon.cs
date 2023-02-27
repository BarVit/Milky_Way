using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject bulletPref;
    private GameObject bullet;
    public float bullet_speed;
    public float bullet_cd;
    private float cd_timer;
    private float damage;
    void Start()
    {
        bullet_speed = 60f;
        bullet_cd = 2f;
        cd_timer = bullet_cd;
        damage = 10f;
    }

    void Update()
    {
        if (cd_timer < 0)
        {
            bullet = Instantiate(bulletPref, transform.position, transform.rotation);
            bullet.GetComponent < Bullet > ().bullet_speed = bullet_speed;
            bullet.GetComponent<Bullet>().bullet_damage = damage;
            bullet.transform.localScale *= 2;
            Destroy(bullet, 5);
            cd_timer = bullet_cd;
        }
        cd_timer -= Time.deltaTime;
    }

}
