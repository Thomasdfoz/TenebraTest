public abstract class GenericSkillClass : IGenericSkillClass
{
    protected long currentLevel;
    private long currentExp;
    protected long previousExpLevel;
    protected long nextExpLevel;
    public long CurrentLevel
    {
        get => currentLevel;
        
    }
    public long CurrentExp
    {
        get => currentExp;
        set
        {
            if (value < 0)
            {
                value = 0;
            }
            currentExp = value;
            if (currentExp > nextExpLevel)
            {
                levelUp(currentExp);
            }
            if (currentExp < previousExpLevel)
            {
                levelDown(currentLevel);
            }
        }
    }
    public long PreviousExpLevel { get => previousExpLevel; }
    public long NextExpLevel { get => nextExpLevel; }


    public virtual void levelUp(long curtExp)
    {
        currentLevel++;
        SetExp(currentLevel);
        if (currentExp > nextExpLevel)
        {
            levelUp(currentExp);
        }
    }
    public virtual void levelDown(long curtExp)
    {
        currentLevel--;
        SetExp(currentLevel);
        if (currentExp < previousExpLevel)
        {
            levelDown(currentLevel);
        }
    }
    public abstract long Formula(long level);
    public void SetExp(long level)
    {
        nextExpLevel = Formula(level);
        previousExpLevel = Formula(level - 1);

    }
}
