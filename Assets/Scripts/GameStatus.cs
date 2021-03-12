using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    public bool GameSituation;

    private void Start()
    {
        GameSituation = false;
    }

    public void RoundEnd()
    {
        GameSituation = false;
    }

    public void RoundStart()
    {
        GameSituation = true;
    }





}
