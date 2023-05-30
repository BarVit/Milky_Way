using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasma_orb : MonoBehaviour
{
    private GameObject target;
    public float orb_speed;
    public float orb_periodic_damage;
    private Color alpha;
    private float a1 = 1f;
    private MeshRenderer meshRenderer;
    private float lifeTime;
    private Vector3 targetPosition = new Vector3();
    private bool isHit;
    private bool onEnter;
    int i = 0;
    //private float scaleUP_tick;
    private void Awake()
    {

    }
    void Start()
    {
        onEnter = false;
        //scaleUP_tick = 0.02f;
        isHit = false;
        lifeTime = 10f;
        alpha = GetComponent<MeshRenderer>().material.color;
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            isHit = true;
            onEnter = true;
            if (lifeTime > 4f)
            {
                Destroy(gameObject, 4);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(orb_periodic_damage * Time.deltaTime);
        }
    }
    private void FixedUpdate()
    {
        //Debug.DrawRay(transform.position, (targetPosition - transform.position) * 50);
        if (onEnter)
        {
            if (i < 200)
            {
                Scale_up();
                i++;
            }
        }
        lifeTime -= Time.fixedDeltaTime;
        if (lifeTime < 0) Destroy(gameObject);
        if (target != null)
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, orb_speed * Time.fixedDeltaTime);
        else if (target == null && !isHit)
        {
            Scale_up();
            if (orb_speed > 0)
                orb_speed -= orb_speed * 0.85f * Time.fixedDeltaTime;
            else
                orb_speed = 0;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, orb_speed * Time.fixedDeltaTime);
            if (lifeTime < 4)
                Destroy(gameObject, lifeTime);
            else
                Destroy(gameObject, 4);
        }
        else if (target == null && isHit)
        {
            transform.position = transform.position;
        }
    }
    private void Scale_up()
    {
        float intensity = 1f;
        Color emissionColor = meshRenderer.material.GetColor("_EmissionColor");
        if (a1 > 0.05f)
            a1 -= 0.006f;
        else
            a1 = 0;
        float x = 0.005f;
        transform.localScale = new Vector3(transform.localScale.x + x, transform.localScale.y + x, transform.localScale.z + x);
        alpha = new Color(alpha.r, alpha.g, alpha.b, a1);
        intensity -= 0.025f / 5;
        if (intensity < 0.03f) intensity = 0.03f;
        GetComponent<MeshRenderer>().material.color = alpha;
        meshRenderer.material.SetColor("_EmissionColor", emissionColor * intensity);
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