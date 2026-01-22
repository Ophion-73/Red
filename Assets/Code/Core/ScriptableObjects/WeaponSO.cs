using System.IO.Enumeration;
using UnityEngine;
[CreateAssetMenu(fileName = "Weapon")]
public class WeaponSO : ScriptableObject
{
    public string nameWeapon;
    public AttackSO[] GroundedAttackSos;
    public AttackSO[] AirAttackSos;
}
