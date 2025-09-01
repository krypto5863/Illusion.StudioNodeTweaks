using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace StudioNodeTweaks
{
    internal class NodePulseEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private bool _isPulseEnabled = true;
        private Vector3 _originalScale;
        private float _offset = 0f;
        
        internal static NodePulseEffect AddComponent(Transform transform)
        {
            var newObj = new GameObject("Pulse Tweener");
            newObj.transform.SetParent(transform.parent);
            transform.SetParent(newObj.transform);
            return newObj.AddComponent<NodePulseEffect>();
        }

        internal void Awake()
        {
            _originalScale = transform.localScale;
        }
        
        private void Update()
        {
            if (!StudioNodeTweaks._pluginInstance._animateNodes.Value || !_isPulseEnabled)
                return;
            _offset += Time.deltaTime;
            var scale = (1.1f + 0.1f * Mathf.Sin(_offset));
            transform.localScale = scale * _originalScale;
        }

        internal void Reset()
        {
            transform.localScale = _originalScale;
        }
        
        void OnDisable()
        {
            _isPulseEnabled = false;
        }
        

        void OnEnable()
        {
            _isPulseEnabled = true;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isPulseEnabled = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isPulseEnabled = true;
        }

        private void OnDestroy()
        {
            _isPulseEnabled = false;
            transform.localScale = _originalScale;
            Destroy(gameObject);
        }
    }
}