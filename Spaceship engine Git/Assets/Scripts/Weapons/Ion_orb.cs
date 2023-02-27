using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ion_orb : MonoBehaviour
{
    public GameObject target;
    public float orb_speed;
    public float orb_damage;
    public float orb_periodic_damage;
    private float periodic_cd = 0.04f;
    private bool doPeriodicDamage = false;
    private Vector3 toTargetCenterAfterHit;

    private void Awake()
    {
        
    }
    void Start()
    {
        target = GameObject.Find("Target");
        toTargetCenterAfterHit = Vector3.forward;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(orb_damage);
            doPeriodicDamage = true;
            toTargetCenterAfterHit = target.transform.position;
            transform.localScale *= 1.2f;
            Destroy(gameObject, 0.2f);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && periodic_cd < 0)
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(orb_periodic_damage);
            periodic_cd = 0.04f;
            transform.localScale *= 1.2f;
        }
    }
    void Update()
    {
        if (doPeriodicDamage)
        {
            periodic_cd -= Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, orb_speed * 0.1f * Time.deltaTime);
        }
        else
        {
            transform.Translate(toTargetCenterAfterHit * Time.deltaTime * orb_speed);
        }
    }
}
