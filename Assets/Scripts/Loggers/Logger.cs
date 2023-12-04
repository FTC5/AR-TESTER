using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class Logger : Singleton<Logger>
{ 
    public enum LogLevel
    {
        Info, Warn, Error
    }
    [SerializeField]
    private TextMeshProUGUI debugAreaText;
    [SerializeField]
    private bool enableDebug = false;
    [SerializeField]
    private int maxLines = 15;
    [SerializeField]
    private LogLevel[] logLevels = new LogLevel[] { LogLevel.Warn, LogLevel.Error };
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
        logMessage($"{DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss")}<color=\"white\">{message}</color>\n", LogLevel.Info);
    }

    public void LogError(string message)
    {
        logMessage($"{DateTime.Now.ToString("yyyy-dd-M--HH-am-ss")}<color=\"red\">{message}</color>\n", LogLevel.Warn);
    }

    public void LogWarning(string message)
    {
        logMessage($"{DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss")}<color=\"yellow\">{message}</color>\n", LogLevel.Error);
    }

    private void logMessage(string formatMessage, LogLevel logLevel)
    {
        if(logLevels.Contains(logLevel)) 
        {
            return;
        }

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