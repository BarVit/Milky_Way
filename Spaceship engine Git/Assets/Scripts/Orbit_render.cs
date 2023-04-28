using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit_render : MonoBehaviour
{
    public LineRenderer circleRenderer;
    public Transform trParent;

    private void Update()
    {
        DrawCircle(100, 30);
    }

    void DrawCircle(int steps, float radius)
        {
            circleRenderer.positionCount = steps + 1;
            Vector3 cPos0;
            for (int currentStep = 0; currentStep < steps; currentStep++)
            {
                float circumferenceProgress = (float)currentStep / steps;
                float currentRadian = circumferenceProgress * 2 * Mathf.PI;
                float xScaled = Mathf.Cos(currentRadian);
                float zScaled = Mathf.Sin(currentRadian);
                float x = xScaled * radius;
                float z = zScaled * radius;
                if (currentStep == 0)
                {
                    cPos0 = new Vector3(x + trParent.position.x, trParent.position.y, z + trParent.position.z);
                    circleRenderer.SetPosition(steps, cPos0);
                }
                Vector3 currentPosition = new Vector3(x + trParent.position.x, trParent.position.y, z + trParent.position.z);
                circleRenderer.SetPosition(currentStep, currentPosition);
            }
        }
}
