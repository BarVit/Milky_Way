using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasma_orb : MonoBehaviour
{
    public GameObject target;
    public float orb_speed;
    public float orb_periodic_damage;
    private Color alpha;
    private float a1 = 1f;
    private MeshRenderer meshRenderer;

    void Start()
    {
        target = GameObject.Find("Target");
        alpha = GetComponent<MeshRenderer>().material.color;
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            StartCoroutine(Scale_up());
            Destroy(gameObject, 4);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(orb_periodic_damage * Time.deltaTime);
        }
    }
    IEnumerator Scale_up()
    {
        float intensity = 1f;
        Color emissionColor = meshRenderer.material.GetColor("_EmissionColor");
        for (int i = 0; i < 40; i++)
        {
            if (a1 > 0.05f)
                a1 -= 0.03f;
            else
                a1 = 0;
            transform.localScale *= 1.02f;
            alpha = new Color(alpha.r, alpha.g, alpha.b, a1);
            intensity -= 0.025f;
            if (intensity < 0.03f) intensity = 0.03f;
            GetComponent<MeshRenderer>().material.color = alpha;
            meshRenderer.material.SetColor("_EmissionColor", emissionColor * intensity);
            yield return new WaitForSeconds(0.1f);
        }
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, orb_speed * Time.deltaTime);
    }
}
