using UnityEngine;
using UnityEngine.UI;

namespace Enigma.LightsHearts
{
    [RequireComponent(typeof(CanvasGroup))]
    public partial class CharacterPanelUI : MonoBehaviour
    {
        // controls
        public Image characterIcon;
        public Slider healthBar;

        private CanvasGroup _canvasGroup;

        void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
    }
}
