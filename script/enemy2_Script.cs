using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class enemy2_Script : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    private Rigidbody2D rb;
    private Animator animator;

    public GameObject bomb;
    public GameObject bulletPrefab;

    public int hp = 15;

    private Vector2 maxBounds;
    private Vector2 minBounds;
    private enum MoveState { MovingDown, MovingSide };
    private MoveState currentState = MoveState.MovingDown;
    private bool movingRight = false;

    private bool isFiring = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetCameraBounds();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        Move();
    }
    void Move()
    {
        Vector2 pos = transform.position;

        if (currentState == MoveState.MovingDown)
        {
            // Y方向に下がる
            if (pos.y > maxBounds.y - 2.0f)
            {
                rb.linearVelocity = new Vector2(0, -moveSpeed);
            }
            else
            {
                currentState = MoveState.MovingSide;
                movingRight = false;    // 中央からスタート、最初は左に向かう
                if (!isFiring)
                {
                    StartCoroutine(FireBulletRoutine());
                    isFiring = true;
                }
            }
        }
        else if (currentState == MoveState.MovingSide)
        {
            float moveX = movingRight ? 1 : -1;
            rb.linearVelocity = new Vector2(moveX * moveSpeed, 0);

            // 左端 or 右端 に到達したら反転
            if (pos.x <= minBounds.x + 1.0f)
            {
                movingRight = true;
            }
            else if (pos.x >= maxBounds.x - 1.0f)
            {
                movingRight = false;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Bullet"))
        {
            hp--;
            Destroy(other.gameObject);
            if (animator != null)
            {
                animator.SetBool("Damaged", true);
            }
            if (hp < 0)
            {
                if (soundManager.Instance != null) soundManager.Instance.Sound_Play("boom");
                Instantiate(bomb, transform.position, Quaternion.identity);
                Destroy(gameObject);
                if (scoreManager.instance != null)
                {
                    scoreManager.instance.AddScore(1000);
                }
                SceneManager.LoadScene("ResultScene");
            }
        }
    }

    void GetCameraBounds()
    {
        // カメラの境界を取得
        Camera cam = Camera.main;
        if (cam != null)
        {
            maxBounds = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));
            minBounds = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        }
    }

    void FireBullet()
    {
        if (bulletPrefab != null)
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, -1.0f, 0), Quaternion.identity);
        }
    }

    IEnumerator FireBulletRoutine()
    {
        while (currentState == MoveState.MovingSide)
        {
            FireBullet();
            yield return new WaitForSeconds(1.0f);
        }
    }
    public void SetAnimationIdle()
    {
        animator.SetBool("Damaged", false);
    }
}
