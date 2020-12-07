using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private DebugController _debugController;

    private void Start()
    {
        DebugCommand<int> debugCommandInt = new DebugCommand<int>("m1", (value) =>
         {
             Debug.Log("debugCommandInt: " + value);
         });

        DebugCommand<string> debugCommandStr = new DebugCommand<string>("m3", (value) =>
        {
            Debug.Log("debugCommandStr: " + value);
        });

        DebugCommand debugCommand = new DebugCommand("m2", () =>
        {
            Debug.Log("debugCommand: ");
        });

        _debugController.AddDebugCommand(debugCommandInt);
        _debugController.AddDebugCommand(debugCommand);
        _debugController.AddDebugCommand(debugCommandStr);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("LOG Q");
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            _debugController.SetActiveDebug(!_debugController.ActiveDebugging);
        }
    }
}