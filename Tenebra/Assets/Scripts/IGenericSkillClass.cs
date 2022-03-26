
public interface IGenericSkillClass 
{
    long CurrentLevel { get; }
    long CurrentExp { get; set; }
    long PreviousExpLevel { get;}
    long NextExpLevel { get; }

    public long Formula(long level);
    public void levelDown(long curtExp);
    public void levelUp(long curtExp);
    public void SetExp(long level);

}
