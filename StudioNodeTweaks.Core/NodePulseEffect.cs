using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace StudioNodeTweaks
{
	internal class NodePulseEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		private Tweener _currentAnimation;

		internal static NodePulseEffect AddComponent(Transform transform)
		{
			var newObj = new GameObject("Pulse Tweener");
			newObj.transform.SetParent(transform.parent);
			transform.SetParent(newObj.transform);
			return newObj.AddComponent<NodePulseEffect>();
		}

		internal void Awake()
		{
			transform.localScale = new Vector3(1, 1, 1);
			_currentAnimation = transform.DOScale(transform.localScale * 1.20f, 1f).SetLoops(-1, LoopType.Yoyo);
		}

		void OnDisable()
		{
			_currentAnimation.Pause();
		}

		void OnEnable()
		{
			_currentAnimation.Play();
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			_currentAnimation.Pause();
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			_currentAnimation.Play();
		}

		private void OnDestroy()
		{
			_currentAnimation.Kill();
			_currentAnimation = null;
		}
	}
}