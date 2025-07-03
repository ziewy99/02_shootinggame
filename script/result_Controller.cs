using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class result_Controller : MonoBehaviour
{
    public TextMeshProUGUI resultScoreText;

    void Start()
    {
        scoreManager scoremanager = GameObject.Find("scoreManager").GetComponent<scoreManager>();
        if (resultScoreText != null)
        {
            int score = scoremanager.score;
            resultScoreText.text = "Score:" + score.ToString();
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
