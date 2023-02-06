using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasma_orb : MonoBehaviour
{
    public GameObject target;
    public float orb_speed;

    void Start()
    {
        target = GameObject.Find("Target");
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, orb_speed * Time.deltaTime);

    }
}
