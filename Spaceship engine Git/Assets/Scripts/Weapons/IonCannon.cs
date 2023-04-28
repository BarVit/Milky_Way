using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IonCannon : MonoBehaviour
{
    public GameObject ionPref;
    private GameObject target;
    private GameObject ion_orb;
    private Quaternion q_target;
    public float orb_speed;
    public float orb_damage;
    public float orb_periodic_damage;
    public float speedRotation;
    public float ion_cd;
    private float cd_timer;
    public bool isFire { get; private set; }
    void Start()
    {
        orb_damage = 2f;
        orb_periodic_damage = 0.5f;
        orb_speed = 30f;
        speedRotation = 40f;
        ion_cd = 0.2f;
        cd_timer = ion_cd;
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
        ion_orb = Instantiate(ionPref, transform.position, transform.rotation);
        ion_orb.GetComponent<Ion_orb>().orb_speed = orb_speed;
        ion_orb.GetComponent<Ion_orb>().orb_damage = orb_damage;
        ion_orb.GetComponent<Ion_orb>().orb_periodic_damage = orb_periodic_damage;
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
            cd_timer -= Time.deltaTime;
            if (cd_timer < 0 && isFire)
            {
                Fire();
                cd_timer = ion_cd;
            }
        }
    }
}
