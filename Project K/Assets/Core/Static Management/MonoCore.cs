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

        GameObject Body = GameObject.Instantiate(Resources.Load("Prefabs/Player") as GameObject);
        Body.name = "Player";
    }

    void Update()
    {
        MClock.Update();
    }

    public bool AppQuit()
    {
        Debug.Log("User wants to quit");
        return false;
    }
}
