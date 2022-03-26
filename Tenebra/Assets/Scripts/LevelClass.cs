public class LevelClass : GenericSkillClass
{
    public LevelClass()
    {
        currentLevel = 1;
        CurrentExp = 0;
        previousExpLevel = 0;
        nextExpLevel = Formula(currentLevel);
    }
    //Formula dos Leveis
    public override long Formula(long level)
    {
        if (level <= 1)
            return 50;
        else
            return ((100 * ((level * level) / 4)) + Formula(level - 1));
    }
}
