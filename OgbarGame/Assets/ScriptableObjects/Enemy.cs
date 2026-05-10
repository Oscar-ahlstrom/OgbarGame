using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy")]
public class Enemy : ScriptableObject
{
    [SerializeField] public Sprite sprite;
    [SerializeField] public int damage;
    [SerializeField] public int maxHealth;
}
