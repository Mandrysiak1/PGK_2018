using UnityEngine;

[CreateAssetMenu(menuName="Beerfest/Level")]
public class GameLevel : ScriptableObject
{
    public string Name;
    public SceneReference Scene;
    public AudioClip Music;
}
