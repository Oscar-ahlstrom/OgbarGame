using UnityEngine;
using UnityEngine.UI;

public class EnemyButton : Button
{
    public int enemyId;

    public CombatManager combatManager;

    public void klick()
    {
        combatManager.DealDamage(enemyId);
        combatManager.PressAttackButton();
    }
}
