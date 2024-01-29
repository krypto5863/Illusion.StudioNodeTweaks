using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KKS_StudioNodeTweaks;

internal class NodePulseEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	//private GuideSelect _currentNode;
	private TweenerCore<Vector3, Vector3, VectorOptions> _currentAnimation;

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