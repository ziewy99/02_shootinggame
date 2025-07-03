using UnityEngine;

public class enemy0_Script : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    private Rigidbody2D rb;

    private float moveY = 0;
    private float lowerBoundY;

    public GameObject bomb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lowerBoundY = GetCameraBottomY();


    }

    void Update()
    {
        Move();
        if (IsOutOfScreenBelow(0.5f))
        {
            Destroy(gameObject);
        }
    }
    void Move()
    {
        //移動
        moveY = -1.0f;
        rb.linearVelocity = new Vector2(0, moveY * moveSpeed);
    }

    // カメラの下端Y座標を取得
    float GetCameraBottomY()
    {
        Camera cam = Camera.main;
        if (cam != null)
        {
            return cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        }
        return -10f; // カメラ取得に失敗した場合のデフォルト値
    }

    //画面下（指定マージン分）を超えたかどうか
    bool IsOutOfScreenBelow(float margin)
    {
        return transform.position.y < (lowerBoundY - margin);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Bullet")) 
        {
            if (soundManager.Instance != null) soundManager.Instance.Sound_Play("boom");
            Instantiate(bomb, transform.position, Quaternion.identity);
            Destroy(gameObject);
            if (scoreManager.instance != null)
            {
                scoreManager.instance.AddScore(50);
            }
        }
    }
}
