using Overmind.Core.Log;
using UnityEngine;

namespace Overmind.GoldenAge.Unity
{
    public class UnityLogger : ILogger
    {
        public void LogVerbose(string message) { Debug.Log(message); }
        public void LogInfo(string message) { Debug.Log(message); }
        public void LogWarning(string message) { Debug.LogWarning(message); }
        public void LogError(string message) { Debug.LogError(message); }
    }
}
