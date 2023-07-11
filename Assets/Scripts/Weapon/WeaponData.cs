using UnityEngine;


namespace WeaponSystem 
{
    [CreateAssetMenu(fileName = "Name", menuName = "Weapon")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private string weaponName;
        [SerializeField] private float damage;
        [SerializeField] private float distance;
        [SerializeField] private float magSize;
        [SerializeField] private float fireRate;
        [SerializeField] private float reloadTime;
        public bool Reloading { get; private set; }

        public string WeaPonName => weaponName;
        public float Damage => damage;
        public float Distance => distance;
        public float MagSize => magSize;
        public float FireRate => fireRate;
        public float ReloadTime => reloadTime;

    }
}



