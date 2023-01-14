using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject bulletPref;
    private GameObject bullet;
    private float bullet_cd = 2f;
    public float cd_timer = 0;
    void Start()
    {
        cd_timer = bullet_cd;
    }

    void Update()
    {
        cd_timer -= Time.deltaTime;
        if (cd_timer < 0)
        {
            bullet = Instantiate(bulletPref, transform.position, transform.rotation);
            Destroy(bullet, 5);
            cd_timer = bullet_cd;
        }        
    }

    //GameObject obj = Instantiate(prefab);
    //obj.GetComponent<Component>().Property = Something;


    //cannon, auto cannon
    //machine gun, gatling gun
    //howitzer
    //artillery
    //rail gun, gauss gun
    //pulse laser, beam laser, photon laser (emitter), quantum laser
    //tachyon beam laser (cannon, gun)
    //plasma cannon
    //ion cannon
    //rockets, missiles, torpedos
}
