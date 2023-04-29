using System.Reflection;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class OpenInNewInspector : EditorWindow
{
    static void NewInspector()
    {
        var inspectorWindowType = typeof(Editor).Assembly.GetType("UnityEditor.InspectorWindow");
        if (inspectorWindowType == null)
            return;
        EditorWindow newInspector = (EditorWindow) CreateInstance(inspectorWindowType);
        newInspector.Show(true);
        newInspector.Repaint();
        newInspector.Focus();
#if UNITY_2018_OR_NEWER
		var flipLockedMethod = inspectorWindowType.GetMethod("FlipLocked", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
		if (flipLockedMethod != null)
			flipLockedMethod.Invoke(newInspector, null);
#else
        var isLockedMethod = inspectorWindowType == null ? null :  inspectorWindowType.GetMethod("set_isLocked", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (isLockedMethod != null)
            isLockedMethod.Invoke(newInspector, new object[]{true});
#endif
    }
    [Shortcut("GameObject/Open In New Inspector...", null, KeyCode.N)]
    static void OpenNewInspector(ShortcutArguments shortcutArguments)
    {
        NewInspector();
    }
    [MenuItem("GameObject/Open In New Inspector... %N", false, -1)]
    static void OpenNewInspector(MenuCommand command)
    {
        NewInspector();
    }
}