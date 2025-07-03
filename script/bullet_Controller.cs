using System.Collections.Generic;
using UnityEngine;

public class bullet_Controller : MonoBehaviour
{
    public GameObject[] bulletPrefabs;
    public GameObject player;
    private Transform firePoint;
    public float bulletSpeed = 10f;
    private int currentBulletIndex = 0;

    private List<GameObject> activeBullets = new List<GameObject>();

    // 連射間隔（秒）
    public float fireInterval = 0.2f;
    private float fireTimer = 0f;

    private player_Controller playerScript;

    void Start()
    {
        Vector3 firePosition = player.transform.position + new Vector3(0, 0.5f, 0);
        GameObject firePointObj = new GameObject("FirePoint");
        firePointObj.transform.position = firePosition;
        firePointObj.transform.parent = player.transform;
        firePoint = firePointObj.transform;

        playerScript = player.GetComponent<player_Controller>();
    }

    void Update()
    {
        if (playerScript != null && playerScript.IsDead()) return;
        // スペースキー押している間、一定間隔で発射
        if (Input.GetKey(KeyCode.Space))
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireInterval)
            {
                FireBullet();
                fireTimer = 0f;
            }
        }
        else
        {
            fireTimer = fireInterval; // キー離したらすぐ撃てるようにリセット
        }
        // 管理している弾が消滅しているかチェックし、リストから削除（nullになってるもの）
        for (int i = activeBullets.Count - 1; i >= 0; i--)
        {
            if (activeBullets[i] == null)
            {
                activeBullets.RemoveAt(i);
            }
        }
    }
    void FireBullet()
    {
        if (bulletPrefabs.Length == 0) return;

        GameObject currentBullet = bulletPrefabs[currentBulletIndex];
        GameObject bullet = Instantiate(currentBullet, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = firePoint.right * bulletSpeed;
        }
        activeBullets.Add(bullet);
        if (soundManager.Instance != null) soundManager.Instance.Sound_Play("shoot");
    }
}
