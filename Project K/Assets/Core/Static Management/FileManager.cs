using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Core;

public static class FileManager
{

    public class PlayerInformation
    {
        //Identification
        public string Name = "John Doe";

        //Loadout
        [XmlArrayItem("Weapon")]
        public List<string> WeaponLineup = new List<string>();

        //Hidden information
        public struct Bars
        {
            public int Current;
            public int Max;
        }
        public Bars HP;
        public Bars MP;
        public Bars Stamina;

        public PlayerInformation()
        {
            //Debugging purposes only, delete after
            HP.Max = 100;
            MP.Max = 100;
            Stamina.Max = 100;
            WeaponLineup.Add("One Handed Sword");
            WeaponLineup.Add("Shield");
        }
    }

    internal static PlayerInformation FileParse = new PlayerInformation();

    public static void WriteFile()
    {
        using(StreamWriter stream = new StreamWriter(Application.persistentDataPath + "/PlayerInformation.xml"))
        {
            new XmlSerializer(typeof(PlayerInformation)).Serialize(stream, FileParse);
            Debug.Log("Written in \"" + Application.persistentDataPath + "/PlayerInformation.xml\"");
        }
    }


    public static void DeleteFile()
    {
        //
    }

    public static void ReadFile()
    {
		using(StreamReader stream = new StreamReader(Application.persistentDataPath + "/PlayerInformation.xml"))
        {
            PlayerInformation Output = new XmlSerializer(typeof(PlayerInformation)).Deserialize(stream.BaseStream) as PlayerInformation;
            Debug.Log("Read as " + Output);
        }
    }
}
