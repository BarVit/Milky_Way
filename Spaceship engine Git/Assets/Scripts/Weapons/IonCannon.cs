using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IonCannon : MonoBehaviour
{
    public GameObject ionPref;
    public GameObject target;
    private GameObject ion_orb;
    private Quaternion q_target;
    public float orb_speed;
    public float orb_damage = 5f;
    public float orb_periodic_damage = 1f;
    public float speedRotation = 50f;
    public float ion_cd;
    private float cd_timer;
    void Start()
    {
        orb_speed = 50f;
        ion_cd = 0.3f;
        cd_timer = ion_cd;
    }

    void Update()
    {
        q_target = Quaternion.LookRotation(transform.position - target.transform.position);
        q_target *= Quaternion.Euler(0, 180, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q_target, Time.deltaTime * speedRotation);
        cd_timer -= Time.deltaTime;
        if (cd_timer < 0)
        {
            ion_orb = Instantiate(ionPref, transform.position, transform.rotation);
            ion_orb.GetComponent<Ion_orb>().orb_speed = orb_speed;
            ion_orb.GetComponent<Ion_orb>().orb_damage = orb_damage;
            ion_orb.GetComponent<Ion_orb>().orb_periodic_damage = orb_periodic_damage;
            Destroy(ion_orb, 10);
            cd_timer = ion_cd;
        }
    }
}
