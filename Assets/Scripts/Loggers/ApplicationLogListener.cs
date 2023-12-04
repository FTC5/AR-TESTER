using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Loggers
{
    public class ApplicationLogListener : MonoBehaviour
    {
        void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        void HandleLog(string logString, string stackTrace, LogType type)
        {
            if(type == LogType.Warning)
            {
                Logger.Instance.LogWarning(logString);
            }
            else if(type != LogType.Log)
            {
                Logger.Instance.LogError(logString);
            }
        }
    }
}
