using UnityEngine;
using UnityEngine.SceneManagement;

public class title_Controller : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
