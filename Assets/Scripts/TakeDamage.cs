using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class TakeDamage : MonoBehaviour
{
    [Tooltip("Enemy Health")]
    public int health = 100;
    int starthealth;
    public Animator enemyAnimation;
    public GameObject PistolObject;    
    int x = 0;

    [Header("Rebirth")]
    [Tooltip("Enemy Spawn Position")]
    public GameObject RebirthPos;

    RigBuilder rigBuilder;

    GameObject sceneManager;
    GameStatus gStatus;
    Round roundScript;

    [Tooltip("Enemy Health image (Red Hearth Image)")]
    public Image EnemyHealthImage;

    float EnemyHealthImageNumber;

    [Header("HitBack")]
    public Transform AimPosRef;
    public ParticleSystem EnemyGunMuzzle;
    public Animator PlayerDamageEffect;


    private void Start()
    {
        rigBuilder = GetComponent<RigBuilder>();
        starthealth = health;
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager");
        gStatus = sceneManager.GetComponent<GameStatus>();
        roundScript = sceneManager.GetComponent<Round>();

        EnemyHealthImage.fillAmount = 1;
        
    }
    public void takeDamage(int damage,bool head)
    {
        enemyAnimation.SetBool("Rebirth", false);
        health = health - damage;

        

        if (health <= 0 )
        {
            Die(head);
            EnemyHealthImage.fillAmount = 0;
        }
        if(health > 0)
        {
            enemyAnimation.SetBool("Hit", true);
            EnemyHealthImage.fillAmount = ((1.0f) / (starthealth / health));

        }       
    }
    
    void Die(bool HeadShot)
    {
        
        if (HeadShot)
        {
            x = Random.Range(1, 3);
            enemyAnimation.SetBool("Die", true);
            enemyAnimation.SetInteger("Head", x);
        }
        if(!HeadShot)
        {
            x = Random.Range(1, 3);
            enemyAnimation.SetBool("Die", true);
            enemyAnimation.SetInteger("Body", x);
        }

        PistolObject.SetActive(false);
        rigBuilder.enabled = false;
        Debug.Log("Düşman öldü");
        EnemyHealthImage.fillAmount = 0;
        gStatus.RoundEnd();
        Invoke("rebirth", 3.8f);
    }

    #region Golden_Bullet

    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.tag == "Bullet")
        {
            enemyAnimation.SetBool("Rebirth", false);
            x = Random.Range(1, 3);
            enemyAnimation.SetBool("Die", true);
            enemyAnimation.SetInteger("Head", 1);
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            other.gameObject.GetComponent<BulletMove>().Invoke("DestroyBullet", 4f);
            PistolObject.SetActive(false);
            rigBuilder.enabled = false;
            EnemyHealthImage.fillAmount = 0;
            //sceneManager.GetComponent<GoldenBullet>().goldenBulletEnd();            
            Invoke("rebirth", 3.8f);
            sceneManager.GetComponent<GoldenBullet>().Invoke("goldenBulletEnd", 5.5f);
        }
      
    }


    #endregion

    void rebirth()
    {
        
        roundScript.PlayerWin();
        roundScript.RoundEnd();    
        Invoke("EnemyReady", 2f);
    }

    public void EnemyReady()
    {
        enemyAnimation.SetBool("Rebirth", true);
    }

    public void Roundstart()
    {
        transform.position = RebirthPos.transform.position;
        transform.rotation = RebirthPos.transform.rotation;
        enemyAnimation.SetInteger("Head", 0);
        enemyAnimation.SetInteger("Body", 0);
        EnemyHealthImage.fillAmount = 1;
        enemyAnimation.SetBool("Die", false);
        enemyAnimation.SetBool("Hit", false);
        health = starthealth;
        PistolObject.SetActive(true);
        rigBuilder.enabled = true;
    }

    #region HitBack

    public void HitBack()
    {

        if(Gun.PlayerHealth > 0)
        {
            PistolObject.transform.position = AimPosRef.transform.position;
            PistolObject.transform.rotation = AimPosRef.transform.rotation;
            EnemyGunMuzzle.Play();
            PlayerDamageEffect.SetTrigger("TakeDamage");
        }
        

        if(Gun.PlayerHealth <= 0)
        {
            roundScript.EnemyWin();
        }
    }
    #endregion

}
