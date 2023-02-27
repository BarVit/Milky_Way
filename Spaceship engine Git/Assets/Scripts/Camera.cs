using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform target;
    float xSensitivity = 500f;
    float ySensitivity = 500f;
    float _currentRotation = 0f;
    Vector3 cameraPosition;

    //для ускорения расчетов дистанция ограничения указана в квадрате
    float zoom_max_in = 9, zoom_max_out = 40000;
    float zoomAmount = 1.3f;

    private float y_rotation, y_rotation_last;
    Quaternion target_last_rotation, target_rotation;

    private void Start()
    {
        cameraPosition = target.position - transform.position;
        target_last_rotation = target.rotation;
        y_rotation_last = target.rotation.eulerAngles.y;
    }

    private void LateUpdate()
    {
        if (transform.parent == null)
        {
            //свободная камера, к объекту не прицеплена, нет родителя
            transform.position = target.position - cameraPosition;

            float mouseX = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
            float mouse3 = Input.GetAxis("Mouse ScrollWheel");

            float tmp = Mathf.Clamp(_currentRotation + mouseY, -89f, 89f);
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

            float zoomAmount = 1.3f;
            if (mouse3 > 0 && (target.position - transform.position).sqrMagnitude > zoom_max_in)
            {
                transform.position = target.position - (target.position - transform.position) / zoomAmount;
            }

            if (mouse3 < 0 && (target.position - transform.position).sqrMagnitude < zoom_max_out)
            {
                transform.position = target.position - (target.position - transform.position) * zoomAmount;
            }
            cameraPosition = target.position - transform.position;
        }
        else
        {
            //камера прицеплена к объекту, является дочерним объектом
            y_rotation = target.rotation.eulerAngles.y;
            //target_rotation = target.rotation;

            if (target_rotation != target_last_rotation)
            {
                transform.RotateAround(target.position, Vector3.up, -(y_rotation - y_rotation_last));
                y_rotation_last = target.rotation.eulerAngles.y;
                target_last_rotation = target.rotation;
            }
            float mouseX = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
            float mouse3 = Input.GetAxis("Mouse ScrollWheel");

            float tmp = Mathf.Clamp(_currentRotation + mouseY, -89f, 89f);
            if (tmp != _currentRotation)
            {
                float rot = tmp - _currentRotation;
                transform.RotateAround(target.position, transform.right, -rot);
                _currentRotation = tmp;
            }
            transform.RotateAround(target.position, Vector3.up, mouseX);
            if (mouse3 > 0 && (target.position - transform.position).sqrMagnitude > zoom_max_in)
            {
                transform.position = target.position - (target.position - transform.position) / zoomAmount;
            }

            if (mouse3 < 0 && (target.position - transform.position).sqrMagnitude < zoom_max_out)
            {
                transform.position = target.position - (target.position - transform.position) * zoomAmount;
            }
        }
    }
}