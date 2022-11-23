using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class MonoCore : MonoBehaviour
{
    void Start()
    {
        #region DebugLines (Delete when done)
            for (int i = 0; i < 15; i++) new BotHubMechanics.Bot();

        #endregion
        Application.wantsToQuit += AppQuit;
    }

    void Update()
    {
        MClock.Update();
        AttackMechanics.Attack(StageMechanics.PlayerLibrary[0], StageMechanics.PlayerLibrary[1]);
    }

    public bool AppQuit()
    {
        Debug.Log("User wants to quit");
        return false;
    }
}
