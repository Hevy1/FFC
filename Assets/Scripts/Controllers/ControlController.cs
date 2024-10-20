using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsController : MonoBehaviour
{
    public void MenuDisplay()
    {
        SceneManager.LoadScene("MenuScene");
    }
}