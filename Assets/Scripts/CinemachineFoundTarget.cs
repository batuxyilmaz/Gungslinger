using UnityEngine;
using Cinemachine;

public class CinemachineFoundTarget : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    GameObject Bullet;

    public void GoldenBulletRelease()
    {
            Bullet = GameObject.FindGameObjectWithTag("Bullet");
            virtualCamera.Follow = Bullet.transform;
            virtualCamera.LookAt = Bullet.transform;
        
    }
}
