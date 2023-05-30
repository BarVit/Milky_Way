using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGun : Weapon
{
    public GameObject shoot_point;
    LineRenderer lr;
    private float rail_shoot_cd;
    private float rail_damage;
    private bool IsRailFire;

    void Start()
    {
        ship = transform.parent;
        weapon_range = 80f;
        weapon_moving_angle = 360f;
        weapon_targeting_angle = 360f;
        rail_damage = 30f;
        speedRotation = 10f;
        weapon_cd = 4f;
        rail_shoot_cd = 1f;


        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        cd_timer = 1f;
        IsRailFire = false;
    }
    private void FixedUpdate()
    {
        //Debug.DrawRay(shoot_point.transform.position, transform.forward * 200);
        if (weapon_targeting_angle < 360f)
        {
            Weapon_to_target();
        }
        else if (target != null)
        {
            q_target = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q_target, Time.fixedDeltaTime * speedRotation);
        }

        cd_timer -= Time.fixedDeltaTime;
        if (cd_timer < 0 && isFire)
        {
            lr.positionCount = 2;
            IsRailFire = true;
            cd_timer = weapon_cd;
            Fire();
        }
        if (IsRailFire)
        {
            rail_shoot_cd -= Time.fixedDeltaTime;
            if (rail_shoot_cd < 0)
            {
                lr.positionCount = 0;
                IsRailFire = false;
                rail_shoot_cd = 1f;
            }
        }
    }
    public override void Fire()
    {
        lr.SetPosition(0, shoot_point.transform.position);
        lr.SetPosition(1, shoot_point.transform.position + transform.forward * 100);
        Ray ray = new Ray(shoot_point.transform.position, transform.forward * 100);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<Enemy>().TakeDamage(rail_damage);
            }
        }
    }
    void Update()
    {
    }
}
