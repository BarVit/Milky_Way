using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_move : MonoBehaviour
{
    public GameObject ship;
    public float speed;
    public float max_speed;
    public float side_speed;
    public float turn_speed;
    float ship_distance;
    Vector3 targetPoint;
    Quaternion q;

    string move_type;
    bool isRotate;
    bool isToTarget;
    bool isBusy;
    bool isNewPoint;

    Vector3[] waypoints;
    //, waypoints_line, waypoints_tri, waypoints_sq, waypoints_random;
    Vector3[] waypoints_line = {new Vector3(0, 0, 0),
                                 new Vector3(30, 0, 0)};
    Vector3[] waypoints_tri = {new Vector3(0, 0, 0),
                               new Vector3(15, 0, 20),
                               new Vector3(30, 0, 0)};
    Vector3[] waypoints_sq = {new Vector3(0, 0, 0),
                            new Vector3(0, 0, 30),
                            new Vector3(30, 0, 30),
                            new Vector3(30, 0, 0)};

    // Start is called before the first frame update
    void Start()
    {
        speed = 0;
        max_speed = 7;
        side_speed = 0;
        turn_speed = 30;
        isBusy = false;
        targetPoint = this.transform.position;
    }

    IEnumerator Change_speed(float new_speed)
    {
        isBusy = true;
        if (new_speed > max_speed)
        {
            new_speed = max_speed;
        }
        else if (new_speed < 0)
        {
            new_speed = 0;
        }
        while (speed != new_speed)
        {
            //Debug.Log(speed);
            if (speed < new_speed)
            {
                speed += max_speed * 0.1f;
            }
            else if (speed > new_speed)
            {
                speed -= max_speed * 0.1f;
            }
            if (Mathf.Abs(speed - new_speed) < max_speed * 0.09f)
            {
                speed = new_speed;
            }
            yield return new WaitForSeconds(0.1f);
        }
        isBusy = false;
    }
    Vector3 ClosePoint(Vector3[] point)
    {
        float distance, min_distance;
        int count = 0;
        min_distance = (this.transform.position - point[0]).sqrMagnitude;
        for (int i = 0; i < point.Length; i++)
        {
            distance = (this.transform.position - point[i]).sqrMagnitude;
            if (distance < min_distance)
            {
                min_distance = distance;
                count = i;
            }
        }
        return point[count];
    }
    Vector3 NextPoint(Vector3[] point)
    {
        float distance, zero_distance;
        int count = 0;
        zero_distance = 0.5f;
        if (point.Length == 1)
        {
            return point[0];
        }
        for (int i = 0; i < point.Length; i++)
        {
            distance = (this.transform.position - point[i]).sqrMagnitude;
            if (distance < zero_distance)
            {
                if (point.Length == 2)
                {
                    count = (1 - i) + (i - 1) * i;
                }
                else
                {
                    if (i <= point.Length - 2)
                    {
                        count = i + 1;
                    }
                    else
                    {
                        count = 0;
                    }
                }
                break;
            }
        }
        return point[count];
    }
    void Update()
    {
        ship_distance = (ship.transform.position - this.transform.position).magnitude;        

        //остановка
        if (Input.GetKeyDown(KeyCode.Space))
        {
            move_type = "stop";
            isRotate = false;
            isToTarget = false;
        }
        //движение туда-сюда
        if (Input.GetKeyDown(KeyCode.Q))
        {
            move_type = "line";
            isRotate = false;
            isToTarget = false;
            waypoints = waypoints_line;
            targetPoint = ClosePoint(waypoints);
            isNewPoint = true;
            StartCoroutine(Change_speed(max_speed));
        }
        //движение по треугольнику
        if (Input.GetKeyDown(KeyCode.W))
        {
            move_type = "tri";
            waypoints = waypoints_tri;
            isRotate = false;
            isToTarget = false;
            targetPoint = ClosePoint(waypoints);
            isNewPoint = true;
            StartCoroutine(Change_speed(max_speed));
        }
        //движение по квадрату
        if (Input.GetKeyDown(KeyCode.E))
        {
            move_type = "sq";
            waypoints = waypoints_sq;
            isRotate = false;            
            targetPoint = ClosePoint(waypoints);
            isNewPoint = true;
            //прописать поворот на первую ближайшую точку
            StartCoroutine(Change_speed(max_speed));
        }
        //движение по кругу
        if (Input.GetKeyDown(KeyCode.R))
        {
            move_type = "orbit";
            isRotate = true;
            isToTarget = false;
        }
        //движение за кораблем
        if (Input.GetKeyDown(KeyCode.T))
        {
            move_type = "toShip";
            isRotate = false;
            isToTarget = false;
        }
        //движение от корабля
        if (Input.GetKeyDown(KeyCode.Y))
        {
            move_type = "fromShip";
            isRotate = false;
            isToTarget = false;
        }
        //атака игрока по орбите на 0,5 от дальности стрельбы
        if (Input.GetKeyDown(KeyCode.A))
        {
            //сделать аналогично поведению игрока
        }

        switch (move_type)
        {
            case "stop":
                targetPoint = this.transform.position;
                break;
            case "line":
            case "tri":
                if (((this.transform.position - targetPoint).sqrMagnitude < 0.5) && !isNewPoint)
                {
                    targetPoint = NextPoint(waypoints);
                    isNewPoint = true;
                    if (!isBusy)
                    {
                        StartCoroutine(Change_speed(max_speed));
                    }
                }
                if ((this.transform.position - targetPoint).sqrMagnitude < 36 && isNewPoint)
                {
                    isNewPoint = false;
                    if (!isBusy)
                    {
                        StartCoroutine(Change_speed(max_speed * 0.2f));
                    }
                }
                break;
            case "sq":
                if (((this.transform.position - targetPoint).sqrMagnitude < 0.5) && !isNewPoint)
                {
                    targetPoint = NextPoint(waypoints);
                    isNewPoint = true;
                    if (!isBusy && (Vector3.Angle(targetPoint - this.transform.position, this.transform.forward) < 2))
                    {
                        StartCoroutine(Change_speed(max_speed));
                        isToTarget = false;
                    }
                    else if (Vector3.Angle(targetPoint - this.transform.position, this.transform.forward) >= 2)
                    {
                        StartCoroutine(Change_speed(0));
                        isToTarget = true;
                    }
                }
                if (speed == 0 && Vector3.Angle(targetPoint - this.transform.position, this.transform.forward) < 2)
                {
                    if (!isBusy)
                    {
                        StartCoroutine(Change_speed(max_speed));
                    }
                }
                if ((this.transform.position - targetPoint).sqrMagnitude < 36 && isNewPoint)
                {
                    isNewPoint = false;
                    if (!isBusy)
                    {
                        StartCoroutine(Change_speed(max_speed * 0.2f));
                    }
                }
                break;
            case "orbit":
                this.transform.Rotate(new Vector3(0, turn_speed, 0) * Time.deltaTime);
                this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
                break;
            case "toShip":
                if (ship_distance > 3)
                {
                    targetPoint = ship.transform.position;
                }
                else
                {
                    targetPoint = this.transform.position;
                }
                break;
            case "fromShip":
                targetPoint = this.transform.position + (this.transform.position - ship.transform.position);
                break;
        }
        if (!isRotate)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, targetPoint, speed * Time.deltaTime);
        }
        if (isToTarget)
        {
            q = Quaternion.LookRotation(this.transform.position - targetPoint);
            q *= Quaternion.Euler(0, 180, 0);
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, q, Time.deltaTime * turn_speed);
        }
    }
}
