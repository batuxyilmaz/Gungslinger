using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenBullet : MonoBehaviour
{

    [Header("Cams")]
    public GameObject mainCamera;
    public GameObject trackCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera.SetActive(true);
        trackCamera.SetActive(false);
    }
    public void goldenBullet()
    {
        mainCamera.GetComponent<Camera>().enabled = false;
        trackCamera.SetActive(true);
        
    }

    public void goldenBulletEnd()
    {
        mainCamera.GetComponent<Camera>().enabled = true;
        trackCamera.SetActive(false);
    }
}
