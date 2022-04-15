using UnityEngine;

static class Critic
{
    /// <summary>
    /// Faz o random da chance de ataque critico.
    /// </summary>
    /// <param name="chance">% do attaque ser critico</param>
    /// <returns>Retorna verdadeiro se o attaque foi critico</returns>
    public static bool IsCritic(int chance)
    {
        if (Random.Range(0, 100) < chance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}