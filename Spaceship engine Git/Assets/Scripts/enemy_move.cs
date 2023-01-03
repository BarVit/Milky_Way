using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_move : MonoBehaviour
{
    public GameObject ship;
    public float speed;
    public float side_speed;
    public float turn_speed;
    float ship_distance;
    Vector3 targetPoint;
    
    Quaternion q;

    string move_type;
    bool isRotate;

    Vector3 line_a = new Vector3(0, 0, 0);
    Vector3 line_b = new Vector3(30, 0, 0);

    Vector3 tri_a = new Vector3(0, 0, 0);
    Vector3 tri_b = new Vector3(15, 0, 20);
    Vector3 tri_c = new Vector3(30, 0, 0);

    Vector3 sq_a = new Vector3(0, 0, 0);
    Vector3 sq_b = new Vector3(0, 0, 30);
    Vector3 sq_c = new Vector3(30, 0, 30);
    Vector3 sq_d = new Vector3(30, 0, 0);


    // Start is called before the first frame update
    void Start()
    {
        speed = 10;
        side_speed = 0;
        turn_speed = 30;
        targetPoint = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ship_distance = (ship.transform.position - this.transform.position).magnitude;
        //остановка
        if (Input.GetKeyDown(KeyCode.Space))
        {
            move_type = "stop";
            isRotate = false;
        }
        //движение туда-сюда
        if (Input.GetKeyDown(KeyCode.Q))
        {
            move_type = "line";
            targetPoint = line_a;
            isRotate = false;
        }
        //движение по треугольнику
        if (Input.GetKeyDown(KeyCode.W))
        {
            move_type = "tri";
            targetPoint = tri_a;
            isRotate = false;
        }
        //движение по квадрату
        if (Input.GetKeyDown(KeyCode.E))
        {
            move_type = "sq";
            targetPoint = sq_a;
            isRotate = false;
        }
        //движение по кругу
        if (Input.GetKeyDown(KeyCode.R))
        {
            move_type = "orbit";
            isRotate = true;
        }
        //движение за кораблем
        if (Input.GetKeyDown(KeyCode.T))
        {
            move_type = "toShip";
            isRotate = false;
        }
        //движение от корабля
        if (Input.GetKeyDown(KeyCode.Y))
        {
            move_type = "fromShip";
            isRotate = false;
        }

        switch (move_type)
        {
            case "stop":
                targetPoint = this.transform.position;
                break;
            case "line":
                if ((line_a - this.transform.position).magnitude < 0.5)
                {
                    targetPoint = line_b;
                }
                else if ((line_b - this.transform.position).magnitude < 0.5)
                {
                    targetPoint = line_a;
                }
                break;
            case "tri":
                if ((tri_a - this.transform.position).magnitude < 0.5)
                {
                    targetPoint = tri_b;
                }
                else if ((tri_b - this.transform.position).magnitude < 0.5)
                {
                    targetPoint = tri_c;
                }
                else if ((tri_c - this.transform.position).magnitude < 0.5)
                {
                    targetPoint = tri_a;
                }
                break;
            case "sq":
                if ((sq_a - this.transform.position).magnitude < 0.5)
                {
                    targetPoint = sq_b;
                }
                else if ((sq_b - this.transform.position).magnitude < 0.5)
                {
                    targetPoint = sq_c;
                }
                else if ((sq_c - this.transform.position).magnitude < 0.5)
                {
                    targetPoint = sq_d;
                }
                else if ((sq_d - this.transform.position).magnitude < 0.5)
                {
                    targetPoint = sq_a;
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
    }
}
