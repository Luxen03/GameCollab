using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

[RequireComponent(typeof(BaseMechanics))]
public class BotMechanics : MonoBehaviour
{
    private BaseMechanics Base;

    // Start is called before the first frame update
    public void StartUp(PlayerDB _Player)
    {
        Base = transform.GetComponent<BaseMechanics>();
        Base.Player = _Player;
    }

    //Bot Behaviour
    void Update()
    {
        
    }
}
