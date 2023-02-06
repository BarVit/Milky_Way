using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bullet_speed;
    private void Awake()
    {
        
    }
    void Update()
    {
        this.transform.Translate(Vector3.forward * Time.deltaTime * bullet_speed);
    }
}