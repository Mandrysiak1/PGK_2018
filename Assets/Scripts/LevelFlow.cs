using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Beerfest/Level Flow")]
public class LevelFlow : ScriptableObject
{
    [SerializeField]
    private List<GameLevel> Levels;

    public IEnumerable<GameLevel> AllLevels
    {
        get { return Levels; }
    }

    public GameLevel GetNextLevel(GameLevel level)
    {
        return Levels
            .SkipWhile(l => l != level)
            .Skip(1)
            .FirstOrDefault();
    }

    public GameLevel GetFirstLevel()
    {
        return Levels[0];
    }
}
