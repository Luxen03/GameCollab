using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class MonoCore : MonoBehaviour
{
    void Start()
    {
        #region DebugLines (Delete when done)
            for (int i = 0; i < 3; i++) StageMechanics.PlayerLibrary.Add(new PlayerDB());

        #endregion
    }

    void Update()
    {
        MClock.Update();
        AttackMechanics.Attack(StageMechanics.PlayerLibrary[0], StageMechanics.PlayerLibrary[1]);
    }
}
