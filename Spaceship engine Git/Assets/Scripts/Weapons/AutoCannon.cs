using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCannon : MonoBehaviour
{

    public GameObject bulletPref;
    private GameObject bullet;
    public int bullets_in_clip;
    public float bullet_speed;
    public float bullet_cd;
    public float reload_cd;
    private float cd_timer;
    private List<Transform> turrets = new List<Transform>();
    void Start()
    {
        bullets_in_clip = 20;
        bullet_speed = 120f;
        bullet_cd = 0.05f;
        reload_cd = 6f;
        cd_timer = reload_cd;
        foreach(Transform turret in transform)
        {
            turrets.Add(turret);
        }
    }

    IEnumerator Fire()
    {
        int i = 0;
        while (bullets_in_clip > 0)
        {
            bullet = Instantiate(bulletPref, turrets[i].position, turrets[i].rotation);
            i++;
            if (i == transform.childCount)
                i = 0;
            bullet.GetComponent<Bullet>().bullet_speed = bullet_speed;
            Destroy(bullet, 2);
            bullets_in_clip--;
            yield return new WaitForSeconds(bullet_cd);
        }
        if (bullets_in_clip == 0)
            bullets_in_clip = 20;
    }
    void Update()
    {
        if (cd_timer < 0)
        {
            StartCoroutine(Fire());
            cd_timer = reload_cd;
        }
        cd_timer -= Time.deltaTime;
    }
}
