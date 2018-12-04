using UnityEngine;

[CreateAssetMenu(menuName="Beerfest/Level")]
public class GameLevel : ScriptableObject
{
    public string Name;
    public SceneReference Scene;
    public AudioClip Music;
    public float Volume = 1.0f;
}
