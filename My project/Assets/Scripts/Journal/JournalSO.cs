using UnityEngine;

[CreateAssetMenu(fileName = "JournalSO", menuName = "Scriptable Objects/JournalSO")]
public class JournalSO : ScriptableObject
{
    public string title;
    public string entry;
    public int number;
    public Sprite image;
    public string playerResponse;
}
