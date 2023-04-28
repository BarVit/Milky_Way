using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject spaceBase;
    private GameObject[] enemies;
    private List<GameObject> targets = new List<GameObject>();
    private GameObject target;

    public AnimationCurve turn_curve;
    public AnimationCurve acceleration_curve;

    [Header("Ship movement stats")]
    public float ship_acceleration;
    public float ship_breaking;
    public float ship_speed;
    public float ship_side_speed;
    public float ship_turn_speed;

    public float ship_max_acceleration;
    public float ship_max_breaking;
    public float ship_max_speed;
    public float ship_max_side_speed;
    public float ship_max_turn_speed;
    public float ship_max_back_speed;

    [SerializeField] float ship_calc_speed;
    [SerializeField] float ship_calc_turn_speed;

    [SerializeField] int ship_turn_side;


    private Quaternion q_ship;
    private Vector3 radius_vector;

    [Header("Angles")]
    [SerializeField] private float target_angle;
    private enum Target_range
    {
        shortrange,
        middlerange,
        longrange
    }
    private enum Target_orient
    {
        forward,
        back,
        side
    }
    private Target_orient target_orient;
    private Target_range target_range;

    public enum Moving_type 
    {
        front_attack,
        forward_attack,
        ace_attack,
        orbit,
        side_orbit,
        standby,
        test,
        move_to_point
    }
    public Moving_type moving_type;
    [SerializeField] float target_distance;

    //заданная орбита
    public float orbit_range = 30f;

    private float long_range = 250f;
    private float short_range = 30f;
    private float ship_break_distance;

    private float fi;

    private bool isBusy = false;


    void Start()
    {
        ship_speed = 0;
        ship_acceleration = 1f;
        ship_breaking = 1f;
        ship_max_breaking = 2f;
        ship_max_acceleration = 2f;
        ship_max_speed = 50f;
        ship_max_back_speed = -10f;
        ship_max_turn_speed = 50f;
        ship_turn_speed = 30f;
        ship_side_speed = 0;
        ship_max_side_speed = 0.2f * ship_max_speed;
        moving_type = Moving_type.standby;

        Ship_start();
    }
    private void Ship_start()
    {
        Debug.Log("Start");
        moving_type = Moving_type.standby;
        //Горизонтальное покачивание в режиме покоя
    }

    IEnumerator Move_to_point()
    {
        Debug.Log("Move to point" + target.name);
        while (true)
        {
            if (target_orient == Target_orient.back)
                Change_speed(5, 3);
            if (target_orient == Target_orient.forward)
            {
                if (target_distance > ship_max_speed * ship_max_speed / 10 * ship_acceleration)
                    Change_speed(50, 3);
                else if (target_distance < Break_distance() + 15)
                    Change_speed(5, ship_acceleration);
                else if (target_distance < 5)
                    Change_speed(2);
                else if (target_distance < 1)
                    Change_speed(0);
                else
                    Change_speed(50);
            }
                
            if (target_orient == Target_orient.side)
                Change_speed(10);
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator Ship_front_attack()
    {
        Debug.Log("Front attack");
        while (true)
        {
            switch (target_range)
            {
                case Target_range.longrange:
                    if (target_orient == Target_orient.back)
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
                    if (target_orient == Target_orient.forward)
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
                    if (target_orient == Target_orient.side)
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
                case Target_range.middlerange:
                    if (target_orient == Target_orient.back)
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
                    if (target_orient == Target_orient.forward)
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
                        else if (target_distance < 3 * orbit_range)
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
                    if (target_orient == Target_orient.side)
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
                case Target_range.shortrange:
                    if (target_orient == Target_orient.back)
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
                    if (target_orient == Target_orient.forward)
                    {
                        if (ship_speed > 0.5 * ship_max_back_speed)
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
                    if (target_orient == Target_orient.side)
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
                case Target_range.longrange:
                    if (target_orient == Target_orient.back)
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
                    if (target_orient == Target_orient.forward)
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
                    if (target_orient == Target_orient.side)
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
                case Target_range.middlerange:
                    if (target_orient == Target_orient.back)
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
                    if (target_orient == Target_orient.forward)
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
                    if (target_orient == Target_orient.side)
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
                case Target_range.shortrange:
                    if (target_orient == Target_orient.back)
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
                    if (target_orient == Target_orient.forward)
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
                    if (target_orient == Target_orient.side)
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
                case Target_range.longrange:
                    ship_side_speed = 0;
                    if (target_orient == Target_orient.back)
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
                    if (target_orient == Target_orient.forward)
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
                    if (target_orient == Target_orient.side)
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
                case Target_range.shortrange:
                    if (target_orient == Target_orient.back)
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
                    if (target_orient == Target_orient.forward)
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
                            if (ship_speed > 0.5f * ship_max_back_speed)
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
                    if (target_orient == Target_orient.side)
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
                case Target_range.middlerange:
                    //10 это коэффициент при котором время равно 1 сек
                    ship_break_distance = ship_speed * ship_speed / (10 * 2);
                    if (target_orient == Target_orient.back)
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
                    if (target_orient == Target_orient.forward)
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
                    if (target_orient == Target_orient.side)
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
                case Target_range.longrange:
                    if (target_orient == Target_orient.back)
                    {
                        if (ship_speed > 10)
                            ship_speed -= 2f;
                        if (ship_turn_speed < 30)
                            ship_turn_speed += 1f;
                    }
                    if (target_orient == Target_orient.forward)
                    {
                        if (ship_speed < 50)
                            ship_speed += 2f;
                        if (ship_turn_speed > 5)
                            ship_turn_speed -= 1f;
                    }
                    if (target_orient == Target_orient.side)
                    {
                        if (ship_speed > 10)
                            ship_speed -= 1f;
                        if (ship_turn_speed < 30)
                            ship_turn_speed += 2f;
                    }
                    break;
                case Target_range.middlerange:
                    ship_break_distance = ship_speed * ship_speed / (10 * 2);

                    if (target_orient == Target_orient.back)
                    {
                        if (ship_speed > 10)
                            ship_speed -= 2f;
                        else
                            ship_speed += 1f;
                        if (ship_turn_speed < 30)
                            ship_turn_speed += 2f;
                        else
                            ship_turn_speed -= 1f;
                    }
                    if (target_orient == Target_orient.forward)
                    {
                        if (target_distance < ship_break_distance + orbit_range * 1.1f)
                        {
                            if (ship_speed > ship_calc_speed)
                                ship_speed -= 2f;
                            else
                                ship_speed += 1f;
                            if (ship_turn_speed < ship_max_turn_speed)
                                ship_turn_speed += 2f;
                            else
                                ship_turn_speed -= 1f;
                        }
                        else if (target_distance > 5 * orbit_range)
                        {
                            if (ship_speed < 50)
                                ship_speed += 2f;
                            else
                                ship_speed -= 1f;
                            if (ship_turn_speed > 10)
                                ship_turn_speed -= 2f;
                            else
                                ship_turn_speed += 1f;
                        }
                        else if (target_distance > 2 * orbit_range)
                        {
                            if (ship_speed > 35)
                                ship_speed -= 1f;
                            else
                                ship_speed += 1f;
                            if (ship_turn_speed < ship_calc_turn_speed)
                                ship_turn_speed += 1f;
                            else
                                ship_turn_speed -= 1f;
                        }
                        else
                        {
                            if (ship_speed > ship_calc_speed)
                                ship_speed -= 1f;
                            else
                                ship_speed += 1f;
                            if (ship_turn_speed < ship_calc_turn_speed)
                                ship_turn_speed += 1f;
                            else
                                ship_turn_speed -= 1f;
                        }
                    }
                    if (target_orient == Target_orient.side)
                    {
                        if (target_distance > 5 * orbit_range)
                        {
                            if (ship_speed < 50)
                                ship_speed += 1f;
                            else
                                ship_speed -= 1f;
                            if (ship_turn_speed > 20)
                                ship_turn_speed -= 2f;
                            else
                                ship_turn_speed += 1f;
                        }
                        else if (target_distance > 2 * orbit_range)
                        {
                            if (ship_speed > 35)
                                ship_speed -= 2f;
                            else
                                ship_speed += 1f;
                            if (ship_turn_speed < 30)
                                ship_turn_speed += 1f;
                            else
                                ship_turn_speed -= 1f;
                        }
                        else if (target_distance > 1.1f * orbit_range)
                        {
                            if (ship_speed > ship_calc_speed)
                                ship_speed -= 1f;
                            else
                                ship_speed += 1f;
                            if (ship_turn_speed < ship_calc_turn_speed)
                                ship_turn_speed += 1f;
                            else
                                ship_turn_speed -= 1f;
                        }
                        else
                        {
                            if (ship_speed < ship_calc_speed)
                                ship_speed += 0.5f;
                            else
                                ship_speed -= 0.5f;
                            if (ship_turn_speed < ship_max_turn_speed)
                                ship_turn_speed += 2f;
                            else
                                ship_turn_speed -= 1f;
                        }
                    }
                    break;
                case Target_range.shortrange:
                    //выход на орбиту изнутри по спирали

                    if (target_orient == Target_orient.forward || target_orient == Target_orient.back)
                    {
                        //если цель впереди или сзади, то замедляемся и поворачиваемся боком
                        if (ship_speed > 5)
                            ship_speed -= 2;
                        else
                            ship_speed += 1;
                        if (ship_turn_speed < ship_max_turn_speed)
                            ship_turn_speed += 2;
                        else
                            ship_turn_speed -= 1;
                    }
                    if (target_orient == Target_orient.side)
                    {
                        if (target_angle > 85 && target_angle < 100)
                        {
                            if (ship_speed < ship_calc_speed)
                                ship_speed += 1;
                            else
                                ship_speed -= 1;
                            if (ship_turn_speed < ship_max_turn_speed)
                                ship_turn_speed += 2;
                            else
                                ship_turn_speed -= 1;
                        }
                        //если не стоим боком, то замедление и поворот боком
                        else
                        {
                            if (ship_speed > 5)
                                ship_speed -= 2;
                            else
                                ship_speed += 1;
                            if (ship_turn_speed < ship_max_turn_speed)
                                ship_turn_speed += 2;
                            else
                                ship_turn_speed -= 1;
                        }
                    }
                    break;
            }
        }
    }
    IEnumerator Ace_attack()
    {
        Debug.Log("Ace attack");
        while (true)
        {
            switch (target_range)
            {
                case Target_range.longrange:
                    if (target_orient == Target_orient.back)
                    {
                        if (ship_speed > 10)
                        {
                            ship_speed -= 3f;
                        }
                        else
                        {
                            ship_speed += 1f;
                        }
                        if (ship_turn_speed < ship_max_turn_speed * 2)
                        {
                            ship_turn_speed += 2f;
                        }
                    }
                    if (target_orient == Target_orient.forward)
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
                    if (target_orient == Target_orient.side)
                    {
                        if (ship_speed > 10)
                        {
                            ship_speed -= 1f;
                        }
                        else
                        {
                            ship_speed += 1f;
                        }
                        if (ship_turn_speed < ship_max_turn_speed * 2)
                        {
                            ship_turn_speed += 3f;
                        }
                    }
                    break;
                case Target_range.middlerange:
                    if (target_orient == Target_orient.back)
                    {
                        if (ship_speed < 30)
                        {
                            ship_speed += 1f;
                        }
                        else
                        {
                            ship_speed -= 1f;
                        }
                        if (target_distance > orbit_range * 1.6f)
                        {
                            if (ship_turn_speed < ship_max_turn_speed * 2)
                            {
                                ship_turn_speed += 5f;
                            }
                        }
                        else
                        {
                            if (ship_turn_speed > 20)
                            {
                                ship_turn_speed -= 5f;
                            }
                        }
                    }
                    if (target_orient == Target_orient.forward)
                    {
                        if (target_distance > 2 * orbit_range)
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
                                ship_speed += 1f;
                            }
                        }
                        if (ship_turn_speed < ship_max_turn_speed)
                        {
                            ship_turn_speed += 2f;
                        }
                    }
                    if (target_orient == Target_orient.side)
                    {
                        if (ship_speed > 20)
                        {
                            ship_speed -= 2f;
                        }
                        else
                        {
                            ship_speed += 1f;
                        }
                        if (target_distance > orbit_range * 1.6f)
                        {
                            if (ship_turn_speed < ship_max_turn_speed * 2)
                            {
                                ship_turn_speed += 5f;
                            }
                        }
                        else
                        {
                            if (ship_turn_speed > 20)
                            {
                                ship_turn_speed -= 5f;
                            }
                        }
                    }
                    break;
                case Target_range.shortrange:
                    if (target_orient == Target_orient.back)
                    {
                        if (ship_speed < 30)
                        {
                            ship_speed += 2f;
                        }
                        else
                        {
                            ship_speed -= 1f;
                        }
                        if (ship_turn_speed > 20)
                        {
                            ship_turn_speed -= 5f;
                        }
                    }
                    if (target_orient == Target_orient.forward)
                    {
                        if (ship_speed > 30)
                        {
                            ship_speed -= 2f;
                        }
                        else
                        {
                            ship_speed += 1f;
                        }
                        if (ship_turn_speed < ship_max_turn_speed)
                        {
                            ship_turn_speed += 2f;
                        }
                        else
                        {
                            ship_turn_speed -= 2f;
                        }
                    }
                    if (target_orient == Target_orient.side)
                    {
                        if (ship_speed < 30)
                        {
                            ship_speed += 1f;
                        }
                        else
                        {
                            ship_speed -= 1f;
                        }
                        if (ship_turn_speed < ship_max_turn_speed)
                        {
                            ship_turn_speed += 2f;
                        }
                        else
                        {
                            ship_turn_speed -= 2f;
                        }
                    }
                    break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator Test()
    {
        Debug.Log("Test");
        while (true)
        {
            switch (target_range)
            {
                case Target_range.longrange:
                    if (target_orient == Target_orient.back || target_orient == Target_orient.side)
                        Change_speed(10f);
                    if (target_orient == Target_orient.forward)
                        Change_speed(50f);
                    break;
                case Target_range.middlerange:
                    if (target_orient == Target_orient.back)
                        Change_speed(10f);
                    if (target_orient == Target_orient.forward)
                    {
                        //ship_break_distance = ship_speed * ship_speed / (10 * 2);
                        ship_break_distance = Break_distance();
                        if (target_distance < orbit_range * 1.03f)
                            Change_speed(0f, 0.5f);
                        else if (target_distance < ship_break_distance + orbit_range * 1.1f || target_distance < orbit_range * 1.1f)
                            Change_speed(5f);
                        else if (target_distance < 3 * orbit_range)
                            Change_speed(30f);
                        else if (target_distance > 3 * orbit_range)
                            Change_speed(50f);
                    }
                    if (target_orient == Target_orient.side)
                        Change_speed(10f);
                    break;
                case Target_range.shortrange:
                    if (target_orient == Target_orient.back)
                        Change_speed(5f);
                    if (target_orient == Target_orient.forward)
                        Change_speed(0.5f * ship_max_back_speed);
                    if (target_orient == Target_orient.side)
                        Change_speed(0f);
                    break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator Ship_standby()
    {
        Debug.Log("Standby");
        yield return new WaitForSeconds(1f);
    }
    private void FixedUpdate()
    {
        //Debug.DrawRay(this.transform.position, this.transform.forward * 200);
        //Debug.DrawRay(this.transform.position, (target.transform.position - this.transform.position) * 50);

        ship_turn_speed = ship_max_turn_speed * turn_curve.Evaluate(ship_speed / ship_max_speed);
        ship_acceleration = acceleration_curve.Evaluate(ship_speed / ship_max_speed);
        ship_breaking = acceleration_curve.Evaluate((ship_max_speed - ship_speed) / ship_max_speed);
    }

    public void MovingType(char movingType)
    {
        StopAllCoroutines();
        switch (movingType)
        {
            case '1':
                moving_type = Moving_type.front_attack;
                StartCoroutine(Ship_front_attack());
                break;
            case '2':
                moving_type = Moving_type.forward_attack;
                StartCoroutine(Ship_forward_attack());
                break;
            case '3':
                moving_type = Moving_type.side_orbit;
                StartCoroutine(Ship_side_orbit());
                break;
            case '4':
                moving_type = Moving_type.orbit;
                StartCoroutine(Ship_orbit());
                break;
            case '5':
                moving_type = Moving_type.ace_attack;
                StartCoroutine(Ace_attack());
                break;
            case '0':
                moving_type = Moving_type.test;
                StartCoroutine(Test());
                break;
            case 's':
                moving_type = Moving_type.standby;
                StartCoroutine(Ship_standby());
                break;
            case 'b':
                moving_type = Moving_type.move_to_point;
                StartCoroutine(Move_to_point());
                break;
        }
    }
    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
    private void Update()
    {
        if (target != null)
        {
            target_distance = (target.transform.position - transform.position).magnitude;
            radius_vector = target.transform.position - transform.position;
            Where_target();

            if (moving_type == Moving_type.front_attack)
            {
                q_ship = Quaternion.LookRotation(transform.position - target.transform.position);
                q_ship *= Quaternion.Euler(0, 180, 0);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, q_ship, Time.deltaTime * ship_turn_speed);
                transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);
            }
            else if (moving_type == Moving_type.forward_attack)
            {
                if (target_range != Target_range.shortrange)
                {
                    q_ship = Quaternion.LookRotation(transform.position - target.transform.position);
                    q_ship *= Quaternion.Euler(0, 180, 0);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, q_ship, Time.deltaTime * ship_turn_speed);
                    transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);
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
            else if (moving_type == Moving_type.side_orbit)
            {
                // 
                if ((target_distance > 1.7 * orbit_range) || (target_distance < 0.8f * orbit_range))
                {
                    q_ship = Quaternion.LookRotation(transform.position - target.transform.position);
                    q_ship *= Quaternion.Euler(0, 180, 0);
                    this.transform.rotation = Quaternion.RotateTowards(transform.rotation, q_ship, Time.deltaTime * ship_turn_speed);
                    this.transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);
                }
                else
                {
                    q_ship = Quaternion.LookRotation(transform.position - target.transform.position);
                    q_ship *= Quaternion.Euler(0, 180, 0);
                    this.transform.rotation = Quaternion.RotateTowards(transform.rotation, q_ship, Time.deltaTime * ship_turn_speed);
                    this.transform.Translate(Vector3.left * ship_side_speed * Time.deltaTime);
                    this.transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);
                }
            }
            else if (moving_type == Moving_type.orbit)
            {
                if (target_range == Target_range.shortrange && target_distance < 0.6f * orbit_range)
                {
                    //разворот корабля боком к цели на малой дистанции для выхода на орбиту изнутри
                    q_ship = Quaternion.LookRotation(transform.position - target.transform.position);
                    q_ship *= Quaternion.Euler(0, 90, 0);
                    this.transform.rotation = Quaternion.RotateTowards(transform.rotation, q_ship, Time.deltaTime * ship_turn_speed);
                    this.transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);
                }
                else if (target_range == Target_range.shortrange && target_distance > 0.6f * orbit_range)
                {
                    //раскручиваемся по спирали
                    q_ship = Quaternion.LookRotation(transform.position - NextPosition());
                    q_ship *= Quaternion.Euler(0, 180, 0);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, q_ship, Time.deltaTime * ship_turn_speed);
                    transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);

                }
                else if (target_distance < 5 * orbit_range)
                {
                    //движение на орбиту в точку касания. Вместо цели, направление на касательную
                    Vector3 orbit_point = new Vector3(0f, 0f, 0f);
                    float a = orbit_range;
                    float b;
                    float c = (target.transform.position - transform.position).magnitude;
                    b = Mathf.Sqrt(c * c - a * a);
                    orbit_point.y = 0;

                    if (target.transform.position.x >= transform.position.x)
                    {
                        fi = Mathf.PI / 2 - Mathf.Asin(b / c) - Mathf.Asin((transform.position.z - target.transform.position.z) / c);
                        orbit_point.x = target.transform.position.x - a * Mathf.Sin(fi);
                        orbit_point.z = target.transform.position.z + a * Mathf.Cos(fi);
                    }
                    else
                    {
                        fi = Mathf.PI / 2 - Mathf.Asin(b / c) - Mathf.Asin((-transform.position.z + target.transform.position.z) / c);
                        orbit_point.x = target.transform.position.x + a * Mathf.Sin(fi);
                        orbit_point.z = target.transform.position.z - a * Mathf.Cos(fi);
                    }

                    q_ship = Quaternion.LookRotation(transform.position - orbit_point);
                    q_ship *= Quaternion.Euler(0, 180, 0);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, q_ship, Time.deltaTime * ship_turn_speed);
                    transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);
                }
                else
                //на большом расстоянии прямое движение на цель
                {
                    q_ship = Quaternion.LookRotation(transform.position - target.transform.position);
                    q_ship *= Quaternion.Euler(0, 180, 0);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, q_ship, Time.deltaTime * ship_turn_speed);
                    transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);
                }
            }
            else if (moving_type == Moving_type.ace_attack)
            {
                if (target_range == Target_range.shortrange && target_distance < 0.5 * orbit_range)
                {
                    this.transform.Rotate(new Vector3(0, -ship_turn_speed, 0) * Time.deltaTime);
                    this.transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);
                }
                else
                {
                    q_ship = Quaternion.LookRotation(this.transform.position - target.transform.position);
                    q_ship *= Quaternion.Euler(0, 180, 0);
                    this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, q_ship, Time.deltaTime * ship_turn_speed);
                    this.transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);
                }
            }
            else if (moving_type == Moving_type.test)
            {
                q_ship = Quaternion.LookRotation(target.transform.position - transform.position);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, q_ship, Time.deltaTime * ship_turn_speed);
                transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);
            }
            else if (moving_type == Moving_type.move_to_point)
            {
                q_ship = Quaternion.LookRotation(transform.position - target.transform.position);
                q_ship *= Quaternion.Euler(0, 180, 0);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, q_ship, Time.deltaTime * ship_turn_speed);
                transform.Translate(Vector3.forward * ship_speed * Time.deltaTime);
            }
            else
                moving_type = Moving_type.standby;
        }
    }

    void Where_target()
    {
        target_angle = Vector3.Angle(radius_vector, transform.forward);
        if (target_angle < 30)
            target_orient = Target_orient.forward;
        else if (target_angle >= 150)
            target_orient = Target_orient.back;
        else
            target_orient = Target_orient.side;

        if (target_distance <= short_range)
            target_range = Target_range.shortrange;
        else if (target_distance < long_range)
            target_range = Target_range.middlerange;
        else if (target_distance >= long_range)
            target_range = Target_range.longrange;
    }
    IEnumerator Ship_turn_side()
    {
        isBusy = true;
        ship_turn_side = 1 - 2 * Random.Range(0, 2);
        yield return new WaitForSeconds(3f);
        isBusy = false;
    }
    private Vector3 NextPosition()
    {
        Vector3 nextPosition;
        float radius;
        float _radiusDelta = 2f, _angleDelta = 2f;
        float angle = Mathf.Atan2(-radius_vector.x, -radius_vector.z);

        radius = target_distance;
        radius += _radiusDelta * Time.deltaTime;
        angle += _angleDelta * Time.deltaTime;

        radius = Mathf.Min(radius, orbit_range);

        nextPosition = target.transform.position + radius * new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
        return nextPosition;
    }
    private void Change_speed(float toSpeed, float acceleration_multiplier = 1f)
    {
        if (ship_speed + ship_acceleration * acceleration_multiplier < toSpeed)
        {
            ship_speed += ship_acceleration * acceleration_multiplier;
        }
        else if (ship_speed - ship_breaking * acceleration_multiplier > toSpeed)
        {
            ship_speed -= ship_breaking * acceleration_multiplier;
        }
        else
        {
            ship_speed = toSpeed;
        }
    }
    private float Break_distance()
    {
        float break_distance;
        break_distance = ship_speed * ship_speed / 10 * ship_acceleration;
        return break_distance;
    }
}
