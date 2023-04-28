using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaussGun : MonoBehaviour
{
    private GameObject target;
    public GameObject shoot_point;
    private Quaternion q_target;
    private LineRenderer lr;
    public float speedRotation;
    private float cd_timer, rail_cd, rail_charge_and_shoot_cd;
    bool IsRailFire;
    public bool isFire { get; private set; }

    private float gauss_damage;
    private bool one_shot;
    private bool justShoot;

    private Vector3 shootPoint = new Vector3(0, 0, 0);
    private Vector3 transformForward = new Vector3(0,0,0);

    void Start()
    {
        justShoot = false;
        isFire = false;
        one_shot = false;
        gauss_damage = 30f;
        speedRotation = 20f;
        rail_cd = 5f;
        rail_charge_and_shoot_cd = 3f;
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        cd_timer = 1f;
        IsRailFire = false;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
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
    public void Fire()
    {
        rail_charge_and_shoot_cd -= Time.deltaTime;
        if (rail_charge_and_shoot_cd > 0)
        {
            if (rail_charge_and_shoot_cd < 1f)
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
            else if (rail_charge_and_shoot_cd < 3f)
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
            IsRailFire = false;
            rail_charge_and_shoot_cd = 3f;
        }
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
            one_shot = true;
            cd_timer = rail_cd;
        }
        if (IsRailFire)
        {
            Fire();
        }
    }
}
