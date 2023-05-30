using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaGun : Weapon
{
    public GameObject plasmaPref;
    private GameObject plasma_orb;
    private float orb_speed;
    private float orb_periodic_damage;
    void Start()
    {
        //no rotating weapon, auto guidance
        ship = transform.parent;
        weapon_range = 100f;
        weapon_moving_angle = 360f;
        weapon_targeting_angle = 360f;
        orb_periodic_damage = 7f;
        orb_speed = 10f;
        weapon_cd = 5f;
        cd_timer = 1f;
    }
    
    public override void Fire()
    {
        plasma_orb = Instantiate(plasmaPref, transform.position, transform.rotation);
        plasma_orb.GetComponent<Plasma_orb>().orb_speed = orb_speed;
        plasma_orb.GetComponent<Plasma_orb>().orb_periodic_damage = orb_periodic_damage;
        plasma_orb.GetComponent<Plasma_orb>().SetTarget(target);
    }
    private void FixedUpdate()
    {
        cd_timer -= Time.fixedDeltaTime;
        if (cd_timer < 0 && isFire)
        {
            Fire();
            cd_timer = weapon_cd;
        }
    }
    void Update()
    {
    }
}