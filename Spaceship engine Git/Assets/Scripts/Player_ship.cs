using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ship : MonoBehaviour
{
    public GameObject ship;
    public float shield_hp = 200f;
    private float armor_resistance = 25f / 100f;
    public float hp = 100f;


    void Start()
    {
        

    }

    void Update()
    {
        

    }

    public void TakeDamage(float damage)
    {

        hp -= damage * armor_resistance;
        if (hp < 0) Death();

    }
    private void Death()
    {
        ship.SetActive(false);
    }
}
