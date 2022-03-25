public abstract class GenericSkillClass : IGenericSkillClass
{
    protected int currentLevel;
    private double currentExp;
    protected double previousExpLevel;
    protected double nextExpLevel;
    public virtual int CurrentLevel
    {
        get => currentLevel;
        set
        {
            if (value < 1)
            {
                value = 1;
            }
            currentLevel += value;
        }
    }
    public double CurrentExp
    {
        get => currentExp;
        set
        {
            if (value < 0)
            {
                value = 0;
            }
            currentExp += value;
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
    public double PreviousExpLevel { get => previousExpLevel; }
    public double NextExpLevel { get => nextExpLevel; }


    public virtual void levelUp(double curtExp)
    {
        currentLevel++;
        SetExp(currentLevel);
        if (currentExp > nextExpLevel)
        {
            levelUp(currentExp);
        }
    }
    public virtual void levelDown(double curtExp)
    {
        currentLevel--;
        SetExp(currentLevel);
        if (currentExp < previousExpLevel)
        {
            levelDown(currentLevel);
        }
    }
    public abstract double Formula(int level);
    public void SetExp(int level)
    {
        nextExpLevel = Formula(level);
        previousExpLevel = Formula(level - 1);
    }
}
