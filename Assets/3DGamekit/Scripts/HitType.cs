using UnityEngine;

public enum HitTypes
{
    Crystal,
    DestructableBox,
    EnemySmall,
    GrenadierGolem

}

public class HitType : MonoBehaviour
{
    public HitTypes hitTypeName = HitTypes.EnemySmall;
}