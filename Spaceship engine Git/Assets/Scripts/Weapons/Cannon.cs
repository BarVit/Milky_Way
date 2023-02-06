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
    void Start()
    {
        bullet_speed = 60f;
        bullet_cd = 2f;
        cd_timer = bullet_cd;
    }

    void Update()
    {
        if (cd_timer < 0)
        {
            bullet = Instantiate(bulletPref, transform.position, transform.rotation);
            bullet.GetComponent < Bullet > ().bullet_speed = bullet_speed;
            bullet.transform.localScale *= 2;
            Destroy(bullet, 5);
            cd_timer = bullet_cd;
        }
        cd_timer -= Time.deltaTime;
    }

    //rail gun, gauss gun
    //pulse laser, beam laser, photon laser (emitter), quantum laser
    //ion cannon - маленькие шары со скоростью чуть медленнее пули, но быстрее пушки
    //rockets, missiles, torpedos
}
