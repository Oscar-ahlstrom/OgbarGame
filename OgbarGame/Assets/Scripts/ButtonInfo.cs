using UnityEngine;

public class ButtonInfo : MonoBehaviour
{
    public int enemyId;
    public CombatManager combatManager;

    public void Klick()
    {
        combatManager.EnemyTakeDamage(enemyId);
        combatManager.PressAttackButton();
        combatManager.FlipState();
        combatManager.PlayEnemyTurn();
    }
}
