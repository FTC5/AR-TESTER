using LiteDB;
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
                
                
                using (var db = new LiteDatabase(@$"{Application.persistentDataPath}/ARObject.db"))
                {
                    var ARGameObjects = db.GetCollection<ErrorLog>("ErrorLog");
                    ARGameObjects.Insert(new ErrorLog(logString, stackTrace));
                }
            }
        }
    }

    public class ErrorLog
    {
        public string Message { get; set; }
        public string stackTrace { get; set; }
        [BsonId]
        public Guid ID { get; set; }
        public ErrorLog()
        {
            ID = Guid.NewGuid();
        }

        public ErrorLog(string message, string stackTrace)
        {
            ID = Guid.NewGuid();
            Message = message;
            this.stackTrace = stackTrace;
        }
    }
}
