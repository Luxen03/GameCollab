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
            public GameObject Dummy = new GameObject("Dummy");
            public Transform PT = Dummy.transform;
            public int AttackType = 0;
            public List<int> WeaponLineup = new List<int>();
            public int Combo = 0;
            public MClock.PClock ComboCooldown = new MClock.PClock(1f);
            public MClock.PClock AttackCooldown = new MClock.PClock(.5f);
        }

        PlayerDB SoldierBoi = new PlayerDB();
    #endregion

    void Start()
    {
        Dummy = new GameObject();
        for (int i = 0; i < 3; i++){
            StageMechanics.PlayerLibrary.Add(new PlayerDB());
        }
    }

    void Update()
    {
        MClock.Update();
        AttackMechanics.Attack(StageMechanics.PlayerLibrary[0], StageMechanics.PlayerLibrary[1]);
    }
}
