using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Enigma.LightsHearts
{
    public partial class WeaponsPanelUI : MonoBehaviour
    {
        public GameObject weaponButtonPrefab;

        // controls
        private List<Button> _weaponIcons = new List<Button>();
        private CanvasGroup _canvasGroup;
        private int _previousWeaponIndex = -1;

        void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void AddWeaponButton(WeaponBaseBehaviour weapon)
        {
            var newWeaponUI = Instantiate(weaponButtonPrefab) as GameObject;
            newWeaponUI.transform.SetParent(transform);
            newWeaponUI.transform.localPosition = Vector3.zero;
            newWeaponUI.transform.localScale = Vector3.one;
            newWeaponUI.GetComponent<Image>().sprite = weapon.DisabledIcon;
            newWeaponUI.transform.GetChild(0).GetComponent<Text>().text = (_weaponIcons.Count + 1).ToString();

            var newButton = newWeaponUI.GetComponent<Button>();
            newButton.interactable = true;
            var spriteState = newButton.spriteState;
            spriteState.disabledSprite = weapon.entityData.Icon;
            spriteState.highlightedSprite = weapon.entityData.Icon;
            spriteState.pressedSprite = weapon.entityData.Icon;
            newButton.spriteState = spriteState;

            //Debug.Log(GameObject.FindGameObjectWithTag("Player") == null);
            //newButton.onClick.AddListener(delegate { GameObject.FindGameObjectWithTag("Player").GetComponent<HeroBaseBehaviour>().SwitchWeapon(_weaponIcons.Count); });
            _weaponIcons.Add(newButton);
        }

        public void SwitchWeaponUI(int index)
        {
            if (_previousWeaponIndex != index)
            {
                if (_previousWeaponIndex != -1)
                    _weaponIcons[_previousWeaponIndex].interactable = true;
                _weaponIcons[index].interactable = false;

                _previousWeaponIndex = index;
            }
        }
    }
}
