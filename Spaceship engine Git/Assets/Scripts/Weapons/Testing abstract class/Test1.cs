using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : Test, IWeapon_test
{
    public override void A2()
    {
        Debug.Log("тест 1");
    }
    public void S1()
    {
        Debug.Log("test 1");
    }
}
