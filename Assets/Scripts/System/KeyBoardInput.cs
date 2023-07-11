using System;
using UnityEngine;
using VContainer.Unity;
using Canvas;
using Interfaces;

namespace GameSystems 
{
    public class KeyBoardInput : ITickable, IInput
    {

        private float mouseX;
        private float mouseY;

        public event Action<float, float> LookDirEvent;
        public event Action ShootingEvent;
        public event Action ReloadEvent;
        public event Action TargetSwitcherEvent;

        AndroidButtons androidButtons;
        GameManager gameManager;

        public KeyBoardInput(AndroidButtons androidButtons, GameManager gameManager)
        {
            this.androidButtons = androidButtons;
            this.gameManager = gameManager;
        }

        public void Tick()
        {
            if (gameManager.platform == RuntimePlatform.Android)
            {
                AndroidInput();
                return;
            }
            PCInput();
        }


        private void PCInput()
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
            if (mouseX != 0 || mouseY != 0)
            {
                LookDirEvent?.Invoke(mouseX, mouseY);
            }

            if (Input.GetMouseButtonDown(0))
            {
                ShootingEvent?.Invoke();
            }

            if (Input.GetMouseButtonDown(1))
            {
                TargetSwitcherEvent?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ReloadEvent?.Invoke();
            }
        }

        private void AndroidInput()
        {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector3 touchDeltaPos = Input.GetTouch(0).deltaPosition;
                LookDirEvent?.Invoke(touchDeltaPos.normalized.x / 2, touchDeltaPos.normalized.y / 2);
            }

            androidButtons.Shot.onClick.AddListener(() => ShootingEvent?.Invoke());
            androidButtons.Reaload.onClick.AddListener(() => ReloadEvent?.Invoke());
            androidButtons.Aim.onClick.AddListener(() => TargetSwitcherEvent?.Invoke());
        }
    }
}


