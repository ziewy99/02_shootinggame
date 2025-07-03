using UnityEngine;

public class camera_Controller : MonoBehaviour
{
    void Start()
    {
        // カメラの表示サイズを縦型シューティング（Raiden風）に設定
        Camera.main.orthographicSize = 5f;

        // 解像度を768×1024（3:4縦長）に設定（ウィンドウモード）
        Screen.SetResolution(768, 1024, false);
    }
}
