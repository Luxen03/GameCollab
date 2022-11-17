using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class MonoCore : MonoBehaviour
{
    #region DataBases
        //Player Information
        public class PlayerDB
        {
            public string Name = "John Doe";
            public Transform PT;
            public int AttackType;
            public List<int> WeaponLineup = new List<int>();
            public int Combo;
            public MClock.PClock ComboCooldown = new MClock.PClock(1f);
            public MClock.PClock AttackCooldown = new MClock.PClock(.5f);
        }

        PlayerDB SoldierBoi = new PlayerDB();
    #endregion

    void Start()
    {
    }

    void Update()
    {
        MClock.Update();
    }
}
