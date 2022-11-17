using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Core
{
    static class StageMechanics
    {
        //All Players
        static MonoCore Mono;
        static MonoCore.PlayerDB[] PlayerLibrary;
    }





    static class AttackMechanics
    {
        static void Attack(MonoCore.PlayerDB _Player, MonoCore.PlayerDB _Target)
        {
            //Combo Parse
            int Weapon = _Player.Combo;
            if (_Player.ComboCooldown.IsMax() || Weapon >= _Player.WeaponLineup.Count) _Player.Combo = 0;
            else _Player.Combo++;

            //Total of all Loadout stats and buffs


            //Attack Conditions
            if ((_Target.PT.position - _Player.PT.position).magnitude < 20 || _Player.AttackType == 0){
                //Melee
                //if weapon sharp:
                    DMG = ((TotalATK * ClassPhysDMG) * (1 + (ComboCount * 0.25))) - EnemyPhysDef
                //if weapon blunt:
                    DMG = ((TotalATK * ClassPhysDMG) * (1 + (ComboCount * 0.25))) * 0.25

                Damage(_Target, DMG);

            } else {
                //Ranged
            }
        }

        static void Damage(MonoCore.PlayerDB _Target, int _Damage)
        {
            Debug.Log($"Dealt {_Damage:.2f} Damage to player {_Target.Name}!");
        }
    }




    public static class MClock
    {
        internal static List<PClock> ClockLibrary = new List<PClock>();
        public class PClock
        {
            internal float Time = 0f;
            internal float Max;
            //Constructor
            public PClock(float _Max){
                Debug.Log("Clock Made");
                Max = _Max;
                ClockLibrary.Add(this);
            }
            ~PClock(){ Debug.Log("Clock Deleted"); }

            //methods
            public void Destroy(){ ClockLibrary.Remove(this); }
            public void Set(float _Time){ Time = _Time; }
            public bool IsMax(){ return Max == Time; }
            public float Read(){ return Time; }
        }

        public static void Update()
        {
            //for each Clock
            for (int i = 0; i < ClockLibrary.Count; i++){
                ClockLibrary[i].Time += Time.deltaTime;
                //if hit max
                if (ClockLibrary[i].Time >= ClockLibrary[i].Max){
                    ClockLibrary[i].Time = ClockLibrary[i].Max;
                    ClockLibrary[i].Destroy();
                    i--;
                    return;
                }
            }
        }
    }
}