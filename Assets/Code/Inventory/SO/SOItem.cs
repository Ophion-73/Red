using UnityEngine;

[CreateAssetMenu(fileName = "SOItem", menuName = "Scriptable Objects/SOItem")]
public abstract class SOItem : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    [TextArea] public string description;
}
