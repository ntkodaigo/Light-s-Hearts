using UnityEngine;
using UnityEngine.UI;

namespace Enigma.LightsHearts
{
    [RequireComponent(typeof(CanvasGroup))]
    public partial class TimeLifePanelUI : MonoBehaviour
    {
        public Text timer;

        private CanvasGroup _canvasGroup;

        void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
    }
}
