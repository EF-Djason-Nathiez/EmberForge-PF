using System;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;
using Debug = UnityEngine.Debug;

public static class Logs
{
    public enum LogColor
    {
        None,
        Red,
        Blue,
        Green,
        Yellow,
        Black
    }

    [Conditional("DEBUG")]
    public static void Log(string title, string log, LogType logType = LogType.Log, LogColor titleColor = LogColor.None, LogColor logColor = LogColor.None)
    {
        if(!DebugManager.instance.ActiveLogs) return;
        
        title = $"<b>[{title}]</b>";

        if (titleColor != LogColor.None)
        {
            title = $"<color={GetColorByLogColor(titleColor)}>{title}</color>";
        }

        if (logColor != LogColor.None)
        {
            log = $"<color={GetColorByLogColor(logColor)}>{log}</color>";
        }
        
        Debug.unityLogger.Log(logType, $"{title} -- {log}");
    }
    
    [Conditional("DEBUG")]
    public static void Log(string title, string log, LogType logType = LogType.Log, LogColor logColor = LogColor.None)
    {
        if(!DebugManager.instance.ActiveLogs) return;
        
        title = $"<b>[{title}]</b>";
        
        if (logColor != LogColor.None)
        {
            log = $"<color={GetColorByLogColor(logColor)}>{log}</color>";
        }
        
        Debug.unityLogger.Log(logType, $"{title} -- {log}");
    }

    private static string GetColorByLogColor(LogColor logColor)
    {
        return logColor switch
        {
            LogColor.None => "",
            LogColor.Red => "#D45656",
            LogColor.Blue => "#71A7C2",
            LogColor.Yellow =>"#F1C232",
            LogColor.Green => "#8FCE00",
            LogColor.Black => "#000000",
            _ => throw new ArgumentOutOfRangeException(nameof(logColor), logColor, null)
        };
    }

    public static void Log(string title, string log)
    {
        if(!DebugManager.instance.ActiveLogs) return;
        
        title = $"<b>[{title}]</b>";

        LogType logType = LogType.Log;
        LogColor logColor = LogColor.None;
        
        if (logColor != LogColor.None)
        {
            log = $"<color={GetColorByLogColor(logColor)}>{log}</color>";
        }
        
        Debug.unityLogger.Log(logType, $"{title} -- {log}");
        
    }

    public static void Log(Type t, MethodBase mb, string log)
    {
        if(!DebugManager.instance.ActiveLogs) return;
        
        string title = $"<b>[{t.FullName} - {mb.Name}]</b>";

        LogType logType = LogType.Log;
        LogColor logColor = LogColor.None;
        
        if (logColor != LogColor.None)
        {
            log = $"<color={GetColorByLogColor(logColor)}>{log}</color>";
        }
        
        Debug.unityLogger.Log(logType, $"{title} -- {log}");
    }

    public static void Log(string log)
    {
        if(!DebugManager.instance.ActiveLogs) return;
        
        string title = $"<b>[DebugLog]</b>";

        LogType logType = LogType.Log;
        LogColor logColor = LogColor.None;
        
        if (logColor != LogColor.None)
        {
            log = $"<color={GetColorByLogColor(logColor)}>{log}</color>";
        }
        
        Debug.unityLogger.Log(logType, $"{title} -- {log}");
    }
}