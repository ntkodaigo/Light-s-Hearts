using UnityEngine;
using System.Collections;

namespace Enigma.LightsHearts
{
    public partial class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        public CharacterPanelUI heroPanel;
        public CharacterPanelUI enemyPanel;
        public TimeLifePanelUI timeLifePanel;
        public WeaponsPanelUI weaponsPanel;
        public UIWindow gameOverWindow;
        public UIWindow wonWindow;

        private bool _toFullHeroLife;

        void Start()
        {
            Instance = this;
        }

        void Update()
        {
            if (_toFullHeroLife)
            {
                heroPanel.healthBar.value = Mathf.Lerp(heroPanel.healthBar.value, 1, Time.deltaTime * 5f);

                if (heroPanel.healthBar.value > 0.95)
                {
                    heroPanel.healthBar.value = 1;
                    _toFullHeroLife = false;
                }
            }
        }

        void OnEnable()
        {
            HeroBaseBehaviour.OnTakedDamage += UpdateHeroPanelUI;
            HeroBaseBehaviour.OnReturnedToLife += UpdateOnReturnedToLifeHeroPanel;
            HeroBaseBehaviour.OnAddedWeapon += UpdateWeaponPanel;
            HeroBaseBehaviour.OnEquippedWeapon += UpdateOnEquipWeaponPanel;
        }

        void OnDisable()
        {
            HeroBaseBehaviour.OnTakedDamage -= UpdateHeroPanelUI;
            HeroBaseBehaviour.OnReturnedToLife -= UpdateOnReturnedToLifeHeroPanel;
            HeroBaseBehaviour.OnAddedWeapon -= UpdateWeaponPanel;
            HeroBaseBehaviour.OnEquippedWeapon -= UpdateOnEquipWeaponPanel;
        }

        void UpdateHeroPanelUI(int damage, float healthPercentage)
        {
            heroPanel.healthBar.value = healthPercentage;
        }

        void UpdateOnReturnedToLifeHeroPanel()
        {
            _toFullHeroLife = true;
        }

        void UpdateWeaponPanel(WeaponBaseBehaviour weapon)
        {
            weaponsPanel.AddWeaponButton(weapon);
        }

        void UpdateOnEquipWeaponPanel(int weaponIndex)
        {
            weaponsPanel.SwitchWeaponUI(weaponIndex);
        }

        public void UpdateEnemyPanelUI(float healthPercentage, Sprite icon)
        {
            enemyPanel.healthBar.value = healthPercentage;
            enemyPanel.characterIcon.sprite = icon;
        }

        public void UpdateTimer(float time)
        {
            timeLifePanel.timer.text = string.Format("{0:0}", time);
        }

        public void ShowGameOverWindow()
        {
            gameOverWindow.Open();
        }

        public void ShowWonWindow()
        {
            wonWindow.Open();
        }
    }
}
