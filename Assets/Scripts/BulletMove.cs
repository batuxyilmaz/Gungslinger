using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{

    GameObject Enemy;

    public float BulletSpeed;

    private void Start()
    {
        Enemy = GameObject.FindGameObjectWithTag("EnemyHead");
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Enemy.transform.position, Time.deltaTime * BulletSpeed);
    }
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
