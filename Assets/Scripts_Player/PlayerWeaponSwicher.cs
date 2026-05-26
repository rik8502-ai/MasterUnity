using UnityEngine;

public class PlayerWeaponSwicher : MonoBehaviour
{
    [Header("武器のオブジェクトを割り当ててください")]
    [SerializeField] private GameObject weapon1;
    [SerializeField] private GameObject weapon2;

    private PlayerAttack playerAttack;
    
    void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
        EquipWeapon(1);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(2);
        }
    }

    public void EquipWeapon(int weaponNumber)
    {
        if (weaponNumber == 1)
        {
            weapon1.SetActive(true);
            weapon2.SetActive(false);

            if (playerAttack != null)
            {
                playerAttack.currentWeapon = PlayerAttack.WeaponType.Club;
            }
        }
        else if (weaponNumber == 2)
        {
            weapon1.SetActive(false);
            weapon2.SetActive(true);

            if (playerAttack != null)
            {
                playerAttack.currentWeapon = PlayerAttack.WeaponType.Fireball;
            }
        }
    }

}
