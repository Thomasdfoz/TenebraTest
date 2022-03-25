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
            maxValue += value;

            if (maxValue < 0)
            {
                maxValue = 0;
            }
        }
    }
    public int CurrentValue
    {
        get => currentValue;
        set
        {
            currentValue += value;

            if (currentValue > maxValue)
            {
                currentValue = MaxValue;
            }
            else if (currentValue < 0)
            {
                currentValue = 0;
            }
        }
    }
}
