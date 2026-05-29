using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy")]
public class Enemy : ScriptableObject
{
    [Header("Sprites")]
    [SerializeField] public Sprite aliveSprite;
    [SerializeField] public Sprite deadSprite;
    [Header("Stats")]
    [SerializeField] public int damage;
    [SerializeField] public int maxHealth;
    [Header("Drops")]
    [SerializeField] public int xp;
    [SerializeField] public int money;


}
