using UnityEngine;

// ★超重要：これを書くことで、Unityの右クリックメニューからこのデータを作れるようになります！
[CreateAssetMenu(fileName = "WeaponData", menuName = "GameData/Weapon Data")]
public class WeaponData : ScriptableObject // ← MonoBehaviour ではなく ScriptableObject を継承します
{
    [Header("武器の名前")]
    public string weaponName = "名無し武器";

    [Header("攻撃力")]
    public int attackPower = 1;

    [Header("攻撃のクールタイム（振る速さ）")]
    public float attackInterval = 0.5f;

    [Header("武器のアイコン画像（UI用など）")]
    public Sprite weaponIcon;
    
    // アニメーターで使うWeaponType（0=剣、1=魔法など）を入れておくのもアリです！
    [Header("アニメーションタイプ")]
    public int animationType = 0;
}