using UnityEngine;

public class LoggerTab : ITabTick
{
    public string TextLogger { get; private set; }

    public void UpdateOnGui()
    {
        GUILayout.BeginArea(new Rect(0, Screen.height * 0.1f, Screen.width, Screen.height * 0.4f));
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(0, Screen.height * 0.1f, Screen.width, Screen.height * 0.4f));
        GUILayout.Label(TextLogger, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
        GUILayout.EndArea();
    }

    public void AppendLog(string log)
    {
        TextLogger += log;
    }
}