using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IonCannon : Weapon
{
    public GameObject ionPref;
    private GameObject ion_orb;
    public float orb_speed;
    public float orb_damage;
    public float orb_periodic_damage;
    void Start()
    {
        ship = transform.parent;
        weapon_range = 75f;
        weapon_moving_angle = 360f;
        weapon_targeting_angle = 360f;
        orb_damage = 2f;
        orb_periodic_damage = 0.5f;
        orb_speed = 30f;
        speedRotation = 40f;
        weapon_cd = 0.2f;
        cd_timer = 1f;
    }
    public override void Fire()
    {
        ion_orb = Instantiate(ionPref, transform.position, transform.rotation);
        ion_orb.GetComponent<Ion_orb>().orb_speed = orb_speed;
        ion_orb.GetComponent<Ion_orb>().orb_damage = orb_damage;
        ion_orb.GetComponent<Ion_orb>().orb_periodic_damage = orb_periodic_damage;
    }
    private void FixedUpdate()
    {
        if (weapon_targeting_angle < 360f)
        {
            Weapon_to_target();
        }
        else if (target != null)
        {
            q_target = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q_target, Time.fixedDeltaTime * speedRotation);
        }
        if (target != null)
        {
            cd_timer -= Time.fixedDeltaTime;
            if (cd_timer < 0 && isFire)
            {
                Fire();
                cd_timer = weapon_cd;
            }
        }
    }
    void Update()
    {
    }
}
