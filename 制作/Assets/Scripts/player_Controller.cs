using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player_Controller : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 minBounds;
    private Vector2 maxBounds;
    private float halfWidth;
    private float halfHeight;
    private bool isDead;

    public GameObject bomb;

    // 残機数
    public int life = 3;
    public Text lifeText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        GetCameraBounds();
        GetPlayerSize();
        isDead = false;
        UpdateLifeText();
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            Move();
        }
    }
    void Move()
    {
        //移動
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        rb.linearVelocity = new Vector2(moveX * moveSpeed, moveY * moveSpeed);
        //動画
        animator.SetFloat("MoveX", moveX);
        // 位置制限
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        pos.y = Mathf.Clamp(pos.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        transform.position = pos;
    }
    void GetCameraBounds()//カメラの境界を取得
    {
        Camera cam = Camera.main;
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));
        minBounds = bottomLeft;
        maxBounds = topRight;
    }
    void GetPlayerSize()//プレイヤーのサイズを取得
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            halfWidth = sr.bounds.extents.x;
            halfHeight = sr.bounds.extents.y;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
        {
            StartCoroutine(DieAndRespawn());
        }
    }
    public bool IsDead()
    {
        return isDead;
    }
    // 残機表示を更新するメソッド
    void UpdateLifeText()
    {
        if (lifeText != null)
        {
            if (life > 0) lifeText.text = "残機数: " + (life - 1);
            if (life <= 0) lifeText.text = "残機数: " + 0;
        }
    }
    IEnumerator DieAndRespawn()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;

        if (soundManager.Instance != null) soundManager.Instance.Sound_Play("boom");

        // 爆発エフェクトを出す
        Instantiate(bomb, transform.position, Quaternion.identity);

        // プレイヤーを非表示にして操作禁止
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        animator.enabled = false;

        life--;
        UpdateLifeText();

        if (life > 0)
        {
            // 1.5秒後に復活処理
            yield return new WaitForSeconds(1.5f);

            // 位置を初期位置に戻す
            transform.position = new Vector3(0f, -3.5f, 0f);

            // 表示・操作を戻す
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
            animator.enabled = true;

            isDead = false;
        }
        else
        {
            // 残機なしならゲームオーバー
            Destroy(gameObject);
            Debug.Log("ゲームオーバー");
            SceneManager.LoadScene("ResultScene");
        }
    }
}
