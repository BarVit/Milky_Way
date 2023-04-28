using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaGun : MonoBehaviour
{
    private GameObject sendTarget;
    public GameObject plasmaPref;
    private GameObject plasma_orb;
    private float orb_speed;
    private float orb_periodic_damage;
    public float plasma_cd;
    private float cd_timer;
    public bool isFire { get; private set; }
    void Start()
    {
        orb_periodic_damage = 2f;
        orb_speed = 10f;
        plasma_cd = 4f;
        cd_timer = 1f;
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
        plasma_orb = Instantiate(plasmaPref, transform.position, transform.rotation);
        plasma_orb.GetComponent<Plasma_orb>().orb_speed = orb_speed;
        plasma_orb.GetComponent<Plasma_orb>().orb_periodic_damage = orb_periodic_damage;
        plasma_orb.GetComponent<Plasma_orb>().SetTarget(sendTarget);
    }
    public void SetTarget(GameObject target)
    {
        this.sendTarget = target;
    }
    void Update()
    {
        cd_timer -= Time.deltaTime;
        if (cd_timer < 0 && isFire)
        {
            Fire();
            cd_timer = plasma_cd;
        }
    }
}