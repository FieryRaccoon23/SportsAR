using System.Collections;
using UnityEngine;

public class DebugLines : MonoBehaviour
{
    string m_LogMessage;
    Queue m_LogQueue = new Queue();

    void Start()
    {
    }

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
        m_LogMessage = logString;
        string newString = "[" + type + "] : " + m_LogMessage + "\n";

        m_LogQueue.Enqueue(newString);

        if (type == LogType.Exception)
        {
            newString = "" + stackTrace;
            m_LogQueue.Enqueue(newString);
        }

        m_LogMessage = string.Empty;
        foreach (string log in m_LogQueue)
        {
            m_LogMessage += log;
        }
    }

    void OnGUI()
    {
        //GUILayout.Label(m_LogMessage);
    }
}
