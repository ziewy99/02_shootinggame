using UnityEngine;

public class Script_enemyBullet_0 : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // �J�����擾�i��ʊO�`�F�b�N�p�j
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

    void OutOfScreen()//��ʊO�ɂȂ�Ə��ł���
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
            Destroy(gameObject);       // �e������
        }
    }
}
