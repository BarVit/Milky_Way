using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private Player_ship player;
    public GameObject shoot_point;
    private GameObject target;
    private GameObject last_target;
    private Quaternion q_target;
    LineRenderer lr;
    public float speedRotation = 20f;
    private float cd_timer, laser_cd, laser_light_cd;
    bool IsLaserFire;
    private Color color;
    public bool isFire { get; private set; }

    private float laser_damage;

    private void Awake()
    {
        player = GameObject.Find("Ship_2").GetComponent<Player_ship>();
        target = player.target;
    }
    void Start()
    {
        laser_damage = 12f;
        laser_cd = 7f;
        laser_light_cd = 5f;
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        //обычное кд 7 сек, ставлю 2 сек для более быстрых тестов
        //cd_timer = laser_cd;
        cd_timer = 2f;
        IsLaserFire = false;
        color = Color.green;

        isFire = false;
    }
    private void FixedUpdate()
    {
        Debug.DrawRay(shoot_point.transform.position, transform.forward * 50);
        if (target != null)
        {
            q_target = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q_target, Time.deltaTime * speedRotation);
            cd_timer -= Time.deltaTime;
            if (cd_timer <= 0 && isFire)
            {
                IsLaserFire = true;
                cd_timer = laser_cd + laser_light_cd;
            }

            if (laser_light_cd > 0 && IsLaserFire)
            {
                lr.positionCount = 2;
                laser_light_cd -= 0.02f;
                last_target = target;
                Fire();
            }
            else if (laser_light_cd <= 0)
            {
                lr.positionCount = 0;
                laser_light_cd = 5f;
                color = Color.green;
                IsLaserFire = false;
            }
        }
        else
        {
            lr.positionCount = 0;
            laser_light_cd = 5f;
            color = Color.green;
        }
    }
    public void onFire()
    {
        isFire = true;
    }
    public void offFire()
    {
        isFire = false;
    }
    private void Fire()
    {
        lr.SetPosition(0, shoot_point.transform.position);
        lr.SetPosition(1, shoot_point.transform.position + transform.forward * 50);
        lr.startColor = color;
        lr.endColor = color;
        Debug.DrawRay(shoot_point.transform.position, transform.forward * 50);
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
    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
    void Update()
    {

    }
}
