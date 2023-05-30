using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Weapon
{
    public GameObject bulletPref;
    private GameObject bullet;
    private float bullet_speed;
    private float bullet_damage;
    private void Start()
    {
        ship = transform.parent;
        weapon_moving_angle = 10f;
        weapon_targeting_angle = 20f;
        weapon_range = 60f;
        speedRotation = 20f;
        weapon_cd = 1f;
        cd_timer = 0f;
        isFire = false;
        bullet_speed = 60f;
        bullet_damage = 20f;
    }
    private void FixedUpdate()
    {
        //Weapon_Limits(weapon_moving_angle);
        //Debug.DrawRay(transform.position, weapon_left_limit * 100, Color.yellow);
        //Debug.DrawRay(transform.position, weapon_right_limit * 100, Color.yellow);
        //Debug.DrawRay(transform.position, transform.forward * 100, Color.red);
        //Debug.DrawRay(ship.position, ship.forward * 100, Color.green);
        //Debug.DrawRay(ship.position, (target.transform.position - ship.transform.position) * 100);

        if (weapon_moving_angle < 360)
            Weapon_to_target();

        cd_timer -= Time.fixedDeltaTime;
        if (cd_timer <= 0 && isFire)
        {
            Fire();
            cd_timer = weapon_cd;
        }
    }
    public override void Fire()
    {
        bullet = Instantiate(bulletPref, transform.position, transform.rotation);
        bullet.GetComponent<Bullet>().SetBulletStats(bullet_speed, bullet_damage);
        bullet.transform.localScale *= 2;
        Destroy(bullet, 1);
    }
    void Update()
    {
    }

}
