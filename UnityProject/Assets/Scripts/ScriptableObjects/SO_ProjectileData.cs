using UnityEngine;

[CreateAssetMenu(fileName = "SO_ProjectileData", menuName = "ScriptableObjects/SO_ProjectileData", order = 1)]
public class SO_ProjectileData : ScriptableObject
{
    [SerializeField] public float speed;
    [SerializeField] public float dmg;
    [SerializeField] public float size;
    [SerializeField] public float impactRadius;
    [SerializeField] public Mesh projectileBody;
    [SerializeField] public float fireRate;
    [SerializeField] public float range;
}
