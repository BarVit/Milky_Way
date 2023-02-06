using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaussGun : MonoBehaviour
{
    public GameObject shoot_point;
    public GameObject target;
    private Quaternion q_target;
    LineRenderer lr;
    public float speedRotation = 10f;
    private float cd_timer, rail_cd, rail_charge_and_shoot_cd;
    private float width;
    bool IsRailFire;
    private Color color;

    void Start()
    {
        rail_cd = 4f;
        rail_charge_and_shoot_cd = 2.5f;
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        cd_timer = rail_cd;
        IsRailFire = false;
        color = Color.red;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
    }
    private void FixedUpdate()
    {
        //Debug.DrawRay(shoot_point.transform.position, transform.forward * 200);
    }
    void Update()
    {
        q_target = Quaternion.LookRotation(transform.position - target.transform.position);
        q_target *= Quaternion.Euler(0, 180, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q_target, Time.deltaTime * speedRotation);
        cd_timer -= Time.deltaTime;
        if (cd_timer < 0)
        {
            lr.positionCount = 2;
            IsRailFire = true;
            cd_timer = rail_cd;
        }
        if (IsRailFire)
        {
            rail_charge_and_shoot_cd -= Time.deltaTime;
            lr.SetPosition(0, shoot_point.transform.position);
            lr.SetPosition(1, transform.forward * 30);
            if (rail_charge_and_shoot_cd > 0)
            {
                if (rail_charge_and_shoot_cd < 0.5f)
                {
                    lr.startWidth = 0.5f;
                    lr.endWidth = 0.5f;
                    lr.startColor = Color.gray;
                    lr.endColor = Color.gray;
                }
                else if (rail_charge_and_shoot_cd < 2.5f)
                {
                    lr.startWidth = 0.1f;
                    lr.endWidth = 0.1f;
                    lr.startColor = Color.red;
                    lr.endColor = Color.red;
                }
            }
            else
            {
                lr.positionCount = 0;
                IsRailFire = false;
                rail_charge_and_shoot_cd = 2.5f;
            }
        }
    }
}
