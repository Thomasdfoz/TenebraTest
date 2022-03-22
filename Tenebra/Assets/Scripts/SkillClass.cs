public class SkillClass : GenericSkillClass
{
    public SkillClass()
    {
        currentLevel = 10;
        CurrentExp = 0;
        previousExpLevel = 0;
        nextExpLevel = Formula(currentLevel);
    }

    //Formula dos Skills
    public override long Formula(long level)
    {
        if (level < 10)
            return 10;
        else
            return ((5 * ((level * level) / 4)) + Formula(level - 1));

    }
   

}
