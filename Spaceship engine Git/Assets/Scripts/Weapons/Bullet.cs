using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bullet_speed;
    public float bullet_damage;
    private void Awake()
    {
        
    }
    void Update()
    {
        this.transform.Translate(Vector3.forward * Time.deltaTime * bullet_speed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(bullet_damage);
            Destroy(gameObject);
        }
    }
}