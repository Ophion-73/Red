using UnityEngine;
[CreateAssetMenu(fileName = "Attacks")]
public class AttackSO : ScriptableObject
{
    public int damage;
    public Vector3 direction;
    public float startup;
    public float endlag;
    public float forceKnockback;
}
