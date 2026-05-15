using UnityEngine;

public class ButtonInfo : MonoBehaviour
{
    public int enemyId;
    public CombatManager combatManager;

    public void Klick()
    {
        combatManager.DealDamage(enemyId);
        combatManager.PressAttackButton();
    }
}
