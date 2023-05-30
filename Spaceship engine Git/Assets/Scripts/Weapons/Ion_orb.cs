using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ion_orb : MonoBehaviour
{
    private GameObject target;
    public float orb_speed;
    private float orb_turn_speed;
    public float orb_damage;
    public float orb_periodic_damage;
    private float periodic_cd;
    private bool doPeriodicDamage = false;
    private Vector3 toTargetCenterAfterHit;

    private Vector3 targetPosition = new Vector3();
    private Quaternion q_target;
    private float lifeTime;

    void Start()
    {
        lifeTime = 2.5f;
        orb_turn_speed = 20f;
        periodic_cd = 0.04f;
        toTargetCenterAfterHit = Vector3.forward;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(orb_damage);
            doPeriodicDamage = true;
            //toTargetCenterAfterHit = target.transform.position;
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
            transform.localScale *= 1.1f;
        }
    }
    private void FixedUpdate()
    {
        lifeTime -= Time.fixedDeltaTime;
        if (lifeTime < 0) Destroy(gameObject);
        if (doPeriodicDamage)
        {
            periodic_cd -= Time.fixedDeltaTime;
            if (target != null)
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, orb_speed * 0.1f * Time.fixedDeltaTime);
            else
                transform.position = transform.position;
        }
        else
        {
            if (target != null)
            {
                q_target = Quaternion.LookRotation(target.transform.position - transform.position);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, q_target, Time.fixedDeltaTime * orb_turn_speed);
                transform.Translate(Vector3.forward * orb_speed * Time.fixedDeltaTime);
            }
            else if (target == null)
            {
                if (orb_speed > 0)
                    orb_speed -= orb_speed * 0.05f * Time.fixedDeltaTime;
                else
                    orb_speed = 0;
                transform.Translate(Vector3.forward * orb_speed * Time.fixedDeltaTime);
            }
        }
    }
    public void SetTarget(GameObject target)
    {
        this.target = target;
        targetPosition = target.transform.position;
    }
    void Update()
    {
    }
}
