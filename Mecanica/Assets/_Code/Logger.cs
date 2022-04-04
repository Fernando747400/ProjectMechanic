using UnityEngine;

[AddComponentMenu("Tools/Logging")]
public class Logger : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] bool showLogs;
    [SerializeField] string prefix;
    [SerializeField] Color prefixColor;
    string hexColor;
    public void OnValidate()
    {
        hexColor = "#" + ColorUtility.ToHtmlStringRGBA(prefixColor);
    }
    public void Log(object message, Object sender)
    {
        if (!showLogs) return;
        Debug.Log($"<color={hexColor}>{prefix}: </color>{message}", sender);
    }
}
