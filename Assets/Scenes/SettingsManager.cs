using UnityEngine;
using UnityEngine.UI;
using System;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public Toggle darkModeToggle;
    public Toggle displayCalendarToggle;
    public Toggle displayTimeToggle;
    public Text calendarDisplay;
    public Text timeDisplay;

    private void Start()
    {
        settingsPanel.SetActive(false);

        LoadSettings();

        InvokeRepeating("UpdateTimeDisplay", 0, 1);
    }

    public void ToggleSettingsPanel()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void OnDarkModeToggleChanged()
    {
        if (darkModeToggle.isOn)
        {
            EnableDarkMode();
        }
        else
        {
            DisableDarkMode();
        }
        SaveSettings();
    }

    public void OnDisplayCalendarToggleChanged()
    {
        calendarDisplay.gameObject.SetActive(displayCalendarToggle.isOn);
        if (displayCalendarToggle.isOn)
        {
            UpdateCalendarDisplay();
        }
        SaveSettings();
    }

    public void OnDisplayTimeToggleChanged()
    {
        timeDisplay.gameObject.SetActive(displayTimeToggle.isOn);
        SaveSettings();
    }

    private void EnableDarkMode()
    {
        // Implement dark mode enable logic, e.g., inverting colors
        InvertColors();
    }

    private void DisableDarkMode()
    {
        // Implement dark mode disable logic, e.g., reverting colors
        InvertColors();
    }

    private void InvertColors()
    {
        // Example: invert the background color of the camera
        Camera.main.backgroundColor = new Color(1 - Camera.main.backgroundColor.r, 1 - Camera.main.backgroundColor.g, 1 - Camera.main.backgroundColor.b);

        // Add logic to invert colors of other UI elements
        Image[] images = FindObjectsOfType<Image>();
        foreach (Image img in images)
        {
            img.color = new Color(1 - img.color.r, 1 - img.color.g, 1 - img.color.b);
        }

        Text[] texts = FindObjectsOfType<Text>();
        foreach (Text txt in texts)
        {
            txt.color = new Color(1 - txt.color.r, 1 - txt.color.g, 1 - txt.color.b);
        }
    }

    private void UpdateCalendarDisplay()
    {
        calendarDisplay.text = DateTime.Now.ToString("MMMM dd, yyyy");
    }

    private void UpdateTimeDisplay()
    {
        if (displayTimeToggle.isOn)
        {
            timeDisplay.text = DateTime.Now.ToString("HH:mm:ss");
        }
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt("DarkMode", darkModeToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("DisplayCalendar", displayCalendarToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("DisplayTime", displayTimeToggle.isOn ? 1 : 0);
    }

    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey("DarkMode"))
        {
            bool isDarkMode = PlayerPrefs.GetInt("DarkMode") == 1;
            darkModeToggle.isOn = isDarkMode;
            if (isDarkMode)
            {
                EnableDarkMode();
            }
            else
            {
                DisableDarkMode();
            }
        }

        if (PlayerPrefs.HasKey("DisplayCalendar"))
        {
            bool displayCalendar = PlayerPrefs.GetInt("DisplayCalendar") == 1;
            displayCalendarToggle.isOn = displayCalendar;
            calendarDisplay.gameObject.SetActive(displayCalendar);
            if (displayCalendar)
            {
                UpdateCalendarDisplay();
            }
        }

        if (PlayerPrefs.HasKey("DisplayTime"))
        {
            bool displayTime = PlayerPrefs.GetInt("DisplayTime") == 1;
            displayTimeToggle.isOn = displayTime;
            timeDisplay.gameObject.SetActive(displayTime);
        }
    }
}
