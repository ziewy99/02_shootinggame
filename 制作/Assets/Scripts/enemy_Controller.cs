using UnityEngine;
using System.Collections.Generic;

public class enemyController : MonoBehaviour
{
    public GameObject enemy0Prefab;
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;

    private float timer = 0f;
    private float totalTime = 0f;
    private bool enemy2Spawned = false;

    private Vector2 minBounds;
    private Vector2 maxBounds;

    void Start()
    {
        GetCameraBounds();
    }

    void Update()
    {
        timer += Time.deltaTime;
        totalTime += Time.deltaTime;

        if (timer >= 2.5f)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    void SpawnEnemyGroup(GameObject prefab, Vector2 spawnPosition, bool is_positionL)
    {
        float spacing = 1.2f;

        for (int i = 0; i < 5; i++)
        {
            Vector2 pos = new Vector2(spawnPosition.x, spawnPosition.y + i * spacing);
            GameObject enemy = Instantiate(prefab, pos, Quaternion.identity);

            if (prefab.name == "enemy1") 
            {
                enemy1_Script script = enemy.GetComponent<enemy1_Script>();
                if (script != null)
                {
                    script.moveType = is_positionL ? enemy1_Script.MoveType.Left : enemy1_Script.MoveType.Right;
                }
            }
        }
    }
    void SpawnEnemy()
    {
        //画面上部のY位置（画面外の少し上）
        float spawnY = maxBounds.y + 1.0f;
        if (totalTime < 25.0f)
        {
            float spawnX = (Random.Range(0, 2) == 0) ? -2.0f : 2.0f;
            Vector2 spawnPos = new Vector2(spawnX, spawnY);
            // 敵の種類（0か1）
            int enemyType = Random.Range(0, 2);

            bool isLeft = (spawnX == -2.0f) ? true : false;

            if (enemyType == 0)
            {
                SpawnEnemyGroup(enemy0Prefab, spawnPos, isLeft);
            }
            else
            {
                SpawnEnemyGroup(enemy1Prefab, spawnPos, isLeft);
            }
        }
        else if (!enemy2Spawned) 
        {
            // enemy2 を中央に1体出現
            Vector2 spawnPos = new Vector2(0f, spawnY);
            Instantiate(enemy2Prefab, spawnPos, Quaternion.identity);
            enemy2Spawned = true;
        }

    }


    void GetCameraBounds()
    {
        // カメラの境界を取得
        Camera cam = Camera.main;
        if (cam != null)
        {
            minBounds = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
            maxBounds = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));
        }
    }
}
