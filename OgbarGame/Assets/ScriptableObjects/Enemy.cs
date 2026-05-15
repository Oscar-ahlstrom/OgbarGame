using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy")]
public class Enemy : ScriptableObject
{
    [SerializeField] public Sprite aliveSprite;
    [SerializeField] public Sprite deadSprite;
    [SerializeField] public int damage;
    [SerializeField] public int maxHealth;
}
