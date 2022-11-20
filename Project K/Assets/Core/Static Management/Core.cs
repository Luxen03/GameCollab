using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Core
{
    public static class StageMechanics
    {
        //All Players
        public static MonoCore Mono;
        public static List<MonoCore.PlayerDB> PlayerLibrary = new List<MonoCore.PlayerDB>();
    }





    public static class AttackMechanics
    {
        public static void Attack(MonoCore.PlayerDB _Player, MonoCore.PlayerDB _Target)
        {
            //Combo Parse
            int Weapon = _Player.Combo;
            if (_Player.ComboCooldown.IsMax() || Weapon >= _Player.WeaponLineup.Count) _Player.Combo = 0;
            else _Player.Combo++;

            //Total of all Loadout stats and buffs


            //Damage Type Formula
            //if physical
                    //if weapon sharp:
                        //DMG = ((TotalATK * ClassPhysDMG) * (1 + (ComboCount * 0.25))) - EnemyPhysDef
                    //if weapon blunt:
                        //DMG = ((TotalATK * ClassPhysDMG) * (1 + (ComboCount * 0.25))) * 0.25
                //if magical
                    //DMG = (TotalATK * ClassMagDMG) * (1 - EnemyMagDef)

            //Attack Conditions
            //if ((_Target.PT.position - _Player.PT.position).magnitude < 20 || _Player.AttackType == 0){
            if ((_Target.PT.position - _Player.PT.position).magnitude < 20){
                //Melee
                int DMG = 20;
                Damage(_Target, DMG);

            } else {
                //Ranged
                int DMG = 20;
                Damage(_Target, DMG);
            }
        }

        static void Damage(MonoCore.PlayerDB _Target, int _Damage)
        {
            //already dead
            if (_Target.HP == 0){
                return;
            }

            _Target.HP -= _Damage;

            //Death
            if (_Target.HP < 1){
                _Target.HP = 0;
                Debug.Log($"Dealt {_Damage:D2} to player {_Target.Name} and then he Died!");
                return;
            }
            Debug.Log($"Dealt {_Damage:D2} Damage to player {_Target.Name}! Remaining HP : {_Target.HP}");
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
                Max = _Max;
                ClockLibrary.Add(this);
            }
            //~PClock(){ Debug.Log("Clock Deleted"); }

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