using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speedRotation = 20;
    public GameObject shoot_point;
    public GameObject target;
    public Quaternion q_target;
    LineRenderer lr;
    private float cd_timer, laser_cd = 5, laser_light_cd = 1.5f;
    bool laserFire;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        cd_timer = laser_cd;
        laserFire = false;
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
            laserFire = true;            
            cd_timer = laser_cd;
        }
        if (laserFire)
        {
            laser_light_cd -= Time.deltaTime;
            if (laser_light_cd > 0)
            {
                lr.SetPosition(0, shoot_point.transform.position);
                lr.SetPosition(1, transform.forward * 50);
            }
            else
            {
                lr.positionCount = 0;
                laserFire = false;
                laser_light_cd = 1.5f;
            }            
        }
    }
}
