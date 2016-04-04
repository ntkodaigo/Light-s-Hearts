using UnityEngine;
using System.Collections;

namespace Enigma.LightsHearts
{
    [RequireComponent(typeof(CanvasGroup))]
    public partial class UIWindow : MonoBehaviour
    {
        public float appearSpeed = 10f;

        protected RectTransform rectTransform;
        protected float alphaTarget = 0f;

        public bool IsOpen
        {
            get
            {
                return alphaTarget == 1f;
            }
        }

        private CanvasGroup _canvasGroup;
        private float _alphaCurrent = 0f;

        void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();

            alphaTarget = _canvasGroup.alpha;
        }

        void Update()
        {
            if (_alphaCurrent != alphaTarget)
            {
                _alphaCurrent = Mathf.Lerp(_alphaCurrent, alphaTarget, Time.deltaTime * appearSpeed);
                if (Mathf.Abs(_alphaCurrent - alphaTarget) < 0.001f)
                {
                    _alphaCurrent = alphaTarget;
                }
                _canvasGroup.alpha = _alphaCurrent * _alphaCurrent;
                
                // Scale effect
                Vector3 size = Vector3.one * (1.5f - _alphaCurrent * 0.5f);
                rectTransform.localScale = size;
            }
        }

        public virtual void Open()
        {
            alphaTarget = 1f;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public virtual void Close()
        {
            alphaTarget = 0f;
            if (_canvasGroup != null)
            {
                _canvasGroup.interactable = false;
                _canvasGroup.blocksRaycasts = false;
            }
        }
    }
}