using UnityEngine;
public static class SkillCalculator
{
    public static int CalculeAbility(float Value, float skill)
    {
        float[] val = { 0, 0 };

        val[0] += Mathf.RoundToInt((((skill / 10) / 100) * Value) + Value);
        val[1] += Mathf.RoundToInt(((skill / 20) * Value) + Value);

        float dam = Random.Range(val[0], val[1]);
        return Mathf.RoundToInt(dam);
    }
    public static int CalculeNormalAttack(float Value)
    {
        float[] val = { 0, 0 };

        val[0] += Mathf.RoundToInt(Value * 0.1f);
        val[1] += Mathf.RoundToInt(Value);

        float dam = Random.Range(val[0], val[1]);

        return Mathf.RoundToInt(dam);
    }
    public static int CalculeCriticAttack(float Value)
    {
        float[] val = { 0, 0 };

        val[0] += Mathf.RoundToInt(Value);
        val[1] += Mathf.RoundToInt(Value * 2);

        float dam = Random.Range(val[0], val[1]);

        return Mathf.RoundToInt(dam);
    }

}
