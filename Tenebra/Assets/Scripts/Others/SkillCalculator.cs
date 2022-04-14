using UnityEngine;
public static class SkillCalculator
{
    public static int Calcule(float Value, int skill)
    {
        int[] val = { 0, 0 };

        val[0] += Mathf.FloorToInt((((skill / 10) / 100) * Value) + Value);
        val[1] += Mathf.FloorToInt(((skill / 100) * Value) + Value);

        int dam = Random.Range(val[0], val[1]);

        return dam;
    }

}
