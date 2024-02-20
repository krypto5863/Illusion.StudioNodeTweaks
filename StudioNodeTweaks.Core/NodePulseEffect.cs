using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace StudioNodeTweaks
{

	internal class NodePulseEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		//private GuideSelect _currentNode;
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
			//_currentNode = gameObject.GetComponent<GuideSelect>();
			transform.localScale = new Vector3(1, 1, 1);
			_currentAnimation = transform.DOScale(transform.localScale * 1.20f, 1f).SetLoops(-1, LoopType.Yoyo);
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			_currentAnimation.Pause();
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			_currentAnimation.Play();
		}
	}
}