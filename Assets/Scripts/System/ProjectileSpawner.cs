using Projectile_Space;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;


namespace GameSystems
{
    public class ProjectileSpawner : MonoBehaviour
    {
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private int defaultCapacity = 10;
        [SerializeField] private int maxPoolSize = 15;

        private Dictionary<string, IObjectPool<Projectile>> poolDictionary;
        private Dictionary<string, GameObject> containersDictionary;

        private IObjectResolver objectResolver;

        [Inject]
        private void Construct(IObjectResolver objectResolver)
        {
            this.objectResolver = objectResolver;
        }

        private void Awake()
        {
            poolDictionary = new Dictionary<string, IObjectPool<Projectile>>();
            containersDictionary = new Dictionary<string, GameObject>();
        }

        public Projectile Spawn()
        {
            if (poolDictionary.ContainsKey(projectilePrefab.ProjectileDate.ProjectileName))
            {
                return GetProjectile();
            }

            IObjectPool<Projectile> Pool = new ObjectPool<Projectile>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
            OnDestroyPoolObject, false, defaultCapacity, maxPoolSize);
            poolDictionary.Add(projectilePrefab.ProjectileDate.ProjectileName, Pool);

            GameObject container = new($"{projectilePrefab.ProjectileDate.ProjectileName}_Pool");
            containersDictionary.Add(projectilePrefab.ProjectileDate.ProjectileName, container);

            return GetProjectile();
        }

        private Projectile CreatePooledItem()
        {
            Projectile obj = objectResolver.Instantiate(projectilePrefab);
            obj.GetComponent<Projectile>().Pool = poolDictionary[projectilePrefab.ProjectileDate.ProjectileName];

            return obj;
        }

        private void OnTakeFromPool(Projectile obj) => obj.gameObject.SetActive(true);

        private void OnReturnedToPool(Projectile obj) => obj.gameObject.SetActive(false);

        private void OnDestroyPoolObject(Projectile obj) => Destroy(obj);


        private Projectile GetProjectile()
        {
            Projectile obj = poolDictionary[projectilePrefab.ProjectileDate.ProjectileName].Get();
            obj.transform.SetParent(containersDictionary[projectilePrefab.ProjectileDate.ProjectileName].transform);
            return obj;
        }

    }
}

