using System.Collections.Generic;
using UnityEngine;

public class ConsoleTab : ITabTick
{
    public List<object> CommandList { get; private set; }

    private string _inputConsole;
    private string _textConsole;
    private Vector2 _scrollPosition = Vector2.zero;


    public ConsoleTab()
    {
        CommandList = new List<object>();
    }

    public void UpdateOnGui()
    {
        GUILayout.BeginArea(new Rect(0, Screen.height * 0.1f, Screen.width, Screen.height * 0.4f));
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(0, Screen.height * 0.1f, Screen.width, Screen.height * 0.4f));
        _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        GUILayout.Label(_textConsole, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
        GUILayout.EndScrollView();
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(0, Screen.height * 0.5f, Screen.width, Screen.height * 0.1f));
        _inputConsole = GUILayout.TextField(_inputConsole, GUILayout.ExpandHeight(true), GUILayout.Width(Screen.width - Screen.height * 0.1f));
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(Screen.width - Screen.height * 0.1f, Screen.height * 0.5f, Screen.height * 0.1f, Screen.height * 0.1f));
        if (GUILayout.Button("OK", GUILayout.ExpandHeight(true), GUILayout.Width(Screen.height * 0.1f)))
        {
            EnterDebugCommand();
        }
        GUILayout.EndArea();
    }

    private void HandleInput()
    {
        for (int i = 0; i < CommandList.Count; i++)
        {
            DebugCommandBase debugCommandBase = CommandList[i] as DebugCommandBase;

            if (_inputConsole.Contains(debugCommandBase.Id))
            {
                if (CommandList[i] as DebugCommand != null)
                {
                    (CommandList[i] as DebugCommand).Invoke();
                }
                else if (CommandList[i] as DebugCommand<int> != null)
                {
                    string[] properties = _inputConsole.Split(' ');
                    (CommandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                }
                else if (CommandList[i] as DebugCommand<string> != null)
                {
                    string[] properties = _inputConsole.Split(' ');
                    (CommandList[i] as DebugCommand<string>).Invoke(properties[1]);
                }
            }
        }

        _textConsole += _inputConsole + "\n";
    }

    private void EnterDebugCommand()
    {
        HandleInput();
        _inputConsole = "";
    }
}