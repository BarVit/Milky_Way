using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Out : MonoBehaviour
{
    private IWeapon_test S;
    private Test[] t_arr;
    //private Test t;
    private void Awake()
    {
        S = transform.GetChild(0).GetComponent<IWeapon_test>();
        //t = GetComponent<Test>();

        t_arr = new Test[3];
        for (int i = 0; i < transform.childCount; i++)
        {
            t_arr[i] = transform.GetChild(i).GetComponent<Test>();
        }
    }

    private void Start()
    {
        //t.A1("из внешнего класса");
        //t.A2();
        if (S == null)
            Debug.Log("нет компонента");
        else
            S.S1();
        //t_arr[0].A2();
        foreach (Test q in t_arr)
        {
            q.A2();
        }
    }
}
