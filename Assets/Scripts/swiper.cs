using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swiper : MonoBehaviour
{
    private Touch initTouch = new Touch();
    Camera mainCamera;
    private float rotX = 0f;
    private float rotY = 0f;
    private Vector3 origRot;

    [Header("Camera Stats")]
    public float rotSpeed = 0.5f;
    public float dir = -1;

    [Header("Camera Rotation")]
    public float RotXmin;
    public float RotXmax;
    public float RotYmin, RotYmax;

    [Header("RandomAim")]
    public float Xmin;
    public float Xmax;
    public float Ymin, Ymax;

    private float width;
    private float height;

    GameStatus gStatus;

    private void Awake()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;
        gStatus = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<GameStatus>();
        Recoil();
    }

    void Start()
    {
        mainCamera = Camera.main;
        origRot = mainCamera.transform.eulerAngles;
        rotX = origRot.x;
        rotY = origRot.y;
        rotX = -0.557f;
        rotY = -11.129f;
        

    }
    void Update()
    {
        if(gStatus.GameSituation)
        {
            if (Input.touchCount < 2)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        initTouch = touch;
                    }
                    else if (touch.phase == TouchPhase.Moved)
                    {
                        float deltaX = initTouch.position.x - touch.position.x;
                        float deltaY = initTouch.position.y - touch.position.y;

                        rotX -= deltaY * Time.deltaTime * rotSpeed * dir;
                        rotY += deltaX * Time.deltaTime * rotSpeed * dir;
                        mainCamera.transform.eulerAngles = new Vector3(rotX, rotY, 0f);
                        rotX = Mathf.Clamp(rotX, RotXmin, RotXmax);
                        rotY = Mathf.Clamp(rotY, RotYmin, RotYmax);
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        initTouch = new Touch();
                    }
                }
            }
        }
    }

    //Random Aim
    public void Recoil()
    {
        /*
        float X,Y;
        X = Random.Range(Xmin,Xmax);
        Y = Random.Range(Ymin,Ymax); 
       gameObject.transform.localEulerAngles = new Vector3(X, Y, 0);
        */
    }
}
