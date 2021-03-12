using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Round : MonoBehaviour
{
    [Header("RoundStart")]
    public GameObject BlackScreen;
    Animator BlackScreenAnim;
    GameStatus gStatus;

    public Image Star1_Image, Star2_Image, Star3_Image;

    [Header("Colors")]
    public Color EmpyStarColor;
    public Color PlayerWinColor;
    public Color EnemyWinColor;

    [Header("Win_Lose")]
    public static int WinCount;
    public static int LoseCount;

    GameObject EnemyObject;
    TakeDamage EnemyScript;
    Gun playerGun;

    Timer timer;
    bool Sıfırlama;
    LevelChange lvlChange;
    bool nextLevelCheck;
    // Start is called before the first frame update
    void Start()
    {
        
        gStatus = GetComponent<GameStatus>();
        playerGun = GameObject.FindGameObjectWithTag("PlayerGun").GetComponent<Gun>();
        EnemyObject = GameObject.FindGameObjectWithTag("Enemy");
        EnemyScript = EnemyObject.GetComponent<TakeDamage>();
        Star1_Image.color = EmpyStarColor;
        Star2_Image.color = EmpyStarColor;
        Star3_Image.color = EmpyStarColor;
        BlackScreenAnim = BlackScreen.GetComponent<Animator>();
        timer = GetComponent<Timer>();
        lvlChange = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelChange>();

        WinCount = 0;
        LoseCount = 0;
        nextLevelCheck = false;
        //RoundStart();
        playerGun.StartRound();
        BlackScreenAnim.Play("StartRound");
        gameObject.GetComponent<AmmoCount>().Reset();
        Gun.PlayerHealth = 100;
        Sıfırlama = false;
        
    }

    public void PlayerWin()
    {
        WinCount++;
        if(WinCount == 1)
        {
            Star1_Image.color = PlayerWinColor;
        }
        if (WinCount == 2)
        {
            Star1_Image.color = PlayerWinColor;
            Star2_Image.color = PlayerWinColor;
        }
    }
    public void EnemyWin()
    {
        if(!Sıfırlama)
        {
            LoseCount++;
            if (LoseCount == 1)
            {
                Star3_Image.color = EnemyWinColor;
            }
            if (LoseCount == 2)
            {
                Star3_Image.color = EnemyWinColor;
                Star2_Image.color = EnemyWinColor;
            }
            Invoke("RoundEnd", 1f);

            Sıfırlama = true;
        }       
    }


    public void RoundStart()
    {
        playerGun.StartRound();
        BlackScreenAnim.Play("StartRound");
        gameObject.GetComponent<AmmoCount>().Reset();
        EnemyScript.Roundstart();
        Gun.PlayerHealth = 100;
        Sıfırlama = false;
        if (!nextLevelCheck)
        {
            timer.StartCoroutine("TimerIE");
        }    
    }

    public void RoundEnd()
    {
        BlackScreenAnim.Play("EndRound");
        gStatus.RoundEnd();
        if (WinCount != 2)
        {
            Invoke("RoundStart", 2.5f);
        }
        if (WinCount == 2)
        {
            NextLevel();
        }
    }


    void NextLevel()
    {
        nextLevelCheck = true;
        PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
         lvlChange.Invoke( "Start",2f);
        Debug.Log(PlayerPrefs.GetInt("CurrentLevel") + " CurrentLevel");
        Invoke("RoundStart", 2.5f);
    }

    [ContextMenu("ResetLevel_Save")]
    void RestartLevelSave()
    {
        PlayerPrefs.SetInt("CurrentLevel",0);
        Debug.Log(PlayerPrefs.GetInt("CurrentLevel") + " CurrentLevel");
    }
}
