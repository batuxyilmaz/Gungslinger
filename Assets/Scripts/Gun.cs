using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun Stats")]
    public int damage;
    public float GunAimSpeed;

    [Header("Bools")]
    public bool aiming;
 
    [Header("Aiming")]
    public GameObject CrossHair;
    public GameObject AimPosRef;
    public GameObject AimLookAt;

    [Header("Shooting")]
    public GameObject ShotParticleObject;
    ParticleSystem MuzzleParticle;
    TakeDamage enemyDamageScript;

    Camera mainCamera;

    [Header("GoldenBullet")]
    public GameObject Bullet;
    public Transform GoldenBulletSpawnLocation;
    public float BulletSpeed;
    public GameObject SceneManager;
    GoldenBullet goldenBullet;
    bool GoldenBulletBool;
    GameObject Enemy;
    bool GoldenBulletMove;

    GameStatus Gstatus;
    Timer timerScript;

    [Header("Cinemachine")]
    public GameObject TrackCamera;
    CinemachineFoundTarget TrackTarget;

    //MissClick
    public static int PlayerHealth;

    public Transform StartHandRef;
    

    void Start()
    {
        SceneManager = GameObject.FindGameObjectWithTag("SceneManager");
        MuzzleParticle = ShotParticleObject.GetComponent<ParticleSystem>();
        CrossHair = GameObject.FindGameObjectWithTag("CrossHair");
        CrossHair.SetActive(false);
        aiming = false;
        PlayerHealth = 100;

        TrackTarget = TrackCamera.GetComponent<CinemachineFoundTarget>();

        enemyDamageScript = GameObject.FindGameObjectWithTag("Enemy").GetComponent<TakeDamage>();
        if(enemyDamageScript == null)
        {
            Debug.LogError("Gun.cs enemyDamageScript == null");
            return;
        }

        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Gun.cs main camera == null");
            return;
        }

        Enemy = GameObject.FindGameObjectWithTag("EnemyHead");
        goldenBullet = SceneManager.GetComponent<GoldenBullet>();
        timerScript = SceneManager.GetComponent<Timer>();
        Gstatus = SceneManager.GetComponent<GameStatus>();

        GoldenBulletMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            if(timerScript.timerBool)
            {
                Aiming();
                aiming = true;
            }
            if(timerScript.timerBool == false)
            {  
                if(Input.GetKeyDown(KeyCode.Mouse0))
                {
                    EarlyClick();
                }               
            }
            
        }
        if(Input.GetKeyUp(KeyCode.Mouse0) && aiming)
        {
            if (SceneManager.GetComponent<AmmoCount>().AmmoLeft == false)
            {
                NoBulletLeft();
            }
            else
            {
                Shoot();
            }         
        }
        if(aiming)
        {
            transform.LookAt(AimLookAt.transform);
        }
        if(GoldenBulletBool)
        {            
            GoldenKill();
        }
    }

    void Aiming()
    {

        if (Gstatus.GameSituation == false)
        {
            return;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, AimPosRef.transform.position, Time.deltaTime * GunAimSpeed);
            transform.rotation = AimPosRef.transform.rotation;

            CrossHair.SetActive(true);
        }    
    }
    void Shoot()
    {
        if(Gstatus.GameSituation == false)
        {
            return;
        }
        RaycastHit hit;
        if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward,out hit))
        {
            if (hit.collider.CompareTag("EnemyHead"))
            {
                //GoldenKill
                if(1==1)
                {
                    goldenBullet.goldenBullet();
                    GoldenBulletBool = true;
                }
                else
                {
                    enemyDamageScript.GetComponent<TakeDamage>().takeDamage(damage * 2, true);
                }
                 
            }
            if (hit.collider.CompareTag("EnemyBody"))
            {
                enemyDamageScript.GetComponent<TakeDamage>().takeDamage(damage,false);
            }            
        }
        MuzzleParticle.Play();
        SceneManager.GetComponent<AmmoCount>().AmmoDecrease();
        CrossHair.SetActive(false);
        //Random Aim
        mainCamera.GetComponent<swiper>().Recoil();
    }

    void GoldenKill()
    {
        Instantiate(Bullet, GoldenBulletSpawnLocation.transform.position, GoldenBulletSpawnLocation.transform.rotation);
        Gstatus.RoundEnd();
        GoldenBulletBool = false;
        TrackTarget.GoldenBulletRelease();
    }

    void EarlyClick()
    {
        Debug.Log("EarlyClick - missclickOnce");
        PlayerTakeDamage(20);    
    }

    public void NoBulletLeft()
    {
        PlayerTakeDamage(100);
    }

    public void PlayerTakeDamage(int damage)
    {        
        PlayerHealth = PlayerHealth - damage;
        enemyDamageScript.HitBack();
        
        Debug.Log(PlayerHealth + "PlayerTakeDamage");
    }

    public void StartRound()
    {
        transform.position = StartHandRef.transform.position;
        transform.rotation = StartHandRef.transform.rotation;
    }



}
