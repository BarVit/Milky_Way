using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Weapon
{
    public GameObject shoot_point;
    // ласт таргет для отключения лазера после убийства цели (или не отключать, еще не решил)
    private GameObject last_target;
    LineRenderer lr;
    private float laser_light_cd;
    private float laser_afterLight_cd;
    bool IsLaserFire;
    bool afterLight;
    private Color color;
    private float laser_damage;

    private void Awake()
    {
        
    }
    void Start()
    {
        ship = transform.parent;
        speedRotation = 20f;
        weapon_range = 70f;
        weapon_moving_angle = 360f;
        weapon_targeting_angle = 360f;
        laser_damage = 8f;
        weapon_cd = 2f;
        laser_light_cd = 5f;
        laser_afterLight_cd = 0;


        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        cd_timer = 1f;
        IsLaserFire = false;
        afterLight = false;
        color = Color.green;

        isFire = false;
        
    }
    private void FixedUpdate()
    {
        //Debug.DrawRay(shoot_point.transform.position, transform.forward * 50);
        
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
            if (cd_timer <= 0 && isFire)
            {
                IsLaserFire = true;
                cd_timer = weapon_cd + laser_light_cd;
                last_target = target;
            }

            if (laser_light_cd > 0 && IsLaserFire)
            {
                lr.positionCount = 2;
                laser_light_cd -= Time.fixedDeltaTime;
                Fire();
                if (last_target == null && !afterLight)
                {
                    if (laser_light_cd > 0.2f)
                    {
                        laser_afterLight_cd = laser_light_cd - 0.2f;
                        afterLight = true;
                    }
                }
            }
            if ((laser_light_cd - laser_afterLight_cd) <= 0)
            {
                lr.positionCount = 0;
                laser_light_cd = 5f;
                laser_afterLight_cd = 0;
                color = Color.green;
                IsLaserFire = false;
                afterLight = false;
            }
        }
        else
        {
            lr.positionCount = 0;
            laser_light_cd = 5f;
            color = Color.green;
        }
    }

    public override void Fire()
    {
        lr.SetPosition(0, shoot_point.transform.position);
        lr.SetPosition(1, shoot_point.transform.position + transform.forward * 50);
        lr.startColor = color;
        lr.endColor = color;
        //Debug.DrawRay(shoot_point.transform.position, transform.forward * 50);
        Ray ray = new Ray(shoot_point.transform.position, transform.forward * 50);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 50f))
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<Enemy>().TakeDamage(laser_damage * 0.02f);
            }
        }
        // переделать на градиент
        if (laser_light_cd < 0.5f)
            color = Color.red;
        else if (laser_light_cd < 2f)
            color = new Color(Mathf.Clamp(1 - (laser_light_cd - 0.5f) / 1.5f, 0, 1), Mathf.Clamp(0 + (laser_light_cd - 0.5f) / 1.5f, 0, 1), 0, 1);
        else
            color = Color.green;
    }

    void Update()
    {
    }
}
