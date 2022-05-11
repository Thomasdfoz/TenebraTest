using UnityEngine;
public class LevelClass : GenericSkillClass
{
    public LevelClass()
    {
        CurrentLevel = 1;
        CurrentExp = 0;
        PreviousExpLevel = 0;
        NextExpLevel = Formula(CurrentLevel);
    }
    //Formula dos 
    protected override long Formula(int level)
    {
        if (level <= 0) return 0;
        else if (level == 1) return 50;
        return ((100 * ((level * level) / 4)) + Formula(level - 1));
    }
    public override void GainExp(int exp, GameController gameController)
    {
        base.GainExp(exp, gameController);
        gameController.PlayerStats.ExpInfoText(exp);
    }
    public override void LosesExp(int exp, GameController gameController)
    {
        base.LosesExp(exp, gameController);
    }
    protected override void levelUp(GameController gameController)
    {
        base.levelUp(gameController);
        gameController.SetTextInfo("You advanced from level " + (CurrentLevel - 1) + " to " + CurrentLevel);
    }
    protected override void levelDown(GameController gameController)
    {
        base.levelDown(gameController);
        gameController.SetTextInfo("You dropped from " + (CurrentLevel + 1) + " to " + CurrentLevel);

    }
    protected override void SetExp(int level, GameController gameController)
    {
        if (level <= 1)
        {
            base.CurrentLevel = 1;
            base.PreviousExpLevel = 0;
            base.NextExpLevel = 50;
            return;
        }
        base.SetExp(level, gameController);
    }
}
