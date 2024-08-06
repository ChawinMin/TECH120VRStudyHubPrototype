using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public Toggle tutorialToggle;
    
    private void Start() {
        tutorialToggle.isOn = false;
    }

    public void OnLoginButtonClicked()
    {
        if (tutorialToggle.isOn)
        {
            SceneManager.LoadScene("Scenes/Tutorial");
        }
        else
        {
            SceneManager.LoadScene("Scenes/Main");
        }
    }
}