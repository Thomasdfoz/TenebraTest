using System.Collections;
using UnityEngine;

public static class BurningManager
{
    public static void InvokeBurning(GameObject target, string nameBurning, float damage, float timeBurning, DamageType damageType)
    {
        Burning[] arrays = target.GetComponents<Burning>();
        if (arrays.Length > 0)
        {
            bool isOther = false;
            foreach (var item in arrays)
            {
                if (item.source == nameBurning)
                {
                    item.ResetTime(damage);
                    isOther = false;
                    return;
                }
                else
                {
                    isOther = true;
                }
            }
            if (isOther)
            {
                Burning burning = target.AddComponent<Burning>();
                burning.Constructor(damage, timeBurning, damageType, nameBurning);
            }
        }
        else
        {
            Burning burning = target.AddComponent<Burning>();
            burning.Constructor(damage, timeBurning, damageType, nameBurning);
        }
    }
}
