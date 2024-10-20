using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ControlsDisplay()
    {
        SceneManager.LoadScene("ControlsScene");
    }

    public void CreditDisplay()
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
