using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KKS_StudioNodeTweaks;

public class PulseEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	//private GuideSelect _currentNode;
	private TweenerCore<Vector3, Vector3, VectorOptions> _currentAnimation;

	public static void AddEffect(Transform transform)
	{
		var newObj = new GameObject("Pulse Tweener");
		newObj.transform.SetParent(transform.parent);
		transform.SetParent(newObj.transform);
		newObj.AddComponent<PulseEffect>();
	}

	public void Awake()
	{
		//_currentNode = gameObject.GetComponent<GuideSelect>();
		_currentAnimation = transform.DOScale(transform.localScale * 1.20f, 0.5f).SetLoops(-1, LoopType.Yoyo);
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