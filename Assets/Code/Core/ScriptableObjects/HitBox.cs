using UnityEngine;

public class HitBox : MonoBehaviour
{
    private AttackSystem attackSystem;

    private void Awake()
    {
        attackSystem = GetComponent<AttackSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Entity>() != null)
        {

        }
    }
}
