using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float BombTime;
    float timer;
    public GameObject Dynamite;
    public Text TimerText;
    Animator BombAnimator;
    public bool timerBool;
    GameStatus gStatus;

    private void Start()
    {
        BombAnimator = Dynamite.GetComponent<Animator>();
        gStatus = GetComponent<GameStatus>();
        Dynamite.SetActive(true);
        StartCoroutine("TimerIE");
    }
    private void Update()
    {
        if(Dynamite.activeInHierarchy)
        {
            timer -= Time.deltaTime;
            TimerText.text = timer.ToString("0");
        }
    }
    
    public IEnumerator TimerIE()
    {
        
        timer = BombTime;       
        timerBool = false;
        gStatus.RoundEnd();
        Bomb();
        yield return new WaitForSeconds(BombTime);
        gStatus.RoundStart();
        Dynamite.SetActive(false);
        timerBool = true;
        
    }

    public void Bomb()
    {
        if (!timerBool)
        {
            if (Dynamite.activeInHierarchy == false)
            {
                Dynamite.SetActive(true);
            } 
            BombAnimator.SetTrigger("Bomb");
        }
    }
}
