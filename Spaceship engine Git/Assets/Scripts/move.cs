using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public GameObject centerPoint;
    public float speed;
    public float turnSpeed;
    public float maxSpeed;
    public float maxTurnSpeed;
    public float circleRadius;
    private float circleSpeed;
    private float circleTurnSpeed;
    private string orientation;
    [SerializeField] private float pointAngle;

    private Quaternion q;

    void Start()
    {
        speed = 0;
        turnSpeed = 50;
        maxSpeed = 50;
        maxTurnSpeed = 50;

        //константы для теста, соответствуют радиусу 29.3615
        circleRadius = 29.3615f;
        circleSpeed = 21;
        circleTurnSpeed = 41;
    }
    void Where_point()
    {
        pointAngle = Vector3.Angle(centerPoint.transform.position - transform.position, transform.forward);
        if (pointAngle < 30)
            orientation = "forward";
        else if (pointAngle >= 150)
            orientation = "back";
        else
            orientation = "side";
    }
    IEnumerator Go_Circle()
    {
        while (true)
        {
            //можем менять скорость движения и поворота 10 раз/сек
            yield return new WaitForSeconds(0.1f);
            if (orientation == "forward" || orientation == "back")
            {
                //если цель впереди или сзади, то замедляемся и поворачиваемся боком
                if (speed > 5)
                    speed -= 2;
                else
                    speed += 1;
                if (turnSpeed < maxTurnSpeed)
                    turnSpeed += 2;
                else
                    turnSpeed -= 1;
            }
            if (orientation == "side")
            {
                
                float target_distance = (transform.position - centerPoint.transform.position).magnitude;
                //Если стоим боком к цели, то раскручиваемся по спирали
                if (pointAngle > 85 && pointAngle < 100)
                {
                    if (speed < circleSpeed)
                        speed += 1;
                    else
                        speed -= 1;
                    if (turnSpeed < maxTurnSpeed)
                        turnSpeed += 2;
                    else
                        turnSpeed -= 1;
                }
                //если не стоим боком, то замедление и поворот боком
                else
                {
                    if (speed > 5)
                        speed -= 2;
                    else
                        speed += 1;
                    if (turnSpeed < maxTurnSpeed)
                        turnSpeed += 2;
                    else
                        turnSpeed -= 1;
                }                
            }
        }
    }
    void Update()
    {
        Where_point();
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            StartCoroutine(Go_Circle());
        }
        q = Quaternion.LookRotation(transform.position - NextPosition());
        q *= Quaternion.Euler(0, 180, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * turnSpeed);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    Vector3 NextPosition()
    {
        Vector3 nextPosition;
        float _radiusDelta = 2f, _angleDelta = 1f;
        float radius = (transform.position - centerPoint.transform.position).magnitude;
        float angle = Mathf.Atan2((transform.position - centerPoint.transform.position).x, (transform.position - centerPoint.transform.position).z);

        radius += _radiusDelta * Time.deltaTime;
        angle += _angleDelta * Time.deltaTime;
        radius = Mathf.Min(radius, circleRadius);

        nextPosition = centerPoint.transform.position + radius * new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
        return nextPosition;
    }
}
