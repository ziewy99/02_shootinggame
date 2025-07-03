using UnityEngine;

public class hokori_Controller : MonoBehaviour
{
    public Transform childA;
    public Transform childB;
    [SerializeField] private float fallSpeed = 2.0f;

    private Vector3 startPosA;
    private Vector3 startPosB;

    void Start()
    {
        if (childA != null) startPosA = childA.position;
        if (childB != null) startPosB = childB.position;
    }

    void Update()
    {
        // 落下処理
        if (childA != null)
        {
            childA.position += Vector3.down * fallSpeed * Time.deltaTime;
        }

        if (childB != null)
        {
            childB.position += Vector3.down * fallSpeed * Time.deltaTime;
        }

        // 入れ替え判定：childAがstartPosBまで来たらスワップ
        if (childA != null && childB != null)
        {
            if (childA.position.y <= startPosB.y)
            {
                // 座標をスワップ
                childA.position = startPosA;
                childB.position = startPosB;
            }
        }
    }
}
