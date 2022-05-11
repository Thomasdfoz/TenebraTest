using UnityEngine;
public abstract class GenericSkillClass
{
    private int currentLevel;
    private long currentExp;
    private long previousExpLevel;
    private long nextExpLevel;
    public int CurrentLevel
    {
        get => currentLevel;
        set
        {
            if (value <= 1)
            {
                value = 1;
            }
            currentLevel = value;
        }

    }
    public virtual long CurrentExp
    {
        get => currentExp;
        set
        {
            currentExp = value;
            if (currentExp <= 0)
            {
                currentExp = 0;
            }
        }
    }

    public long PreviousExpLevel { get => previousExpLevel; set => previousExpLevel = value; }
    public long NextExpLevel { get => nextExpLevel; set => nextExpLevel = value; }

    public virtual void GainExp(int exp, GameController gameController)
    {
        CurrentExp += exp;
        if (CurrentExp >= NextExpLevel)
        {
            levelUp(gameController);
        }
    }
    public virtual void LosesExp(int exp, GameController gameController)
    {
        CurrentExp -= exp;
        if (CurrentExp < PreviousExpLevel)
        {
            levelDown(gameController);
        }
    }
    protected virtual void levelUp(GameController gameController)
    {
        CurrentLevel += 1;
        SetExp(CurrentLevel, gameController);
    }
    protected virtual void levelDown(GameController gameController)
    {
        CurrentLevel -= 1;
        SetExp(CurrentLevel, gameController);
    }
    protected abstract long Formula(int level);
    protected virtual void SetExp(int level, GameController gameController)
    {
        NextExpLevel = Formula(level);
        PreviousExpLevel = Formula(level - 1);
        if (CurrentExp >= NextExpLevel) levelUp(gameController);
        if (CurrentExp < PreviousExpLevel) levelDown(gameController);
    }

}
