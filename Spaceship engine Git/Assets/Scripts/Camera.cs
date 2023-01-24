using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    float xSensitivity = 500f;
    float ySensitivity = 500f;
    float _currentRotation = 0f;
    Vector3 cameraPosition;

    private void Start()
    {
        cameraPosition = target.position - transform.position;
    }

    private void LateUpdate()
    {
        transform.position = target.position - cameraPosition;

        float mouseX = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
        float mouse3 = Input.GetAxis("Mouse ScrollWheel");

        float tmp = Mathf.Clamp(_currentRotation + mouseY, -90f, 90f);
        if (tmp != _currentRotation)
        {
            float rot = tmp - _currentRotation;
            transform.RotateAround(target.position, transform.right, -rot);
            _currentRotation = tmp;
        }
        if (mouseX != 0)
        {
            transform.RotateAround(target.position, Vector3.up, mouseX);
        }
        if (mouse3 > 0)
        {
            //доработать минимальное и максимальное приближение отдаление
            //float range = Mathf.Clamp((target.position - transform.position).sqrMagnitude, -1, 1);
            transform.position = target.position - (target.position - transform.position) * (0.8f - mouse3);
        }
            
        if (mouse3 < 0)
            transform.position = target.position - (target.position - transform.position) * (1.2f - mouse3);

        cameraPosition = target.position - transform.position;
    }
}

//рабочее решение, когда камера цепляется к объекту
//    public Transform target;
//    float xSensitivity = 500f;
//    float ySensitivity = 500f;

//    private float _currentRotation = 0f;
//    public float yy, yy_last;


//    Quaternion t_last_rot, t_rot;

//    void Start()
//    {
//        t_last_rot = target.rotation;
//        yy_last = target.rotation.eulerAngles.y;
//    }

//    void LateUpdate()
//    {
//        yy = target.rotation.eulerAngles.y;
//        t_rot = target.rotation;

//        if (t_rot != t_last_rot)
//        {
//            transform.RotateAround(target.position, Vector3.up, -(yy-yy_last));
//            yy_last = target.rotation.eulerAngles.y;
//            t_last_rot = target.rotation;
//        }
//        float mouseX = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
//        float mouseY = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;

//        float tmp = Mathf.Clamp(_currentRotation + mouseY, -75f, 105f);
//        if (tmp != _currentRotation)
//        {
//            float rot = tmp - _currentRotation;
//            transform.RotateAround(target.position, transform.right, -rot);
//            _currentRotation = tmp;
//        }
//        transform.RotateAround(target.position, Vector3.up, mouseX);
//    }
//}








//transform.position = target.TransformPoint(_localPos);

//float mouseX = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
//float mouseY = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;

//float tmp = Mathf.Clamp(_currentRotation + mouseY, -75f, 105f);
//if (tmp != _currentRotation)
//{
//    float rot = tmp - _currentRotation;
//    //transform.RotateAround(target.position, transform.right, -rot);
//    _currentRotation = tmp;
//}
//transform.RotateAround(target.position, Vector3.up, mouseX);
//_localPos = target.InverseTransformPoint(transform.position);
//}
//}

//другое решение, но эффект тот же
//public Transform target;
//public Transform cam;
//float yMin = 90f, yMax = 90f;
//float xSens = 5f, ySens = 5f;
//private void Start()
//{
//    //Cursor.lockState = CursorLockMode.Locked;
//    //Cursor.visible = false;
//}
//float mouseX = Input.GetAxis("Mouse X");
//float mouseY = Input.GetAxis("Mouse Y");

//cam.rotation *= Quaternion.AngleAxis(mouseX * xSens, Vector3.up);
//cam.rotation *= Quaternion.AngleAxis(mouseY * ySens, Vector3.right);

//float angleX = cam.localEulerAngles.x;
//if (angleX > 180 && angleX < yMax)
//{
//    angleX = yMax;
//}
//else if (angleX < 180 && angleX > yMin)
//{
//    angleX = yMin;
//}
//cam.localEulerAngles = new Vector3(angleX, cam.localEulerAngles.y, 0);

////ниже код вращает камеру вокруг точки нахождения камеры, а не вокруг игрока
//public Transform target;
//float xSensitivity = 200f;
//float ySensitivity = 200f;

//private Vector3 _localPos;
//private float _currentRotation = 0f;

//void Start()
//{
//    //_localPos = target.InverseTransformPoint(transform.position);
//}

//void LateUpdate()
//{
//    //transform.position = target.TransformPoint(_localPos);

//    float mouseX = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
//    float mouseY = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;

//    if (mouseY != 0)
//    {
//        float tmp = Mathf.Clamp(_currentRotation + mouseY * ySensitivity * Time.deltaTime, -60f, 60f);
//        if (tmp != _currentRotation)
//        {
//            float rot = tmp - _currentRotation;
//            transform.RotateAround(transform.position, transform.right, -rot);
//            _currentRotation = tmp;
//        }
//    }
//    if (mouseX != 0)
//    {
//        transform.RotateAround(transform.position, Vector3.up, mouseX * xSensitivity * Time.deltaTime);
//    }
//    _localPos = target.InverseTransformPoint(transform.position);
//}

