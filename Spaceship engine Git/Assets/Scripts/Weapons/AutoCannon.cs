using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCannon : Weapon
{
    public GameObject bulletPref;
    private GameObject bullet;
    public int bullets_in_clip;
    public float bullet_speed;
    public float bullet_damage;
    public float bullet_cd;
    private List<Transform> turrets = new List<Transform>();
    private bool autoCannonFire;
    int fixedupdate_ticks = 0;
    private int turretNumber;
    void Start()
    {
        ship = transform.parent;
        weapon_moving_angle = 30f;
        weapon_targeting_angle = 45f;
        weapon_range = 60f;
        speedRotation = 50f;
        weapon_cd = 6f;
        cd_timer = 1f;
        isFire = false;
        autoCannonFire = false;

        turretNumber = 0;
        bullets_in_clip = 50;
        bullet_speed = 120f;
        bullet_damage = 1f;
        bullet_cd = 0.06f;
        foreach (Transform turret in transform)
        {
            turrets.Add(turret);
        }
    }
    private void FixedUpdate()
    {
        if (weapon_moving_angle < 360)
            Weapon_to_target();

        cd_timer -= Time.fixedDeltaTime;
        if (cd_timer <= 0 && isFire)
        {
            autoCannonFire = true;
            cd_timer = weapon_cd;
        }
        if (autoCannonFire)
        {
            if (fixedupdate_ticks % 3 == 0)
                Fire();
            fixedupdate_ticks++;
        }
        else
            fixedupdate_ticks = 0;
    }
    public override void Fire()
    {
        bullet = Instantiate(bulletPref, turrets[turretNumber].position, turrets[turretNumber].rotation);
        bullet.GetComponent<Bullet>().SetBulletStats(bullet_speed, bullet_damage);
        Destroy(bullet, 0.5f);
        turretNumber++;
        if (turretNumber == transform.childCount)
            turretNumber = 0;
        bullets_in_clip--;
        if (bullets_in_clip == 0)
        {
            autoCannonFire = false;
            bullets_in_clip = 50;
        }
    }
    void Update()
    {
    }
}
