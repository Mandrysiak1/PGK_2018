using Assets.PGKScripts;
using UnityEngine;

public class LevelScene : MonoBehaviour
{
    public Transform PlayerStartingPosition;
    public Transform CameraStartingPosition;

    public Player Player { get; set; }
    public MainScript Main { get; set; }
}
