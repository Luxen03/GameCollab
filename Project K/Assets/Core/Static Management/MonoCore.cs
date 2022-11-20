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
            //Identification
            public string Name = "John Doe";
            public Transform PT;


            //Loadout
            public List<Weapon> WeaponLineup = new List<Weapon>();


            //Hidden information
            public int Combo = 0;
            public int MaxHP = 100;
            public int HP = 0;
            public int MaxMP = 100;
            public int MP = 0;


            //Player Cooldowns
            public MClock.PClock ComboCooldown = new MClock.PClock(1f);
            public MClock.PClock AttackCooldown = new MClock.PClock(.5f);


            public PlayerDB()
            {
                HP = MaxHP;
                PT = new GameObject("Dummy").transform;
            }
        }

        public class Weapon
        {
            public string Name;
            public int[] Types = new int[3];
            public int Damage;
            public int Hands;
            public Weapon(int WeaponNo)
            {

            }
        }


    #endregion

    void Start()
    {
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
