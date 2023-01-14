using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasma : MonoBehaviour
{
    public GameObject plasmaPref;
    private GameObject plasma_orb;
    private float plasma_cd = 4f;
    public float cd_timer = 0;
    void Start()
    {
        cd_timer = plasma_cd;
    }

    void Update()
    {
        cd_timer -= Time.deltaTime;
        if (cd_timer < 0)
        {
            plasma_orb = Instantiate(plasmaPref, transform.position, transform.rotation);
            Destroy(plasma_orb, 20);
            cd_timer = plasma_cd;
        }
    }
}