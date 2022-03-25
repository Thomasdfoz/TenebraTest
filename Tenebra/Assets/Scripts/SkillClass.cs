public class SkillClass : GenericSkillClass
{
    public SkillClass()
    {
        currentLevel = 10;
        CurrentExp = 0;
        previousExpLevel = 0;
        nextExpLevel = Formula(currentLevel);
    }
    public override int CurrentLevel
    {
        get => currentLevel;
        set
        {
            if (value < 10)
            {
                value = 10;
            }
            currentLevel += value;
        }
    }

    //Formula dos Skills
    public override double Formula(int level)
    {
        if (level < 10)
            return 10;
        else
            return ((5 * ((level * level) / 4)) + Formula(level - 1));

    }
   

}
