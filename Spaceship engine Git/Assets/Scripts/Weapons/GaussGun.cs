using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaussGun : Weapon
{
    public GameObject shoot_point;
    private LineRenderer lr;
    private float gauss_charge_and_shoot_cd;
    bool IsGaussFire;

    private float gauss_damage;
    private bool one_shot;
    private bool justShoot;

    private Vector3 shootPoint = new Vector3(0, 0, 0);
    private Vector3 transformForward = new Vector3(0, 0, 0);

    void Start()
    {
        ship = transform.parent;
        weapon_moving_angle = 60f;
        weapon_targeting_angle = 60f;
        weapon_range = 100f;
        justShoot = false;
        isFire = false;
        one_shot = false;
        speedRotation = 20f;
        weapon_cd = 5f;
        gauss_charge_and_shoot_cd = 3f;

        gauss_damage = 30f;

        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        cd_timer = 1f;
        IsGaussFire = false;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
    }
    private void FixedUpdate()
    {
        //Weapon_Limits(weapon_moving_angle);
        //Debug.DrawRay(transform.position, weapon_left_limit * 100, Color.yellow);
        //Debug.DrawRay(transform.position, weapon_right_limit * 100, Color.yellow);
        //Debug.DrawRay(transform.position, transform.forward * 100, Color.red);
        //Debug.DrawRay(ship.position, ship.forward * 100, Color.green);
        //Debug.DrawRay(ship.position, (target.transform.position - ship.transform.position) * 100);
        if (weapon_targeting_angle < 360)
            Weapon_to_target();

        cd_timer -= Time.fixedDeltaTime;
        if (cd_timer < 0 && isFire)
        {
            lr.positionCount = 2;
            IsGaussFire = true;
            one_shot = true;
            cd_timer = weapon_cd;
        }
        if (IsGaussFire)
            Fire();
    }
    public override void Fire()
    {
        gauss_charge_and_shoot_cd -= Time.fixedDeltaTime;
        if (gauss_charge_and_shoot_cd > 0)
        {
            if (gauss_charge_and_shoot_cd < 1f)
            {
                lr.startWidth = 0.5f;
                lr.endWidth = 0.5f;
                lr.startColor = Color.gray;
                lr.endColor = Color.gray;
                if (one_shot)
                {
                    if (!justShoot)
                    {
                        shootPoint = shoot_point.transform.position;
                        transformForward = transform.forward;
                    }
                    justShoot = true;
                    Ray ray = new Ray(shoot_point.transform.position, transform.forward * 100);
                    RaycastHit[] hits = Physics.RaycastAll(ray, 100f);
                    if (hits.Length > 0)
                    {
                        foreach (RaycastHit hit in hits)
                        {
                            if (hit.collider.gameObject.tag == "Enemy")
                                hit.collider.gameObject.GetComponent<Enemy>().TakeDamage(gauss_damage);
                        }
                    }
                }
                lr.SetPosition(0, shootPoint);
                lr.SetPosition(1, shootPoint + transformForward * 100);
                one_shot = false;
            }
            else if (gauss_charge_and_shoot_cd < 3f)
            {
                lr.SetPosition(0, shoot_point.transform.position);
                lr.SetPosition(1, shoot_point.transform.position + transform.forward * 100);
                lr.startWidth = 0.1f;
                lr.endWidth = 0.1f;
                lr.startColor = Color.red;
                lr.endColor = Color.red;
            }
        }
        else
        {
            justShoot = false;
            lr.positionCount = 0;
            IsGaussFire = false;
            gauss_charge_and_shoot_cd = 3f;
        }
    }
    void Update()
    {
    }
}
