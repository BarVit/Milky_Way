using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Test : MonoBehaviour
{
    public void A1(string text)
    {
        Debug.Log(text + " ����� �� ������������ ������");
    }
    public abstract void A2();

    private void Awake()
    {
        A1("�� ����� ������");
    }
}