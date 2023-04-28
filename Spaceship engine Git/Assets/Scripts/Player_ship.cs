using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ship : MonoBehaviour
{
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
    private Cannon weapon1;
    private AutoCannon weapon2;
    private Laser weapon3;
    private PlasmaGun weapon4;
    private IonCannon weapon5;
    private GaussGun weapon6;
    private RailGun weapon7;
    private float orbit_range = 30f;

    private float target_angle;
    private Vector3 radius_vector;

    private float target_distance;
    private float long_range = 250f;
    private float short_range = 30f;


    void Start()
    {
        waveSpawner = GameObject.Find("GameManager").GetComponent<EnemyWaveSpawner>();
        orbitRender = gameObject.GetComponent<LineRenderer>();
        movement = gameObject.GetComponent<Movement>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");


        //weapon1 = transform.Find("Cannon").GetComponent<Cannon>();
        //weapon2 = transform.Find("AutoCannon").GetComponent<AutoCannon>();
        //weapon3 = transform.Find("Laser").GetComponent<Laser>();
        weapon4 = GetComponentInChildren<PlasmaGun>();
        //weapon5 = GetComponentInChildren<IonCannon>();
        //weapon6 = GetComponentInChildren<GaussGun>();
        //weapon7 = GetComponentInChildren<RailGun>();

        NextTarget();
        Where_target();
    }

    void Update()
    {
        Controller();
        TimeScale();
        if (target == null) NextTarget();
        if (target != null) Where_target();
        if (weapon1 != null)
        {
            if (!weapon1.isFire && target_distance < 50f && target_angle < 15f && movement.moving_type != Movement.Moving_type.standby)
                weapon1.onFire();
            else
                weapon1.offFire();
        }
        if (weapon2 != null)
        {
            if (!weapon2.isFire && target_distance < 35f && target_angle < 15f && movement.moving_type != Movement.Moving_type.standby)
                weapon2.onFire();
            else
                weapon2.offFire();
        }
        if (weapon3 != null)
        {
            if (!weapon3.isFire && target_distance < 45f && movement.moving_type != Movement.Moving_type.standby)
                weapon3.onFire();
            else
                weapon3.offFire();
        }
        if (weapon4 != null && target != null)
        {
            if (!weapon4.isFire && target_distance < 100f && movement.moving_type != Movement.Moving_type.standby)
                weapon4.onFire();
            else
                weapon4.offFire();
        }
        if (weapon5 != null && target != null)
        {
            if (!weapon5.isFire && target_distance < 80f && movement.moving_type != Movement.Moving_type.standby)
                weapon5.onFire();
            else
                weapon5.offFire();
        }
        if (weapon6 != null && target != null)
        {
            if (!weapon6.isFire && target_distance < 180f && movement.moving_type != Movement.Moving_type.standby)
                weapon6.onFire();
            else
                weapon6.offFire();
        }
        if (weapon7 != null && target != null)
        {
            if (!weapon7.isFire && target_distance < 90f && movement.moving_type != Movement.Moving_type.standby)
                weapon7.onFire();
            else
                weapon7.offFire();
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
            weapon4.SetTarget(target);
            for (int i = 0; i < targets.Count; i++)
            {
                distance = (targets[i].transform.position - transform.position).sqrMagnitude;
                if (distance < min_distance)
                {
                    distance = min_distance;
                    target = targets[i];
                    movement.SetTarget(target);
                    weapon4.SetTarget(target);
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
            movement.MovingType('b');
            target = spaceBase;
            movement.SetTarget(target);
            weapon3.offFire();
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
