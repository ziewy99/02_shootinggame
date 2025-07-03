using UnityEngine;

public class Script_enemyBullet_0 : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // カメラ取得（画面外チェック用）
        mainCamera = Camera.main;

        Move();
    }

    void Update()
    {
        OutOfScreen();
    }

    void Move()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.down * speed;
        }
    }

    void OutOfScreen()//画面外になると消滅する
    {
        Vector3 screenPos = mainCamera.WorldToViewportPoint(transform.position);
        if (screenPos.y > 1.1f || screenPos.y < -0.1f || screenPos.x < -0.1f || screenPos.x > 1.1f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);       // 弾を消す
        }
    }
}
