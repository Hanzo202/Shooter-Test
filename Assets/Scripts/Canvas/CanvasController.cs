using GameSystems;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;


namespace Canvas 
{
    public class CanvasController : MonoBehaviour
    {
        [SerializeField] private Image standartTargetImage;
        [SerializeField] private Image approximationTargetImage;
        [SerializeField] private GameObject androidUGIPanel;
        [SerializeField] private TextMeshProUGUI ammoText;
        [SerializeField] private TextMeshProUGUI reloadText;

        private IInput input;
        private GameManager gameManager;

        [Inject]
        private void Construct(IInput input, GameManager gameManager)
        {
            this.input = input;
            this.gameManager = gameManager;
        }

        private void Start()
        {
            if (gameManager.platform == RuntimePlatform.Android)
            {
                androidUGIPanel.SetActive(true);
            }
            else
            {
                androidUGIPanel.SetActive(false);
            }
        }

        private void OnEnable()
        {
            input.TargetSwitcherEvent += TargetSwitcher;
        }

        private void TargetSwitcher()
        {
            approximationTargetImage.enabled = !approximationTargetImage.enabled;
            standartTargetImage.enabled = !standartTargetImage.enabled;
        }

        public void OnAmmoTextChanged(float currentAmmo, float magSize)
        {
            ammoText.text = $"Ammo {currentAmmo}/{magSize}";
        }

        public void OnReloadTextChanged()
        {
            if (reloadText.text == string.Empty)
            {
                reloadText.text = "Reload";
                return;
            }

            reloadText.text = string.Empty;
        }
    }
}


