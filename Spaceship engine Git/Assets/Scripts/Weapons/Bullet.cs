using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bullet_speed = 50f;
    void Update()
    {
        this.transform.Translate(Vector3.forward * Time.deltaTime * bullet_speed);
    }
}