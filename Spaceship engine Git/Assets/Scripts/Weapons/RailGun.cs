using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGun : MonoBehaviour
{
    private GameObject target;
    public GameObject shoot_point;
    private Quaternion q_target;
    LineRenderer lr;
    private float speedRotation;
    private float cd_timer, rail_cd, rail_shoot_cd;
    private bool IsRailFire;
    public bool isFire { get; private set; }

    void Start()
    {
        speedRotation = 10f;
        rail_cd = 5f;
        rail_shoot_cd = 1f;
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        cd_timer = 1f;
        IsRailFire = false;
    }
    private void FixedUpdate()
    {
        //Debug.DrawRay(shoot_point.transform.position, transform.forward * 200);
    }
    public void onFire()
    {
        isFire = true;
    }
    public void offFire()
    {
        isFire = false;
    }
    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
    void Update()
    {
        if (target != null)
        {
            q_target = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q_target, Time.deltaTime * speedRotation);
        }
        
        cd_timer -= Time.deltaTime;
        if (cd_timer < 0 && isFire)
        {
            lr.positionCount = 2;
            IsRailFire = true;
            cd_timer = rail_cd;
            lr.SetPosition(0, shoot_point.transform.position);
            lr.SetPosition(1, shoot_point.transform.position + transform.forward * 70);
        }
        if (IsRailFire)
        {
            rail_shoot_cd -= Time.deltaTime;
            if (rail_shoot_cd < 0)
            {
                lr.positionCount = 0;
                IsRailFire = false;
                rail_shoot_cd = 1f;
            }
        }
    }
}
