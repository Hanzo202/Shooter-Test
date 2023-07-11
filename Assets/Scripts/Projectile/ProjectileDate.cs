using UnityEngine;


namespace Projectile_Space
{
    [CreateAssetMenu(fileName = "Name", menuName = "Projectile")]
    public class ProjectileDate : ScriptableObject
    {
        [SerializeField] private string projectileName;
        [SerializeField] private float force;
        [SerializeField] private bool isRaycast;

        public string ProjectileName => projectileName;
        public float Force => force;
        public bool IsRaycast => isRaycast;
    }
}


