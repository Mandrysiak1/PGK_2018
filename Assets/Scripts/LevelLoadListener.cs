using System;
using UnityEngine;
using UnityEngine.Events;

public class LevelLoadListener : MonoBehaviour
{
    [Serializable]
    public class LevelLoadedEvent : UnityEvent<GameLevel, LevelScene> {}

    public LevelLoadedEvent LevelLoaded;
}
