using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bullet_speed;
    private float bullet_damage;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * bullet_speed);
    }
    public void SetBulletStats (float speed, float damage)
    {
        bullet_speed = speed;
        bullet_damage = damage;
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