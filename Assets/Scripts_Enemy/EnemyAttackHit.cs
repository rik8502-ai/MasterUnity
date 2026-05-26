using UnityEngine;

public class EnemyAttackHit : MonoBehaviour
{
    [SerializeField] private int AttackPower = 1;
    [SerializeField] private PlayerHP playerHP;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            if(playerHP != null)
            {
                playerHP.TakeDamage(AttackPower);
            }
        }
    }
}
