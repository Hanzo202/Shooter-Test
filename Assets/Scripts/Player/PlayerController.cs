using Interfaces;
using UnityEngine;
using VContainer;
using WeaponSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Vector3 weaponPos;
        [SerializeField] Weapon weapon;


        private IInput input;

        [Inject]
        public void Construct(IInput input)
        {
            this.input = input;
        }


        private void OnEnable()
        {
            input.ShootingEvent += Shooting;
            input.ReloadEvent += Reload;
        }

        public void Shooting()
        {
            weapon.Shooting();
        }
        private void Reload()
        {
            weapon.Reload();
        }

    }
}

