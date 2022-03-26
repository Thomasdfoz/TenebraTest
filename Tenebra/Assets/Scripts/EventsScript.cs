using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsScript : MonoBehaviour
{
    public PlayerController playerController;
    private void AutoAttackMelee()
    {
        playerController.AutoAttackMelee();
    }
    private void AutoAttackDistance()
    {
        playerController.AutoAttackMelee();
    }
}
