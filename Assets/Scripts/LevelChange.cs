using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChange : MonoBehaviour
{
    public GameObject[] Levels;

    public int ChangeCurrentLevelCount;
    // Start is called before the first frame update
    public void Start()
    {
        for (int i = 0; i < Levels.Length; i++)
        {
            Levels[i].SetActive(false);
        }
        if(PlayerPrefs.GetInt("CurrentLevel") - 1> Levels.Length)
        {
            PlayerPrefs.SetInt("CurrentLevel", 0);
            Levels[PlayerPrefs.GetInt("CurrentLevel")].SetActive(true);
        }
        else
        {
            Levels[PlayerPrefs.GetInt("CurrentLevel")].SetActive(true);
        }
        
    }

    [ContextMenu("ChangeCurrentLevel")]
    void ChangeCurrentLevel()
    {
        PlayerPrefs.SetInt("CurrentLevel", ChangeCurrentLevelCount);
        Debug.Log(PlayerPrefs.GetInt("CurrentLevel") + " CurrentLevel");
    }
}
