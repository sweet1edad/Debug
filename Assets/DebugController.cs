using System;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    public bool ActiveDebugging { get; private set; }

    private Rect _windowRect = new Rect(20, 20, 120, 50);
    private bool _showConsole;

    private Dictionary<Type, ITabTick> _debugTabs;
    private ITabTick _currentDebugTab;

    public static Action<string> OnError;

    private void Awake()
    {
        _debugTabs = new Dictionary<Type, ITabTick>()
        {
            {typeof(ConsoleTab), new ConsoleTab()},
            {typeof(LoggerTab), new LoggerTab()},
        };
        SwitchTab<ConsoleTab>();

        Application.logMessageReceived += LogCallback;
    }

    public void SetActiveDebug(bool value)
    {
        ActiveDebugging = value;

        if (value)
        {
            return;
        }

        _showConsole = value;
    }

    public void AddDebugCommand(DebugCommandBase debugCommandBase)
    {
        ConsoleTab consoleTab = _debugTabs[typeof(ConsoleTab)] as ConsoleTab;
        consoleTab.CommandList.Add(debugCommandBase);
    }

    private void LogCallback(string condition, string stacktrace, LogType type)
    {
        LoggerTab loggerTab = _debugTabs[typeof(LoggerTab)] as LoggerTab;
        loggerTab.AppendLog($"{DateTime.UtcNow} {condition} \n");

        if (type == LogType.Error || type == LogType.Exception)
        {
            OnError?.Invoke(loggerTab.TextLogger);
        }
    }

    void DoMyWindow(int windowID)
    {
        GUI.DragWindow(new Rect(0, 0, 10000, 20));

        if (GUI.Button(new Rect(10, 20, 100, 20), "off/on"))
        {
            _showConsole = !_showConsole;
        }
    }

    private void OnGUI()
    {
        if (ActiveDebugging == false)
        {
            return;
        }

        _windowRect = GUI.Window(0, _windowRect, DoMyWindow, "Debug");

        if (_showConsole == false)
        {
            return;
        }

        GUILayout.BeginArea(new Rect(0, 0, Screen.width / 2, Screen.height * 0.1f));
        if (GUILayout.Button("CONSOLE", GUILayout.ExpandHeight(true)))
        {
            SwitchTab<ConsoleTab>();
        }
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(Screen.width / 2, 0, Screen.width / 2, Screen.height * 0.1f));
        if (GUILayout.Button("LOGGER", GUILayout.ExpandHeight(true)))
        {
            SwitchTab<LoggerTab>();
        }
        GUILayout.EndArea();

        _currentDebugTab?.UpdateOnGui();

        GUI.backgroundColor = new Color(0, 0, 0, 0);
    }

    private void SwitchTab<T>() where T : ITabTick
    {
        if (_currentDebugTab != null && _currentDebugTab.GetType() == typeof(T))
        {
            return;
        }

        _currentDebugTab = _debugTabs[typeof(T)];
    }
}