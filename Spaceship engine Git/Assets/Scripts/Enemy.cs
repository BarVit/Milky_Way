using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Image hp_bar;
    private Transform goToFind;
    //private float enemy_shield_hp = 100f;
    private float enemy_hp;
    private float enemy_max_hp = 50f;
    [SerializeField] private Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    private void Awake()
    {
        gradient = new Gradient();
        colorKey = new GradientColorKey[5];
        alphaKey = new GradientAlphaKey[5];

        for (int i = 0; i < 5; i++) alphaKey[i].alpha = 1f;
        alphaKey[0].time = 0.0f;
        alphaKey[0].time = 0.2f;
        alphaKey[0].time = 0.35f;
        alphaKey[0].time = 0.65f;
        alphaKey[0].time = 1.0f;

        colorKey[0].color = Color.red;
        colorKey[1].color = Color.red;
        colorKey[2].color = Color.yellow;
        colorKey[3].color = Color.yellow;
        colorKey[4].color = Color.green;
        colorKey[0].time = 0.0f;
        colorKey[1].time = 0.2f;
        colorKey[2].time = 0.35f;
        colorKey[3].time = 0.65f;
        colorKey[4].time = 1.0f;
        gradient.SetKeys(colorKey, alphaKey);
    }

    void Start()
    {
        goToFind = transform.GetChild(0).GetChild(0).GetChild(0);
        hp_bar = goToFind.GetComponent<Image>();
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
        if (enemy_hp <= 0)
        {
            Death();
        }
    }
    private void Death()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
