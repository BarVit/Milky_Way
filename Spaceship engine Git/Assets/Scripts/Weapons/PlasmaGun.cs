using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaGun : MonoBehaviour
{
    public GameObject plasmaPref;
    private GameObject plasma_orb;
    public float orb_speed;
    private float orb_periodic_damage = 10f;
    public float plasma_cd;
    private float cd_timer;
    void Start()
    {
        orb_speed = 10f;
        plasma_cd = 6f;
        cd_timer = 1f;
    }
    void Update()
    {
        cd_timer -= Time.deltaTime;
        if (cd_timer < 0)
        {
            plasma_orb = Instantiate(plasmaPref, transform.position, transform.rotation);
            plasma_orb.GetComponent<Plasma_orb>().orb_speed = orb_speed;
            plasma_orb.GetComponent<Plasma_orb>().orb_periodic_damage = orb_periodic_damage;
            Destroy(plasma_orb, 20);
            cd_timer = plasma_cd;
        }
    }
}