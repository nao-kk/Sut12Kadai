using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour
{
    public void GoToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
