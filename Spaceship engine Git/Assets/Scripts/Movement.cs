using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject target;
    public GameObject orbitPoint;
    public float ship_speed;
    public float ship_real_speed;
    float ship_max_back_speed;
    [SerializeField] float ship_side_speed;
    float ship_max_side_speed;

    [SerializeField] float ship_calc_speed;
    [SerializeField] float ship_calc_turn_speed;
    public float ship_turn_speed;
    [SerializeField] int ship_turn_side;
    public float ship_max_speed;
    public float ship_max_turn_speed;
    
    private Quaternion q_ship;
    private Vector3 vector3_check;

    [SerializeField] float target_speed;
    [SerializeField] private float target_angle;
    [SerializeField] public string target_orient;
    [SerializeField] private string target_range;
    private string target_side;

    private string moving_type;
    [SerializeField] float target_distance;
    //private bool IsShoot = false;

    //заданная орбита
    public float orbit_range = 30f;
    //расчетная орбита
    [SerializeField] float orbit_radius;

    //private float shoot_range = 35f;
    //private bool InWeaponRange = false;
    private float long_range = 250f;
    private float short_range = 30f;
    private float ship_break_distance;

    private float fi;

    bool isBusy = false;

    bool oddFrame = true;
    float distance1, distance2;

    void Start()
    {
        Where_target();
        ship_speed = 0;
        ship_max_speed = 50f;
        ship_max_back_speed = -10f;
        ship_max_turn_speed = 50f;
        ship_turn_speed = 30f;
        ship_side_speed = 0;
        ship_max_side_speed = 0.2f * ship_max_speed;

        //target_speed = target.GetComponent<enemy_move>().speed;
        //target_speed = target.GetComponent<enemy_move>().side_speed;
        Ship_start();

        distance1 = distance2 = (this.transform.position - target.transform.position).magnitude;
        target_speed = 0;
    }
    private void FixedUpdate()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward * 200);
        //Debug.DrawRay(this.transform.position, (target.transform.position - this.transform.position) * 50);
    }
    private void Ship_start()
    {
        Debug.Log("Start");
        //Горизонтальное покачивание в режиме покоя
        //123

    }

    IEnumerator Ship_front_attack()
    {
        Debug.Log("Front attack");
        while (true)
        {
            switch (target_range)
            {
                case "longrange":
                    if (target_orient == "back")
                    {
                        if (ship_speed > 10)
                        {
                            ship_speed -= 3f;
                        }
                        else
                        {
                            ship_speed += 1f;
                        }
                        if (ship_turn_speed < 50)
                        {
                            ship_turn_speed += 2f;
                        }
                    }
                    if (target_orient == "forward")
                    {
                        if (ship_speed < 50)
                        {
                            ship_speed += 3f;
                        }
                        else
                        {
                            ship_speed -= 1f;
                        }
                        if (ship_turn_speed > 10)
                        {
                            ship_turn_speed -= 1f;
                        }
                    }
                    if (target_orient == "side")
                    {
                        if (ship_speed > 10)
                        {
                            ship_speed -= 1f;
                        }
                        else
                        {
                            ship_speed += 1f;
                        }
                        if (ship_turn_speed < 50)
                        {
                            ship_turn_speed += 3f;
                        }
                    }
                    break;
                case "middlerange":
                    if (target_orient == "back")
                    {
                        if (ship_speed > 10)
                        {
                            ship_speed -= 3f;
                        }
                        else
                        {
                            ship_speed += 1f;
                        }
                        if (ship_turn_speed < 50)
                        {
                            ship_turn_speed += 2f;
                        }
                    }
                    if (target_orient == "forward")
                    {
                        ship_break_distance = ship_speed * ship_speed / (10 * 2);
                        if (target_distance < orbit_range * 1.03f)
                        {
                            if (ship_speed > 0)
                            {
                                ship_speed -= 0.5f;
                            }
                            else
                            {
                                ship_speed += 0.5f;
                            }
                            if (ship_turn_speed < 50)
                            {
                                ship_turn_speed += 2f;
                            }
                            else
                            {
                                ship_turn_speed -= 1f;
                            }
                        }
                        else if (target_distance < ship_break_distance + orbit_range * 1.1f || target_distance < orbit_range * 1.1f)
                        {
                            if (ship_speed > 5)
                            {
                                ship_speed -= 2f;
                            }
                            else
                            {
                                ship_speed += 1f;
                            }
                            if (ship_turn_speed < 50)
                            {
                                ship_turn_speed += 2f;
                            }
                            else
                            {
                                ship_turn_speed -= 1f;
                            }
                        }
                        else if (target_distance < 3 * orbit_range )
                        {
                            if (ship_speed > 30)
                            {
                                ship_speed -= 2f;
                            }
                            else
                            {
                                ship_speed += 1f;
                            }
                            if (ship_turn_speed < 30)
                            {
                                ship_turn_speed += 2f;
                            }
                            else
                            {
                                ship_turn_speed -= 1f;
                            }
                        }
                        else if (target_distance > 3 * orbit_range)
                        {
                            if (ship_speed < 50)
                            {
                                ship_speed += 2f;
                            }
                            else
                            {
                                ship_speed -= 1f;
                            }
                        }
                        if (ship_turn_speed > 10)
                        {
                            ship_turn_speed -= 1f;
                        }
                    }
                    if (target_orient == "side")
                    {
                        if (ship_speed > 10)
                        {
                            ship_speed -= 3f;
                        }
                        else
                        {
                            ship_speed += 1f;
                        }
                        if (ship_turn_speed < 50)
                        {
                            ship_turn_speed += 2f;
                        }
                    }
                    break;
                case "shortrange":
                    if (target_orient == "back")
                    {
                        if (ship_speed > 5)
                        {
                            ship_speed -= 2f;
                        }
                        else
                        {
                            ship_speed += 1f;
                        }
                        if (ship_turn_speed < 50)
                        {
                            ship_turn_speed += 2f;
                        }
                    }
                    if (target_orient == "forward")
                    {
                        if (ship_speed > -5)
                        {
                            ship_speed -= 1f;
                        }
                        else
                        {
                            ship_speed += 1f;
                        }
                        if (ship_turn_speed < 50)
                        {
                            ship_turn_speed += 2f;
                        }
                    }
                    if (target_orient == "side")
                    {
                        if (ship_speed > 0)
                        {
                            ship_speed -= 2f;
                        }
                        else
                        {
                            ship_speed += 1f;
                        }
                        if (ship_turn_speed < 50)
                        {
                            ship_turn_speed += 2f;
                        }
                    }
                    break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator Ship_forward_attack()
    {
        Debug.Log("Forward attack");
        while (true)
        {
            switch (target_range)
            {
                case "longrange":
                    if (target_orient == "back")
                    {
                        if (ship_speed > 10)
                        {
                            ship_speed -= 3f;
                        }
                        else
                        {
                            ship_speed += 1f;
                        }
                        if (ship_turn_speed < 50)
                        {
                            ship_turn_speed += 2f;
                        }
                    }
                    if (target_orient == "forward")
                    {
                        if (ship_speed < 50)
                        {
                            ship_speed += 3f;
                        }
                        else
                        {
                            ship_speed -= 1f;
                        }
                        if (ship_turn_speed > 10)
                        {
                            ship_turn_speed -= 1f;
                        }
                    }
                    if (target_orient == "side")
                    {
                        if (ship_speed > 10)
                        {
                            ship_speed -= 1f;
                        }
                        else
                        {
                            ship_speed += 1f;
                        }
                        if (ship_turn_speed < 50)
                        {
                            ship_turn_speed += 3f;
                        }
                    }
                    break;
                case "middlerange":
                    if (target_orient == "back")
                    {
                        if (ship_speed < 50)
                        {
                            ship_speed += 3f;
                        }
                        else
                        {
                            ship_speed -= 1f;
                        }
                        if (ship_turn_speed > 10)
                        {
                            ship_turn_speed -= 1f;
                        }
                    }
                    if (target_orient == "forward")
                    {
                        if (target_distance > 3 * orbit_range)
                        {
                            if (ship_speed < 50)
                            {
                                ship_speed += 2f;
                            }
                            else
                            {
                                ship_speed -= 1f;
                            }
                        }
                        else
                        {
                            if (ship_speed > 30)
                            {
                                ship_speed -= 2f;
                            }
                            else
                            {
                                ship_speed += 2f;
                            }
                        }
                        if (ship_turn_speed > 5)
                        {
                            ship_turn_speed -= 1f;
                        }
                    }
                    if (target_orient == "side")
                    {
                        if (ship_speed < 50)
                        {
                            ship_speed += 2f;
                        }
                        else
                        {
                            ship_speed -= 1f;
                        }
                        if (ship_turn_speed > 10)
                        {
                            ship_turn_speed -= 1f;
                        }
                    }
                    break;
                case "shortrange":
                    if (target_orient == "back")
                    {
                        if (ship_speed < 30)
                        {
                            ship_speed += 2f;
                        }
                        else
                        {
                            ship_speed -= 1f;
                        }
                        if (ship_turn_speed > 10)
                        {
                            ship_turn_speed -= 2f;
                        }
                    }
                    if (target_orient == "forward")
                    {
                        if (ship_speed > 20)
                        {
                            ship_speed -= 2f;
                        }
                        else
                        {
                            ship_speed += 1f;
                        }
                        if (ship_turn_speed < 20)
                        {
                            ship_turn_speed += 5f;
                        }
                    }
                    if (target_orient == "side")
                    {
                        if (ship_speed < 30)
                        {
                            ship_speed += 2f;
                        }
                        else
                        {
                            ship_speed -= 1f;
                        }
                        if (ship_turn_speed < 20)
                        {
                            ship_turn_speed += 5f;
                        }
                    }
                    break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator Ship_side_orbit()
    {
        Debug.Log("Side orbit");
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            switch (target_range)
            {
                case "longrange":
                    ship_side_speed = 0;
                    if (target_orient == "back")
                    {
                        if (ship_speed > 10)
                        {
                            ship_speed -= 2f;
                        }
                        if (ship_turn_speed < 50)
                        {
                            ship_turn_speed += 1f;
                        }
                    }
                    if (target_orient == "forward")
                    {
                        if (ship_speed < 50)
                        {
                            ship_speed += 3f;
                        }
                        if (ship_turn_speed > 5)
                        {
                            ship_turn_speed -= 1f;
                        }
                    }
                    if (target_orient == "side")
                    {
                        if (ship_speed > 10)
                        {
                            ship_speed -= 1f;
                        }
                        if (ship_turn_speed < 50)
                        {
                            ship_turn_speed += 2f;
                        }
                    }
                    break;
                case "shortrange":
                    if (target_orient == "back")
                    {
                        ship_side_speed = 0;
                        if (ship_speed > 3)
                        {
                            ship_speed -= 2f;
                        }
                        if (ship_turn_speed < 50)
                        {
                            ship_turn_speed += 2f;
                        }
                    }
                    if (target_orient == "forward")
                    {
                        if (target_distance > 0.9 * orbit_range)
                        {
                            if (ship_speed < -3)
                            {
                                ship_speed += 1f;
                            }
                            else if (ship_speed < -1)
                            {
                                ship_speed += 0.5f;
                            }
                        }
                        else
                        {
                            if (ship_speed > -5)
                            {
                                ship_speed -= 1f;
                            }
                        }
                        if (ship_turn_speed > 20)
                        {
                            ship_turn_speed -= 2f;
                        }
                        else
                        {
                            ship_turn_speed += 1f;
                        }
                        if (target_distance < orbit_range * 0.8)
                        {
                            ship_side_speed = 0;
                        }
                        if ((ship_side_speed < ship_max_side_speed) && (target_distance > orbit_range * 0.8))
                        {
                            ship_side_speed += 0.05f * ship_max_side_speed;
                        }
                    }
                    if (target_orient == "side")
                    {
                        if (ship_speed > 0)
                        {
                            ship_speed -= 1f;
                        }
                        if (ship_turn_speed < 50)
                        {
                            ship_turn_speed += 2f;
                        }
                        if ((ship_side_speed < ship_max_side_speed) && (target_distance > orbit_range * 0.8))
                        {
                            ship_side_speed += 0.02f * ship_max_side_speed;
                        }
                    }
                    break;
                case "middlerange":
                    //10 это коэффициент при котором время равно 1 сек
                    ship_break_distance = ship_speed * ship_speed / (10 * 2);
                    if (target_orient == "back")
                    {
                        //сделать потом плавное боковое замедление
                        ship_side_speed = 0;
                        if (ship_speed > 10)
                        {
                            ship_speed -= 2f;
                        }
                        else
                        {
                            ship_speed += 2f;
                        }
                        if (ship_turn_speed < 50)
                        {
                            ship_turn_speed += 2f;
                        }
                    }
                    if (target_orient == "forward")
                    {
                        if (target_distance < orbit_range * 1.03f)
                        {
                            if (ship_speed > 0)
                            {
                                ship_speed -= 0.5f;
                            }
                            else if (ship_speed < 0)
                            {
                                ship_speed += 0.5f;
                            }
                            if (ship_turn_speed < 20)
                            {
                                ship_turn_speed += 1f;
                            }
                            if (ship_side_speed < ship_max_side_speed)
                            {
                                ship_side_speed += 0.05f * ship_max_side_speed;
                            }
                        }
                        else if ((target_distance < ship_break_distance + orbit_range * 1.03f) || (target_distance < orbit_range * 1.7))
                        {
                            if (target_distance < orbit_range * 1.03f)
                            {
                                if (ship_speed > 5)
                                {
                                    ship_speed -= 2f;
                                }
                                if (ship_speed > 2)
                                {
                                    ship_speed -= 1f;
                                }
                                else if (ship_speed > 0.5f)
                                {
                                    ship_speed -= 0.5f;
                                }
                                else
                                {
                                    ship_speed += 0.5f;
                                }
                            }
                            else
                            {
                                if (ship_speed > 7)
                                {
                                    ship_speed -= 2f;
                                }
                                else if (ship_speed > 3)
                                {
                                    ship_speed -= 0.5f;
                                }
                                else
                                {
                                    ship_speed += 0.5f;
                                }
                            }
                            if (ship_turn_speed < 20)
                            {
                                ship_turn_speed += 2f;
                            }
                            if (ship_side_speed < ship_max_side_speed)
                            {
                                if (target_distance < orbit_range * 1.7)
                                {
                                    ship_side_speed += 0.05f * ship_max_side_speed;
                                }
                                else
                                {
                                    ship_side_speed += 0.02f * ship_max_side_speed;
                                }
                            }
                        }
                        else
                        {
                            ship_side_speed = 0;
                            if (target_distance > ship_break_distance + orbit_range * 1.7)
                            {
                                if (ship_speed < 50)
                                {
                                    ship_speed += 2f;
                                }
                            }
                            if (target_distance < orbit_range * 1.7)
                            {
                                if (ship_speed < 20)
                                {
                                    ship_speed += 0.5f;
                                }
                            }
                            else
                            {
                                if (ship_speed < 30)
                                {
                                    ship_speed += 1f;
                                }
                            }
                            if (ship_turn_speed > 20)
                            {
                                ship_turn_speed -= 1f;
                            }
                        }
                    }
                    if (target_orient == "side")
                    {
                        //сделать потом плавное боковое замедление
                        ship_side_speed = 0;
                        if (ship_speed > 10)
                        {
                            ship_speed -= 2f;
                        }
                        else
                        {
                            ship_speed += 2f;
                        }
                        if (ship_turn_speed < 50)
                        {
                            ship_turn_speed += 2f;
                        }
                    }
                    break;
            }
        }
    }
    IEnumerator Ship_orbit()
    {
        Debug.Log("Orbit");
        float orb = ship_max_speed / (ship_max_turn_speed * Mathf.PI / 180);
        ship_calc_speed = ship_max_speed;
        ship_calc_turn_speed = 0.85f * ship_max_turn_speed;
        while (orb > orbit_range)
        {
            for (int i = Mathf.RoundToInt(ship_max_speed); i > 1; i--)
            {
                for (int ii = 1; ii < Mathf.RoundToInt(0.85f * ship_max_turn_speed); ii++)
                {
                    float temp;
                    temp = ii * Mathf.PI / 180;
                    orb = i / temp;

                    if (orb < orbit_range)
                    {
                        ship_calc_speed = i;
                        ship_calc_turn_speed = ii;
                        break;
                    }
                }
                if (orb < orbit_range)
                {
                    break;
                }
            }
            orb = ship_calc_speed / (ship_calc_turn_speed * Mathf.PI / 180);
        }
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            switch (target_range)
            {
                case "longrange":
                    if (target_orient == "back")
                    {
                        if (ship_speed > 10)
                        {
                            ship_speed -= 2f;
                        }
                        if (ship_turn_speed < 30)
                        {
                            ship_turn_speed += 1f;
                        }
                    }
                    if (target_orient == "forward")
                    {
                        if (ship_speed < 50)
                        {
                            ship_speed += 2f;
                        }
                        if (ship_turn_speed > 5)
                        {
                            ship_turn_speed -= 1f;
                        }
                    }
                    if (target_orient == "side")
                    {
                        if (ship_speed > 10)
                        {
                            ship_speed -= 1f;
                        }
                        if (ship_turn_speed < 30)
                        {
                            ship_turn_speed += 2f;
                        }
                    }
                    break;
                case "middlerange":
                    ship_break_distance = ship_speed * ship_speed / (10 * 2);

                    if (target_orient == "back")
                    {
                        if (ship_speed > 10)
                        {
                            ship_speed -= 2f;
                        }
                        if (ship_turn_speed < 30)
                        {
                            ship_turn_speed += 2f;
                        }
                    }
                    if (target_orient == "forward")
                    {
                        if (target_distance < ship_break_distance + orbit_range * 1.2)
                        {
                            if (ship_speed > ship_calc_speed)
                            {
                                ship_speed -= 2f;
                            }
                            else
                            {
                                ship_speed += 1f;
                            }
                            if (ship_turn_speed < ship_calc_turn_speed)
                            {
                                ship_turn_speed += 2f;
                            }
                            else
                            {
                                ship_turn_speed -= 1f;
                            }
                        }
                        else if (target_distance > 5 * orbit_range)
                        {
                            if (ship_speed < 50)
                            {
                                ship_speed += 2f;
                            }
                            else
                            {
                                ship_speed -= 1f;
                            }
                            if (ship_turn_speed > 10)
                            {
                                ship_turn_speed -= 2f;
                            }
                        }
                        else if (target_distance > 2 * orbit_range)
                        {
                            if (ship_speed > 35)
                            {
                                ship_speed -= 1f;
                            }
                            else
                            {
                                ship_speed += 1f;
                            }
                            if (ship_turn_speed < ship_calc_turn_speed)
                            {
                                ship_turn_speed += 1f;
                            }
                            else
                            {
                                ship_turn_speed -= 1f;
                            }
                        }
                        else
                        {
                            if (ship_speed > ship_calc_speed)
                            {
                                ship_speed -= 1f;
                            }
                            else
                            {
                                ship_speed += 1f;
                            }
                        }
                        if (ship_turn_speed < ship_calc_turn_speed)
                        {
                            ship_turn_speed += 1f;
                        }
                        else
                        {
                            ship_turn_speed -= 1f;
                        }
                    }
                    if (target_orient == "side")
                    {
                        if (target_distance > 5 * orbit_range)
                        {
                            if (ship_speed < 50)
                            {
                                ship_speed += 2f;
                            }
                            else
                            {
                                ship_speed -= 1f;
                            }
                            if (ship_turn_speed > 10)
                            {
                                ship_turn_speed -= 2f;
                            }
                        }
                        else if (target_distance > 2 * orbit_range)
                        {
                            //if (ship_speed > ship_calc_speed * 2)
                            if (ship_speed > 35)
                            {
                                ship_speed -= 2f;
                            }
                            else
                            {
                                ship_speed += 1f;
                            }
                            if (ship_turn_speed < ship_calc_turn_speed)
                            {
                                ship_turn_speed += 1f;
                            }
                            else
                            {
                                ship_turn_speed -= 1f;
                            }
                        }
                        else if (target_distance > 1.2f * orbit_range)
                        {
                            if (ship_speed > ship_calc_speed)
                            {
                                ship_speed -= 1f;
                            }
                            else
                            {
                                ship_speed += 1f;
                            }
                            if (ship_turn_speed < ship_calc_turn_speed)
                            {
                                ship_turn_speed += 1f;
                            }
                            else
                            {
                                ship_turn_speed -= 1f;
                            }
                        }
                        else
                        {
                            if (ship_speed < ship_calc_speed)
                            {
                                ship_speed += 0.5f;
                            }
                            else
                            {
                                ship_speed -= 0.5f;
                            }
                            if (ship_turn_speed < ship_calc_turn_speed)
                            {
                                ship_turn_speed += 1f;
                            }
                            else
                            {
                                ship_turn_speed -= 1f;
                            }
                        }
                    }
                    break;
                case "shortrange":
                    //выход на орбиту изнутри по спирали
                    if (ship_turn_speed < ship_calc_turn_speed)
                    {
                        ship_turn_speed += 2f;
                    }
                    else if (ship_turn_speed > ship_calc_turn_speed)
                    {
                        ship_turn_speed -= 1f;
                    }
                    if (target_orient == "forward" || target_orient == "back")
                    {
                        if (ship_speed > 5)
                        {
                            ship_speed -= 2f;
                        }
                        else if (ship_speed < 0)
                        {
                            ship_speed += 1f;
                        }
                    }
                    if (target_orient == "side")
                    {
                        //Если стоим боком к цели, то раскручиваемся по спирали
                        if (target_angle > 70 && target_angle < 110)
                        {
                            if (target_distance < orbit_range * 0.5)
                            {
                                if (ship_speed < ship_calc_speed * ((target_distance / orbit_range) + 0.01))
                                {
                                    ship_speed += 0.5f;
                                }
                                else
                                {
                                    ship_speed -= 0.5f;
                                }
                            }
                            else if (target_distance < orbit_range * 0.8)
                            {
                                if (ship_speed < ship_calc_speed * ((target_distance / orbit_range) + 0.02))
                                {
                                    ship_speed += 0.5f;
                                }
                                else
                                {
                                    ship_speed -= 0.5f;
                                }
                            }
                            else
                            {
                                if (ship_speed < ship_calc_speed * (target_distance / orbit_range))
                                {
                                    ship_speed += 0.5f;
                                }
                                else
                                {
                                    ship_speed -= 0.5f;
                                }
                            }
                        }
                        // если не стоим боком, то полное замедление и поворот боком
                        else
                        {
                            if (ship_speed > 7)
                            {
                                ship_speed -= 1f;
                            }
                            else if (ship_speed > 3)
                            {
                                ship_speed -= 0.5f;
                            }
                            else
                            {
                                ship_speed += 0.5f;
                            }
                        }
                    }
                    break;
            }
        }
    }
    IEnumerator Ace_attack()
    {
        Debug.Log("Ace attack");
        float orb = ship_max_speed / (ship_max_turn_speed * Mathf.PI / 180);
        ship_calc_speed = ship_max_speed;
        ship_calc_turn_speed = 0.75f * ship_max_turn_speed;
        while (orb > orbit_range)
        {
            for (int i = Mathf.RoundToInt(ship_max_speed); i > 1; i--)
            {
                for (int ii = 1; ii < Mathf.RoundToInt(0.75f * ship_max_turn_speed); ii++)
                {
                    float temp;
                    temp = ii * Mathf.PI / 180;
                    orb = i / temp;

                    if (orb < orbit_range)
                    {
                        ship_calc_speed = i;
                        ship_calc_turn_speed = ii;
                        break;
                    }
                }
                if (orb < orbit_range)
                {
                    break;
                }
            }
            orb = ship_calc_speed / (ship_calc_turn_speed * Mathf.PI / 180);
        }
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            switch (target_range)
            {
                case "longrange":
                    if (target_orient == "back")
                    {
                        if (ship_speed > 10)
                        {
                            ship_speed -= 2f;
                        }
                        if (ship_turn_speed < 30)
                        {
                            ship_turn_speed += 1f;
                        }
                    }
                    if (target_orient == "forward")
                    {
                        if (ship_speed < 50)
                        {
                            ship_speed += 2f;
                        }
                        if (ship_turn_speed > 5)
                        {
                            ship_turn_speed -= 1f;
                        }
                    }
                    if (target_orient == "side")
                    {
                        if (ship_speed > 10)
                        {
                            ship_speed -= 1f;
                        }
                        if (ship_turn_speed < 30)
                        {
                            ship_turn_speed += 2f;
                        }
                    }
                    break;
                case "middlerange":
                    if (target_orient == "back")
                    {
                        if (ship_speed > 10)
                        {
                            ship_speed -= 2f;
                        }
                        if (ship_turn_speed < 30)
                        {
                            ship_turn_speed += 1f;
                        }
                    }
                    if (target_orient == "forward")
                    {
                        if (ship_speed < 40)
                        {
                            ship_speed += 2f;
                        }
                        else
                        {
                            ship_speed -= 1f;
                        }
                        if (ship_turn_speed < 30)
                        {
                            ship_turn_speed += 1f;
                        }
                    }
                    if (target_orient == "side")
                    {
                        if ((target.transform.position - this.transform.position).magnitude > 5 * orbit_range)
                        {
                            if (ship_speed < 40)
                            {
                                ship_speed += 2f;
                            }
                            else
                            {
                                ship_speed -= 1f;
                            }
                            if (ship_turn_speed < 30)
                            {
                                ship_turn_speed += 2f;
                            }
                        }
                        else if ((target.transform.position - this.transform.position).magnitude > 1.05f * orbit_range)
                        {
                            if (ship_speed > 10)
                            {
                                ship_speed -= 1f;
                            }
                            if (ship_turn_speed < ship_max_turn_speed)
                            {
                                ship_turn_speed += 2f;
                            }
                        }
                        else
                        {
                            if (ship_speed < ship_calc_speed)
                            {
                                ship_speed += 1f;
                            }
                            else if (ship_speed > ship_calc_speed)
                            {
                                ship_speed -= 1f;
                            }
                            if (ship_turn_speed < ship_calc_turn_speed)
                            {
                                ship_turn_speed += 1f;
                            }
                            else if (ship_turn_speed > ship_calc_turn_speed)
                            {
                                ship_turn_speed -= 1f;
                            }
                        }
                    }
                    break;
                case "shortrange":
                    //выход на орбиту изнутри по спирали                    
                    if (ship_speed < ship_calc_speed)
                    {
                        ship_speed += 1f;
                    }
                    else if (ship_speed > ship_calc_speed)
                    {
                        ship_speed -= 2f;
                    }
                    if (ship_turn_speed < ship_calc_turn_speed)
                    {
                        ship_turn_speed += 2f;
                    }
                    else if (ship_turn_speed > ship_calc_turn_speed)
                    {
                        ship_turn_speed -= 1f;
                    }
                    break;
            }
        }
    }
    IEnumerator Test()
    {
        Debug.Log("Test");
        yield return new WaitForSeconds(1f);
    }
    IEnumerator Ship_standby()
    {
        Debug.Log("Standby");
        yield return new WaitForSeconds(1f);
    }
    void Update()
    {
        target_distance = (target.transform.position - this.transform.position).magnitude;
        orbit_radius = ship_speed / (ship_turn_speed * Mathf.PI / 180);
        Where_target();
        //Target_speed();

        //Подлететь и атаковать
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StopAllCoroutines();
            moving_type = "front_attack";
            StartCoroutine(Ship_front_attack());
        }
        //Прямая атака с уходом
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StopAllCoroutines();
            moving_type = "forward_attack";
            StartCoroutine(Ship_forward_attack());
        }
        //Боковая орбита
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StopAllCoroutines();
            moving_type = "side_orbit";
            StartCoroutine(Ship_side_orbit());
        }
        //Круговая орбита
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StopAllCoroutines();
            moving_type = "orbit";
            StartCoroutine(Ship_orbit());
        }
        //Маневровая атака аса
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            StopAllCoroutines();
            moving_type = "ace_attack";
            StartCoroutine(Ace_attack());
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            StopAllCoroutines();
            moving_type = "test";
            StartCoroutine(Test());
        }
        //Состояние standby
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopAllCoroutines();
            moving_type = "standby";
            StartCoroutine(Ship_standby());
        }

        //ship_real_speed = ship_speed + target_speed;
        //if (ship_real_speed > ship_max_speed)
        //{
        //    ship_real_speed = ship_max_speed;
        //}
        //else if (ship_real_speed < ship_max_back_speed)
        //{
        //    ship_real_speed = ship_max_back_speed;
        //}
        if (moving_type == "front_attack")
        {
            q_ship = Quaternion.LookRotation(this.transform.position - target.transform.position);
            q_ship *= Quaternion.Euler(0, 180, 0);
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, q_ship, Time.deltaTime * ship_turn_speed);
            this.transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);
        }
        else if (moving_type == "forward_attack")
        {
            if (target_range != "shortrange")
            {
                q_ship = Quaternion.LookRotation(this.transform.position - target.transform.position);
                q_ship *= Quaternion.Euler(0, 180, 0);
                this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, q_ship, Time.deltaTime * ship_turn_speed);
                this.transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);
            }
            else
            {
                if (!isBusy)
                {
                    StartCoroutine(Ship_turn_side());
                }
                this.transform.Rotate(new Vector3(0, ship_turn_side * ship_turn_speed, 0) * Time.deltaTime);
                this.transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);
            }
        }
        else if (moving_type == "side_orbit")
        {
            // 
            if ((target_distance > 1.7 * orbit_range) || (target_distance < 0.8f * orbit_range))
            {
                q_ship = Quaternion.LookRotation(this.transform.position - target.transform.position);
                q_ship *= Quaternion.Euler(0, 180, 0);
                this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, q_ship, Time.deltaTime * ship_turn_speed);
                this.transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);
            }
            else
            {
                q_ship = Quaternion.LookRotation(this.transform.position - target.transform.position);
                q_ship *= Quaternion.Euler(0, 180, 0);
                this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, q_ship, Time.deltaTime * ship_turn_speed);
                this.transform.Translate(Vector3.left * ship_side_speed * Time.deltaTime);
                this.transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);
            }
        }
        else if (moving_type == "orbit")
        {
            if (target_distance > 5 * orbit_range)
            {
                q_ship = Quaternion.LookRotation(this.transform.position - target.transform.position);
                q_ship *= Quaternion.Euler(0, 180, 0);
                this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, q_ship, Time.deltaTime * ship_turn_speed);
                this.transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);
            }
            else if (target_range == "shortrange" && target_distance < 0.975 * orbit_range)
            {
                q_ship = Quaternion.LookRotation(this.transform.position - target.transform.position);
                q_ship *= Quaternion.Euler(0, 90, 0);
                this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, q_ship, Time.deltaTime * ship_turn_speed);
                this.transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);
            }
            else
            {
                if (target_distance > 1.025f * orbit_range)
                {
                    //вместо цели, направление на касательную
                    Vector3 orbit_point = new Vector3(0f, 0f, 0f);
                    float a = orbit_range;
                    float b;
                    float c = (target.transform.position - this.transform.position).magnitude;
                    b = Mathf.Sqrt(c * c - a * a);
                    orbit_point.y = 0;

                    if (target.transform.position.x >= this.transform.position.x)
                    {
                        fi = Mathf.PI / 2 - Mathf.Asin(b / c) - Mathf.Asin((this.transform.position.z - target.transform.position.z) / c);
                        orbit_point.x = target.transform.position.x - a * Mathf.Sin(fi);
                        orbit_point.z = target.transform.position.z + a * Mathf.Cos(fi);
                    }
                    else
                    {
                        fi = Mathf.PI / 2 - Mathf.Asin(b / c) - Mathf.Asin((-this.transform.position.z + target.transform.position.z) / c);
                        orbit_point.x = target.transform.position.x + a * Mathf.Sin(fi);
                        orbit_point.z = target.transform.position.z - a * Mathf.Cos(fi);
                    }
                    //orbitPoint.transform.position = orbit_point;

                    q_ship = Quaternion.LookRotation(this.transform.position - orbit_point);
                    q_ship *= Quaternion.Euler(0, 180, 0);
                    this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, q_ship, Time.deltaTime * ship_turn_speed);
                    this.transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);
                }
                else
                {
                    q_ship = Quaternion.LookRotation(this.transform.position - target.transform.position);
                    q_ship *= Quaternion.Euler(0, 91f, 0);
                    this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, q_ship, Time.deltaTime * ship_turn_speed);
                    this.transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);
                }
            }
        }
        else if (moving_type == "ace_attack")
        {
            if ((target_range == "middlerange" && target_distance > 5 * orbit_range) || (target_range == "longrange"))
            {
                q_ship = Quaternion.LookRotation(this.transform.position - target.transform.position);
                q_ship *= Quaternion.Euler(0, 180, 0);
                this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, q_ship, Time.deltaTime * ship_turn_speed);
                this.transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);
            }
            else if (target_range == "shortrange" && target_distance < 0.5 * orbit_range)
            {
                this.transform.Rotate(new Vector3(0, -ship_turn_speed, 0) * Time.deltaTime);
                this.transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);
            }
            else if (target_range == "shortrange" && target_distance < 0.9 * orbit_range)
            {
                q_ship = Quaternion.LookRotation(this.transform.position - target.transform.position);
                q_ship *= Quaternion.Euler(0, 87, 0);
                this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, q_ship, Time.deltaTime * ship_turn_speed);
                this.transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);
            }
            else
            {
                if (target_distance > 1.1f * orbit_range)
                {
                    //вместо цели, направление на касательную
                    Vector3 orbit_point = new Vector3(0f, 0f, 0f);
                    float a = orbit_range;
                    float b;
                    float c = (target.transform.position - this.transform.position).magnitude;
                    b = Mathf.Sqrt(c * c - a * a);

                    orbit_point.x = target.transform.position.x - a * a / c;
                    orbit_point.y = 0;
                    orbit_point.z = target.transform.position.z + a * b / c;
                    orbitPoint.transform.position = orbit_point;

                    q_ship = Quaternion.LookRotation(this.transform.position - orbit_point);
                    q_ship *= Quaternion.Euler(0, 180, 0);
                    this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, q_ship, Time.deltaTime * ship_turn_speed);
                    this.transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);
                }
                else
                {
                    q_ship = Quaternion.LookRotation(this.transform.position - target.transform.position);
                    q_ship *= Quaternion.Euler(0, 91.5f, 0);
                    this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, q_ship, Time.deltaTime * ship_turn_speed);
                    this.transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);
                }
            }
        }
        else if (moving_type == "test")
        {
            q_ship = Quaternion.LookRotation(this.transform.position - target.transform.position);
            q_ship *= Quaternion.Euler(0, 90, 0);
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, q_ship, Time.deltaTime * ship_turn_speed);
            this.transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);
        }
        else
        {
            moving_type = "standby";
        }
    }

    void Target_speed()
    {
        if (oddFrame)
        {
            distance1 = target_distance;
            oddFrame = false;
        }
        else
        {
            distance2 = target_distance;
            oddFrame = true;
            if (distance1 != distance2)
            {
                target_speed = (distance2 - distance1) / Time.deltaTime;
            }
        }
    }
    void Where_target()
    {
        vector3_check = target.transform.position - this.transform.position;
        target_angle = Vector3.Angle(vector3_check, this.transform.forward);
        if (target_angle < 30)
        {
            target_orient = "forward";
        }
        else if (target_angle >= 150)
        {
            target_orient = "back";
        }
        else
        {
            target_orient = "side";
        }
        if (Vector3.Angle(vector3_check, this.transform.right) > 90f)
        {
            target_side = "left";
        }
        else
        {
            target_side = "right";
        }

        if (target_distance <= short_range)
        {
            target_range = "shortrange";
        }
        else if (target_distance < long_range)
        {
            target_range = "middlerange";
        }
        else if (target_distance >= long_range)
        {
            target_range = "longrange";
        }
        //if (target_distance <= shoot_range)
        //{
        //    InWeaponRange = true;
        //}
        //else
        //{
        //    InWeaponRange = false;
        //}
    }
    IEnumerator Ship_turn_side()
    {
        isBusy = true;
        ship_turn_side = 1 - 2 * Random.Range(0, 2);
        yield return new WaitForSeconds(3f);
        isBusy = false;
    }
}
