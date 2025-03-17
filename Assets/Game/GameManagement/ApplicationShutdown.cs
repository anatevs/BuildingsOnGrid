using UnityEditor;
using UnityEngine;

namespace GameManagement
{
    public sealed class ApplicationShutdown
    {
        public void QuitApp()
        {
            Application.Quit();

#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif

        }
    }
}