using Interfaces;
using UnityEngine;
using VContainer;


namespace Player
{
    public class MouseLook : MonoBehaviour
    {
        [SerializeField] private float damping;
        [SerializeField] private float turningSpeed;

        private float xRot;
        private float YRot;
        private Camera mainCamera;
        private const float RotMaxAngle = 45f;
        private IInput input;



        private const float StandartFOV = 60;
        private const float ApproximationFOV = 10;

        [Inject]
        public void Construct(IInput input)
        {
            this.input = input;
        }

        private void OnEnable()
        {
            input.LookDirEvent += LookDir;
            input.TargetSwitcherEvent += Aim;
        }

        private void Start()
        {
            mainCamera = Camera.main;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            var rotation = Quaternion.Euler(xRot, YRot, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, damping);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, rotation, damping);
        }

        public void Aim()
        {
            if (mainCamera.fieldOfView == StandartFOV)
            {
                mainCamera.fieldOfView = ApproximationFOV;
                turningSpeed /= 10;
            }
            else
            {
                mainCamera.fieldOfView = StandartFOV;
                turningSpeed *= 10;
            }

        }

        private void LookDir(float mouseX, float mouseY)
        {
            xRot -= mouseY * turningSpeed * Time.deltaTime;
            YRot += mouseX * turningSpeed * Time.deltaTime;
            xRot = Mathf.Clamp(xRot, -RotMaxAngle, RotMaxAngle);
        }


    }
}

