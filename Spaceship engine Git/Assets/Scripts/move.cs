using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public GameObject point;
    public float speed;
    public float turnSpeed;
    public float maxSpeed;
    public float maxTurnSpeed;
    public float circleRadius;
    private float circleSpeed;
    private float circleTurnSpeed;
    private string orientation;
    private float pointAngle;

    private Quaternion q;

    void Start()
    {
        speed = 0;
        turnSpeed = 50;
        maxSpeed = 50;
        maxTurnSpeed = 50;
        circleRadius = 29.3615f;

        //константы для теста, соответствуют радиусу 29.3615
        circleSpeed = 21;
        circleTurnSpeed = 41;
    }

    IEnumerator Go_Circle()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            //выход на окружность изнутри по спирали
            if (orientation == "forward" || orientation == "back")
            {
                if (speed > 5)
                    speed -= 2;
                else if (speed < 0)
                    speed += 1;
                if (turnSpeed < maxTurnSpeed)
                    turnSpeed += 2;
                else
                    turnSpeed -= 1;
            }
            if (orientation == "side")
            {
                //Если стоим боком к цели, то раскручиваемся по спирали
                if (pointAngle > 70 && pointAngle < 110)
                {
                    if (speed < circleSpeed * (this.transform.position - point.transform.position).magnitude / circleRadius)
                        speed += 1;
                    else
                        speed -= 1;
                    if (turnSpeed < circleTurnSpeed)
                        turnSpeed += 2;
                    else
                        turnSpeed -= 1;
                }
                // если не стоим боком, то замедление и поворот боком
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
        //StartCoroutine(Go_Circle());

        //q = Quaternion.LookRotation(this.transform.position - point.transform.position);
        //if ((point.transform.position - this.transform.position).magnitude < 0.95 * circleRadius)
        //    q *= Quaternion.Euler(0, 90, 0);
        //else
        //    q *= Quaternion.Euler(0, 180, 0);
        //this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, q, Time.deltaTime * turnSpeed);
        //this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        this.transform.position = NextPosition();
    }

    void Where_point()
    {
        pointAngle = Vector3.Angle(point.transform.position - this.transform.position, this.transform.forward);
        if (pointAngle < 30)
            orientation = "forward";
        else if (pointAngle >= 150)
            orientation = "back";
        else
            orientation = "side";
    }
    Vector3 NextPosition()
    {
        Vector3 nextPosition;
        float _radiusDelta = 4f, _angleDelta = 3f;
        float radius = (this.transform.position - point.transform.position).magnitude;
        float angle = Mathf.Atan2((this.transform.position - point.transform.position).x, (this.transform.position - point.transform.position).z);

        radius += _radiusDelta * Time.deltaTime;
        angle += _angleDelta * Time.deltaTime;

        radius = Mathf.Min(radius, circleRadius);

        nextPosition = point.transform.position + radius * new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
        return nextPosition;
    }
}
