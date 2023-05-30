using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public Transform ship;
    public GameObject target;
    public bool isFire;
    public float weapon_cd;
    public float cd_timer;
    public float speedRotation;
    public float weapon_range;
    //угол наводки, возможного вращения оружия, всегда меньше угла targeting
    public float weapon_moving_angle;
    //угол, при котором оружие может начать стрельбу, всегда больше угла moving
    public float weapon_targeting_angle;
    public Vector3 weapon_left_limit;
    public Vector3 weapon_right_limit;
    public float ship_to_target_angle;
    public float ship_to_weapon_angle;
    public Quaternion q_target;

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
    public void onFire()
    {
        isFire = true;
    }
    public void offFire()
    {
        isFire = false;
    }
    public abstract void Fire();
    public void Weapon_Limits(float weapon_moving)
    {
        weapon_left_limit = Quaternion.AngleAxis(-weapon_moving / 2, Vector3.up) * ship.forward;
        weapon_right_limit = Quaternion.AngleAxis(weapon_moving / 2, Vector3.up) * ship.forward;
    }
    public void Rotating_weapon()
    {
        ship_to_target_angle = Vector3.SignedAngle(ship.forward, (target.transform.position - transform.position), Vector3.up);
        ship_to_weapon_angle = Vector3.SignedAngle(ship.forward, transform.forward, Vector3.up);
        if (Mathf.Abs(ship_to_weapon_angle) < weapon_moving_angle / 2)
        {
            q_target = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q_target, speedRotation * Time.fixedDeltaTime);
        }
        else if ((ship_to_weapon_angle < 0 && ship_to_target_angle > ship_to_weapon_angle) || (ship_to_weapon_angle > 0 && ship_to_target_angle < ship_to_weapon_angle))
        {
            q_target = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q_target, speedRotation * Time.fixedDeltaTime);
        }
    }
    public void Weapon_to_target()
    {
        if (target != null && weapon_moving_angle > 0)
        {
            Rotating_weapon();
        }
        else if (target == null && weapon_moving_angle > 0)
        {
            //centering weapon if no target
            q_target = Quaternion.LookRotation(transform.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q_target, speedRotation * Time.fixedDeltaTime);
        }
        else if (weapon_moving_angle == 0)
        {
            transform.rotation = Quaternion.LookRotation(transform.forward);
        }
    }
}
