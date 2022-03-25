
public interface IGenericSkillClass 
{
    int CurrentLevel { get; }
    double CurrentExp { get; set; }
    double PreviousExpLevel { get;}
    double NextExpLevel { get; }

    public double Formula(int level);
    public void levelDown(double curtExp);
    public void levelUp(double curtExp);
    public void SetExp(int level);

}
