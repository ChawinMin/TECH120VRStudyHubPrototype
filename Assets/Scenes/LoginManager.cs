using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    // public InputField usernameInput;
    // public InputField passwordInput;
    // public Text statusText;

    // private string correctUsername = "user";
    // private string correctPassword = "password";

    public void OnLoginButtonClicked()
    {
        // string username = usernameInput.text;
        // string password = passwordInput.text;

        // if (username == correctUsername && password == correctPassword)
        // {
        //     SceneManager.LoadScene("Scenes/Main"); 
        // }
        // else
        // {
        //     statusText.text = "Invalid username or password.";
        // }
        SceneManager.LoadScene("Scenes/Main"); 
    }
}
