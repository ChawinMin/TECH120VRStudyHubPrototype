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

    private bool isDarkMode = false;
    private Color lightModeBackgroundColor = Color.white;
    private Color darkModeBackgroundColor = Color.black;
    private Color lightModeTextColor = Color.black;
    private Color darkModeTextColor = Color.white;

    private void Start()
    {
        settingsPanel.SetActive(false);
        calendarDisplay.gameObject.SetActive(false);
        timeDisplay.gameObject.SetActive(false);

        LoadSettings();

        InvokeRepeating("UpdateTimeDisplay", 0, 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ToggleSettingsPanel();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            ToggleDarkMode();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCalendarDisplay();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleTimeDisplay();
        }
    }

    public void ToggleSettingsPanel()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void ToggleDarkMode()
    {
        isDarkMode = !isDarkMode;
        darkModeToggle.isOn = isDarkMode;
        OnDarkModeToggleChanged();
    }

    public void ToggleCalendarDisplay()
    {
        bool isActive = !displayCalendarToggle.isOn;
        displayCalendarToggle.isOn = isActive;
        OnDisplayCalendarToggleChanged();
    }

    public void ToggleTimeDisplay()
    {
        bool isActive = !displayTimeToggle.isOn;
        displayTimeToggle.isOn = isActive;
        OnDisplayTimeToggleChanged();
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
        bool isActive = displayCalendarToggle.isOn;
        calendarDisplay.gameObject.SetActive(isActive);
        if (isActive)
        {
            UpdateCalendarDisplay();
        }
        SaveSettings();
    }

    public void OnDisplayTimeToggleChanged()
    {
        bool isActive = displayTimeToggle.isOn;
        timeDisplay.gameObject.SetActive(isActive);
        SaveSettings();
    }

    private void EnableDarkMode()
    {
        SetColors(darkModeBackgroundColor, darkModeTextColor);
    }

    private void DisableDarkMode()
    {
        SetColors(lightModeBackgroundColor, lightModeTextColor);
    }

    private void SetColors(Color backgroundColor, Color textColor)
    {
        Camera.main.backgroundColor = backgroundColor;

        Image[] images = FindObjectsOfType<Image>();
        foreach (Image img in images)
        {
            img.color = backgroundColor;
        }

        Text[] texts = FindObjectsOfType<Text>();
        foreach (Text txt in texts)
        {
            txt.color = textColor;
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
        PlayerPrefs.SetInt("DarkMode", isDarkMode ? 1 : 0);
        PlayerPrefs.SetInt("DisplayCalendar", displayCalendarToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("DisplayTime", displayTimeToggle.isOn ? 1 : 0);
    }

    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey("DarkMode"))
        {
            isDarkMode = PlayerPrefs.GetInt("DarkMode") == 1;
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
