using UnityEngine;
using UnityEngine.Pool;


namespace Projectile_Space
{
    public abstract class Projectile : MonoBehaviour
    {
        public IObjectPool<Projectile> Pool { get; set; }
        public abstract ProjectileDate ProjectileDate { get; }

        public abstract void StartPos();
        public abstract void ReleaseObject();
    }
}

