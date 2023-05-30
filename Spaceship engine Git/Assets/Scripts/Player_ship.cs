using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ship : MonoBehaviour
{
    public List<Weapon> _weapons = new List<Weapon>();

    public GameObject hp_bar;
    private EnemyWaveSpawner waveSpawner;
    public GameObject spaceBase;

    private LineRenderer orbitRender;
    private GameObject[] enemies;
    private List<GameObject> targets = new List<GameObject>();
    public GameObject target;
    private Movement movement;

    public GameObject ship;
    public float shield_hp = 200f;
    public float armor_resistance = 1.25f;
    public float hp = 100f;

    private float orbit_range = 30f;
    private float target_angle;
    //private Vector3 radius_vector;

    private float target_distance;
    //private float long_range = 250f;
    //private float short_range = 30f;


    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Weapon>() != null)
                _weapons.Add(transform.GetChild(i).GetComponent<Weapon>());
        }
        waveSpawner = GameObject.Find("GameManager").GetComponent<EnemyWaveSpawner>();
        orbitRender = gameObject.GetComponent<LineRenderer>();
        movement = gameObject.GetComponent<Movement>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        NextTarget();
        Where_target();
    }

    void Update()
    {
        Controller();
        TimeScale();
        if (target == null) NextTarget();
        else
        {
            Where_target();
            if (movement.moving_type != Movement.Moving_type.standby && target.tag == "Enemy")
            {
                foreach (Weapon w in _weapons)
                {
                    if (!w.isFire && target_distance < w.weapon_range * 0.9f && target_angle < w.weapon_targeting_angle)
                        w.onFire();
                    else
                        w.offFire();
                }
            }
            else if (movement.moving_type == Movement.Moving_type.standby || target == null)
            {
                foreach (Weapon w in _weapons)
                {
                    w.offFire();
                }
            }
        }
        
    }

    public void TakeDamage(float damage)
    {
        hp -= damage * armor_resistance;
        if (hp <= 0) Death();
    }
    private void Death()
    {
        ship.SetActive(false);
    }
    private void Where_target()
    {
        target_distance = (target.transform.position - transform.position).magnitude;
        target_angle = Vector3.Angle((target.transform.position - transform.position), transform.forward);
    }
    public void NextTarget()
    {
        targets.Clear();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null) targets.Add(enemies[i]);
        }
        if (targets.Count != 0)
        {
            float min_distance, distance;
            min_distance = (targets[0].transform.position - transform.position).sqrMagnitude;
            target = targets[0];
            movement.SetTarget(target);
            foreach (Weapon w in _weapons)
            {
                w.SetTarget(target);
            }
            for (int i = 0; i < targets.Count; i++)
            {
                distance = (targets[i].transform.position - transform.position).sqrMagnitude;
                if (distance < min_distance)
                {
                    distance = min_distance;
                    target = targets[i];
                    movement.SetTarget(target);
                    foreach (Weapon w in _weapons)
                    {
                        w.SetTarget(target);
                    }
                }
            }
        }
        else
        {
            waveSpawner.SpawnWave();
        }
        DrawOrbit(100, orbit_range);
    }
    private void Controller()
    {
        //Подлететь и атаковать
        if (Input.GetKeyDown(KeyCode.Alpha1))
            movement.MovingType('1');
        //Прямая атака с уходом
        if (Input.GetKeyDown(KeyCode.Alpha2))
            movement.MovingType('2');
        //Боковая орбита
        if (Input.GetKeyDown(KeyCode.Alpha3))
            movement.MovingType('3');
        //Круговая орбита
        if (Input.GetKeyDown(KeyCode.Alpha4))
            movement.MovingType('4');
        //Маневровая атака аса
        if (Input.GetKeyDown(KeyCode.Alpha5))
            movement.MovingType('5');
        //
        if (Input.GetKeyDown(KeyCode.Alpha0))
            movement.MovingType('0');
        //Состояние standby
        if (Input.GetKeyDown(KeyCode.Space))
            movement.MovingType('s');
        //вернуться на базу
        if (Input.GetKeyDown(KeyCode.B))
        {
            target = spaceBase;
            movement.SetTarget(target);
            movement.MovingType('b');
        }

        //полет в точку

    }
    private void TimeScale()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            Time.timeScale = 1f;
        if (Input.GetKeyDown(KeyCode.F2))
            Time.timeScale = 0.25f;
        if (Input.GetKeyDown(KeyCode.F3))
            Time.timeScale = 0.1f;
        if (Input.GetKeyDown(KeyCode.F4))
            Time.timeScale = 2f;
    }
    void DrawOrbit(int steps, float radius)
    {
        orbitRender.positionCount = steps + 1;
        Vector3 cPos0;
        for (int currentStep = 0; currentStep < steps; currentStep++)
        {
            float circumferenceProgress = (float)currentStep / steps;
            float currentRadian = circumferenceProgress * 2 * Mathf.PI;
            float xScaled = Mathf.Cos(currentRadian);
            float zScaled = Mathf.Sin(currentRadian);
            float x = xScaled * radius;
            float z = zScaled * radius;
            if (target != null)
            {
                if (currentStep == 0)
                {
                    cPos0 = new Vector3(x + target.transform.position.x, target.transform.position.y, z + target.transform.position.z);
                    orbitRender.SetPosition(steps, cPos0);
                }
                Vector3 currentPosition = new Vector3(x + target.transform.position.x, target.transform.position.y, z + target.transform.position.z);
                orbitRender.SetPosition(currentStep, currentPosition);
            }
        }
    }
}
