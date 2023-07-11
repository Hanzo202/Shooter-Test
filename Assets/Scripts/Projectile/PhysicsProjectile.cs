using GameSystems;
using UnityEngine;
using VContainer;


namespace Projectile_Space
{
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicsProjectile : Projectile
    {
        [SerializeField] private ProjectileDate projectileDate;

        private Rigidbody rb;
        private GameManager gameManager;

        public override ProjectileDate ProjectileDate => projectileDate;

        [Inject]
        private void Construct(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void AddForce(Vector3 dir)
        {
            rb.AddForce(dir * projectileDate.Force, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            gameManager.WasHit(collision.gameObject);
            if (collision.gameObject.CompareTag("Ground"))
            {
                ReleaseObject();
            }
        }

        public override void StartPos()
        {
            rb.velocity = Vector3.zero;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }

        public override void ReleaseObject()
        {
            Pool.Release(this);
        }
    }
}


