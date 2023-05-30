using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace test
{
    public abstract class Weapon_test : MonoBehaviour
    {
        public abstract void Fire();
    }
    public class Laser : Weapon_test
    {
        public override void Fire()
        {
            //Shooting from Laser
        }
    }
    public class Cannon : Weapon_test
    {
        public override void Fire()
        {
            //Shooting from Cannon
        }
    }
    public class Player : MonoBehaviour
    {
        //на плеере несколько лазеров и пушек, точно кол-во не известно
    }
}
