using UnityEngine;

public class DifficultyOrderSourceActivation : OrderSourceActivationCondition
{
    public GameDifficulty Difficulty = GameDifficulty.Hard;
    private GameContext Context;

    public override bool IsMeet()
    {
        if(Context == null)
            GameContext.FindIfNull(ref Context);
        if (Context == null)
            return false;

        return Context.Settings.Difficulty == Difficulty;
    }
}
