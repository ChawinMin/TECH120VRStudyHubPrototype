using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValidLogin : MonoBehaviour
{
    public InputField emailInputField;  // Assign this in the Unity Editor
    public Text errorMessage;           // Assign this in the Unity Editor

    // Start is called before the first frame update
    void Start()
    {
        errorMessage.text = "";  // Clear the error message at the start
    }

    // This function is called when the user clicks the login button
    public void ValidateEmail()
    {
        string email = emailInputField.text;

        if (email.EndsWith("@purdue.edu"))
        {
            // If the email is valid, clear the error message and proceed with the login
            errorMessage.text = "";
            FindObjectOfType<LoginManager>().OnLoginButtonClicked();
        }
        else
        {
            // If the email is invalid, display an error message
            errorMessage.text = "Invalid email!";
        }
    }
}

