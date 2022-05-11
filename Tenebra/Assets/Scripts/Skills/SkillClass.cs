public class SkillClass : GenericSkillClass
{
    public SkillClass()
    {
        CurrentLevel = 10;
        CurrentExp = 0;
        PreviousExpLevel = 0;
        NextExpLevel = Formula(CurrentLevel);
    }

    //Formula dos Skills
    protected override long Formula(int level)
    {
        if (level < 10)
            return 10;
        else
            return ((5 * ((level * level) / 4)) + Formula(level - 1));

    }
    protected override void SetExp(int level, GameController gameController)
    {
        if (level <= 10)
        {
            base.CurrentLevel = 10;
            base.PreviousExpLevel = 0;
            base.NextExpLevel = 135;
            return;
        }
        base.SetExp(level, gameController);
    }
}
