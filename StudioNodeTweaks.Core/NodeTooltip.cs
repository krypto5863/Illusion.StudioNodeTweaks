using Studio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StudioNodeTweaks
{

	internal class NodeTooltip : MonoBehaviour
	{
		private static NodeTooltip _instance;

		internal static void InitTooltip()
		{
			var newObj = new GameObject("Tooltip Canvas");
			DontDestroyOnLoad(newObj);

			newObj.AddComponent<RectTransform>();

			var canvas = newObj.AddComponent<Canvas>();
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			canvas.targetDisplay = 0;
			//Makes render above all other UI elements.
			canvas.sortingOrder = 7;

			//standard addition.
			var canvasScaler = newObj.AddComponent<CanvasScaler>();
			canvasScaler.scaleFactor = 1;
			canvasScaler.referencePixelsPerUnit = 100;

			/* No raycaster as this doesn't need it.
			var graphicRaycaster = newObj.AddComponent<GraphicRaycaster>();
			graphicRaycaster.ignoreReversedGraphics = true;
			graphicRaycaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;
			*/

			var managerObj = new GameObject("Tooltip Manager");
			var managerRectTransform = managerObj.AddComponent<RectTransform>();
			managerRectTransform.SetParent(newObj.transform);
			managerObj.AddComponent<NodeTooltip>();
			managerRectTransform.pivot = Vector3.zero;
			managerRectTransform.anchorMax = Vector2.zero;
			managerRectTransform.anchorMin = Vector2.zero;
		}

		private TextMeshProUGUI _text;
		private Image _imageComponent;

		private void Awake()
		{
			var backgroundObject = new GameObject("Tooltip Background");
			backgroundObject.transform.SetParent(gameObject.transform);
			_imageComponent = backgroundObject.AddComponent<Image>();
			_imageComponent.color = new Color(0, 0, 0, 0.5f);

			var backgroundTransform = backgroundObject.transform as RectTransform;
			backgroundTransform.pivot = Vector3.zero;
			backgroundTransform.anchorMax = Vector3.zero;
			backgroundTransform.anchorMin = Vector3.zero;
			backgroundTransform.localPosition = Vector3.zero;
			backgroundTransform.offsetMax = Vector3.zero;
			backgroundTransform.offsetMin = Vector3.zero;

			var textObject = new GameObject("Tooltip TextMesh");
			_text = textObject.AddComponent<TextMeshProUGUI>();
			_text.overflowMode = TextOverflowModes.Overflow;
			_text.alignment = TextAlignmentOptions.BottomLeft;

			_text.fontSize = 24;
			_text.transform.SetParent(gameObject.transform);
			_text.color = Color.white;
			_text.margin = new Vector4(4, 4, 4, 4);

			_text.outlineColor = Color.black;
			_text.outlineWidth = 0.33f;
			_text.fontSharedMaterial.EnableKeyword(ShaderUtilities.Keyword_Outline);
#if KKS
			_text.ForceMeshUpdate(true, true);
#else
			_text.ForceMeshUpdate(true);
#endif
			_text.enableWordWrapping = false;
			_text.fontStyle = FontStyles.Bold;

			var textTransform = _text.GetComponent<RectTransform>();
			textTransform.pivot = Vector3.zero;
			textTransform.anchorMax = Vector3.zero;
			textTransform.anchorMin = Vector3.zero;
			textTransform.localPosition = Vector3.zero;
			textTransform.offsetMax = Vector3.zero;
			textTransform.offsetMin = Vector3.zero;

			_instance = this;
			HideTooltip();
		}

		internal void Update()
		{
			/*
			RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform.parent.transform,
				Input.mousePosition, null, out var point2LocalPoint);
			transform.localPosition = point2LocalPoint;
			*/
			var parentRectTransform = (RectTransform)gameObject.transform.parent;

			var newPosition = Input.mousePosition / parentRectTransform.localScale.x;

			//Makes sure tooltip can't exit screen space.
			if (newPosition.x + _imageComponent.rectTransform.rect.width > parentRectTransform.rect.width)
			{
				newPosition.x = parentRectTransform.rect.width - _imageComponent.rectTransform.rect.width;
			}

			if (newPosition.y + _imageComponent.rectTransform.rect.height > parentRectTransform.rect.height)
			{
				newPosition.y = parentRectTransform.rect.height - _imageComponent.rectTransform.rect.height;
			}

			((RectTransform)transform).anchoredPosition = newPosition;
		}

		internal void ShowTooltip(string text)
		{
			Update();

			_text.SetText(text);

#if KKS
			_text.ForceMeshUpdate(true, true);
#else
			_text.ForceMeshUpdate(true);
#endif

			_imageComponent.rectTransform.sizeDelta =
				_text.GetRenderedValues() + new Vector2(_text.margin.x * 2, _text.margin.y * 2);

			gameObject.SetActive(true);
		}

		internal static void ShowTooltip_Static(string text)
		{
			_instance.ShowTooltip(text);
		}

		internal void HideTooltip()
		{
			gameObject.SetActive(false);
		}

		internal static void HideTooltip_Static()
		{
			_instance.gameObject.SetActive(false);
		}
	}
}