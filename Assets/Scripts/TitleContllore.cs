using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    public void StartGame()
    {
        // 💡「MainScene」の部分を、あなたの実際のゲームステージのシーン名（例：MainScene）に変えてください
        SceneManager.LoadScene("Main");
    }
}