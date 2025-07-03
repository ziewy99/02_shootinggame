// Script_bullet_0.cs
using UnityEngine;
using UnityEngine.EventSystems;

public class Script_bullet_0 : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 10f;
    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // アニメーション再生
        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            anim.Play("shoot_0");
        }
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
            rb.linearVelocity = Vector2.up * speed;
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
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);       // 弾を消す
        }
    }
}
