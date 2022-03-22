public class AttributesClass
{
    int maxValue;
    int currentValue;
    public AttributesClass(int maxValue)
    {
        this.maxValue = maxValue;
        currentValue = this.maxValue;
    }
    public int MaxValue
    {
        get => maxValue;
        set
        {
            if (value < 0)
            {
                maxValue = 0;
            }
            else
            {
                maxValue = value;
            }
        }
    }
    public int CurrentValue
    {
        get => currentValue;
        set
        {
            if (value > maxValue)
            {
                currentValue = MaxValue;
            }
            else if (value < 0)
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }
        }
    }
}
