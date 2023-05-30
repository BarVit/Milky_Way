using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : Test, IWeapon_test
{
    public override void A2()
    {
        Debug.Log("тест 2");
    }
    public void S1()
    {
        Debug.Log("test 2");
    }
}