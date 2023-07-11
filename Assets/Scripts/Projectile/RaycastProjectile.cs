using GameSystems;
using System.Collections;
using UnityEngine;
using VContainer;


namespace Projectile_Space 
{
    [RequireComponent(typeof(LineRenderer))]
    public class RaycastProjectile : Projectile
    {
        [SerializeField] private ProjectileDate projectileDate;
        private LineRenderer lineRenderer;
        private GameManager gameManager;

        public override ProjectileDate ProjectileDate => projectileDate;


        [Inject]
        private void Construct(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }
        public override void StartPos()
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        public void StartRaycast(RaycastHit hitInfo)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hitInfo.point);
            gameManager.WasHit(hitInfo.transform.gameObject);
            StartCoroutine(RaycastLineDeley());
        }

        public override void ReleaseObject()
        {
            Pool.Release(this);
        }

        private IEnumerator RaycastLineDeley()
        {
            yield return new WaitForSeconds(0.1f);
            ReleaseObject();
        }
    }
}


