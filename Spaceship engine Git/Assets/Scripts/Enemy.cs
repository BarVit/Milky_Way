using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Image hp_bar;
    public float enemy_shield_hp = 100f;
    public float enemy_hp;
    public float enemy_max_hp = 50f;
    [SerializeField] Gradient gradient;

    void Start()
    {
        enemy_hp = enemy_max_hp;
        hp_bar.fillAmount = enemy_hp / enemy_max_hp;
        hp_bar.color = gradient.Evaluate(enemy_hp / enemy_max_hp);
    }

    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        enemy_hp -= damage;
        hp_bar.fillAmount = enemy_hp / enemy_max_hp;
        hp_bar.color = gradient.Evaluate(enemy_hp / enemy_max_hp);
        if (enemy_hp <= 0) Death();
    }
    private void Death()
    {
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
