using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    [SerializeField] private Collider2D damageCollider;

    [SerializeField] private int damagePower = 1;

    [SerializeField] private bool isDamaging = false;

    [SerializeField] private PlayerHP playerHP;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") ) 
        {

            if (playerHP != null && playerHP.isInvincible == false)
            {
                playerHP.TakeDamage(damagePower);

            }
        }
    }
}
