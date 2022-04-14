
public interface IGenericSkillClass 
{
    int CurrentLevel { get; }
    long CurrentExp { get; set; }
    long PreviousExpLevel { get;}
    long NextExpLevel { get; }

    public long Formula(int level);
    public void levelDown(long curtExp);
    public void levelUp(long curtExp);
    public void SetExp(int level);

}
