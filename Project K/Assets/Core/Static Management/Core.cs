using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Core
{
    #region Instances

        //Player Information
        public class PlayerDB
        {
            //Identification
            public string Name = "John Doe";
            public Transform PT;

            //Loadout
            public List<string> WeaponLineup = new List<string>();

            //Hidden information
            public int Combo = 0;
            public struct Bars
            {
                public int Current;
                public int Max;
            }
            public Bars HP;
            public Bars MP;
            public Bars Stamina;

            //Player Cooldowns
            public MClock.PClock ComboCooldown = new MClock.PClock(1f);
            public MClock.PClock AttackCooldown = new MClock.PClock(.5f);

            public PlayerDB(string _Name, GameObject _Body)
            {
                Name = _Name;
                HP.Current = HP.Max;
                PT = _Body.transform;
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
    //Universal Information
    public static class StageMechanics
    {
        public static MonoCore Mono;
        //contains all weapons of all players
        public static Dictionary<string, Weapon> WeaponsDictionary = new Dictionary<string, Weapon>();
        //contains all players
        public static List<PlayerDB> PlayerLibrary = new List<PlayerDB>();
    }

    public static class BotHubMechanics
    {
        public class Bot
        {
            public PlayerDB Player;
            public BotMechanics Behaviour;

            public Bot()
            {
                GameObject Body = GameObject.Instantiate(Resources.Load("Prefabs/Player") as GameObject);
                Body.transform.position = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
                BotLibrary.Add(this);
                StageMechanics.PlayerLibrary.Add(new PlayerDB("Bot" + BotLibrary.Count, Body));
                Debug.Log("Made Bot" + BotLibrary.Count);
            }
        }

        public static List<Bot> BotLibrary = new List<Bot>();
    }

    //How attacking is processed
    public static class AttackMechanics
    {
        //parse damage type, play attack animation, dmg calculation
        public static void Attack(PlayerDB _Player, PlayerDB _Target)
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
                Damage(_Player, _Target, DMG);

            } else {
                //Ranged
                int DMG = 20;
                Damage(_Player, _Target, DMG);
            }
        }

        //actual damage, play damaged calculation, check if dead
        static void Damage(PlayerDB _Player, PlayerDB _Target, int _Damage)
        {
            //already dead
            if (_Target.HP.Current == 0){
                return;
            }

            _Target.HP.Current -= _Damage;

            //Death
            if (_Target.HP.Current < 1){
                _Target.HP.Current = 0;
                Debug.Log($"{_Player.Name} killed {_Target.Name} with {_Damage:D2} damage!");
                return;
            }
            Debug.Log($"{_Player.Name} dealt {_Damage:D2} Damage to {_Target.Name}! Remaining HP : {_Target.HP.Current}");
        }
    }

    //class for managing clocks, timers, etc
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