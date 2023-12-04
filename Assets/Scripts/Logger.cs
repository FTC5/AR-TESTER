using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class Logger : Singleton<Logger>
{ 
    [SerializeField]
    private TextMeshProUGUI debugAreaText;
    [SerializeField]
    private bool enableDebug = false;
    [SerializeField]
    private int maxLines = 15;
    private static Logger _instance;

    private Logger()
    {
        Instance = this;
    }


    void OnEnable()
    {
        debugAreaText.enabled = enableDebug;
        gameObject.active = enableDebug;
    }

    public void LogInfo(string message)
    {
        logMessage($"{DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss")}<color=\"white\">{message}</color>\n");
    }

    public void LogError(string message)
    {
        logMessage($"{DateTime.Now.ToString("yyyy-dd-M--HH-am-ss")}<color=\"red\">{message}</color>\n");
    }

    public void LogWarning(string message)
    {
        logMessage($"{DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss")}<color=\"yellow\">{message}</color>\n");
    }

    private void logMessage(string formatMessage)
    {
        if(enableDebug && debugAreaText != null)
        {
            clearLines();
            debugAreaText.text += formatMessage;
        }
    }
    private void clearLines()
    {
        if (debugAreaText.text.Split('\n').Count() >= maxLines)
        {
            debugAreaText.text = string.Empty;
        }
    }
}