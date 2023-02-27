using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject shoot_point;
    public GameObject target;
    private Quaternion q_target;
    LineRenderer lr;
    public float speedRotation = 20f;
    private float cd_timer, laser_cd, laser_light_cd;
    bool IsLaserFire;
    private Color color;

    private float laser_damage = 5f;

    void Start()
    {
        
        laser_cd = 7f;
        laser_light_cd = 5f;
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        //обычное кд 7 сек, ставлю 2 сек для более быстрых тестов
        //cd_timer = laser_cd;
        cd_timer = 2f;
        IsLaserFire = false;
        color = Color.green;
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
            IsLaserFire = true;            
            cd_timer = laser_cd;
        }
        if (IsLaserFire)
        {
            laser_light_cd -= Time.deltaTime;
            if (laser_light_cd > 0)
            {
                lr.SetPosition(0, shoot_point.transform.position);
                lr.SetPosition(1, transform.forward * 50);
                lr.startColor = color;
                lr.endColor = color;
                Ray ray = new Ray(shoot_point.transform.position, transform.forward * 50);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "Enemy")
                        hit.collider.gameObject.GetComponent<Enemy>().TakeDamage(laser_damage * Time.deltaTime);
                }

                // переделать на градиент
                if (laser_light_cd < 0.5f)
                    color = Color.red;
                else if (laser_light_cd < 2f)
                    color = new Color(Mathf.Clamp(1 - (laser_light_cd - 0.5f) / 1.5f, 0, 1), Mathf.Clamp(0 + (laser_light_cd - 0.5f) / 1.5f, 0, 1), 0, 1);
                else
                    color = Color.green;
            }
            else
            {
                lr.positionCount = 0;
                IsLaserFire = false;
                laser_light_cd = 5f;
                color = Color.green;
            }            
        }
    }
}
