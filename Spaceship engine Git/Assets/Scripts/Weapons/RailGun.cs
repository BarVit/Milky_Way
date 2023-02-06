using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGun : MonoBehaviour
{
    public GameObject shoot_point;
    public GameObject target;
    private Quaternion q_target;
    LineRenderer lr;
    public float speedRotation = 5f;
    private float cd_timer, gauss_cd, gauss_shoot_cd;
    bool IsGaussFire;

    void Start()
    {
        gauss_cd = 5f;
        gauss_shoot_cd = 1f;
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        cd_timer = gauss_cd;
        IsGaussFire = false;
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
            IsGaussFire = true;
            cd_timer = gauss_cd;
            lr.SetPosition(0, shoot_point.transform.position);
            lr.SetPosition(1, transform.forward * 70);
        }
        if (IsGaussFire)
        {
            gauss_shoot_cd -= Time.deltaTime;
            if (gauss_shoot_cd < 0)
            {
                lr.positionCount = 0;
                IsGaussFire = false;
                gauss_shoot_cd = 1f;
            }
        }
    }
}
