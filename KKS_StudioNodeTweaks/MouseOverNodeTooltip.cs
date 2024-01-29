using Studio;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KKS_StudioNodeTweaks;

internal class MouseOverNodeTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	private static readonly Dictionary<string, string> BonesUserFriendlyNames = new()
	{
		{"cf_t_shoulder_R(work)", "R. Shoulder (IK)"},
		{"cf_t_shoulder_L(work)", "L. Shoulder (IK)"},
		{"cf_t_elbo_R(work)", "R. Elbow (IK)"},
		{"cf_t_elbo_L(work)", "L. Elbow (IK)"},
		{"cf_t_hand_R(work)", "R. Hand (IK)"},
		{"cf_t_hand_L(work)", "L. Hand (IK)"},
		{"cf_t_hips(work)", "Waist (IK)"},
		{"cf_t_waist_R(work)", "R. Hips (IK)"},
		{"cf_t_waist_L(work)", "L. Hips (IK)"},
		{"cf_t_knee_R(work)", "R. Knee (IK)"},
		{"cf_t_knee_L(work)", "L. Knee (IK)"},
		{"cf_t_leg_R(work)", "R. Foot (IK)"},
		{"cf_t_leg_L(work)", "L. Foot (IK)"},
		{"cf_j_neck", "Neck"},
		{"cf_j_head", "Head"},
		{"cf_j_little01_R", "R. Small Proximal"},
		{"cf_j_little02_R", "R. Small Middle"},
		{"cf_j_little03_R", "R. Small Distal"},
		{"cf_j_ring01_R", "R. Ring Proximal"},
		{"cf_j_ring02_R", "R. Ring Middle"},
		{"cf_j_ring03_R", "R. Ring Distal"},
		{"cf_j_middle01_R", "R. Middle Proximal"},
		{"cf_j_middle02_R", "R. Middle Middle"},
		{"cf_j_middle03_R", "R. Middle Distal"},
		{"cf_j_index01_R", "R. Index Proximal"},
		{"cf_j_index02_R", "R. Index Middle"},
		{"cf_j_index03_R", "R. Index Distal"},
		{"cf_j_thumb01_R", "R. Thumb Metacarpal"},
		{"cf_j_thumb02_R", "R. Thumb Proximal"},
		{"cf_j_thumb03_R", "R. Thumb Distal"},
		{"cf_j_little01_L", "L. Small Proximal"},
		{"cf_j_little02_L", "L. Small Middle"},
		{"cf_j_little03_L", "L. Small Distal"},
		{"cf_j_ring01_L", "L. Ring Proximal"},
		{"cf_j_ring02_L", "L. Ring Middle"},
		{"cf_j_ring03_L", "L. Ring Distal"},
		{"cf_j_middle01_L", "L. Middle Proximal"},
		{"cf_j_middle02_L", "L. Middle Middle"},
		{"cf_j_middle03_L", "L. Middle Distal"},
		{"cf_j_index01_L", "L. Index Proximal"},
		{"cf_j_index02_L", "L. Index Middle"},
		{"cf_j_index03_L", "L. Index Distal"},
		{"cf_j_thumb01_L", "L. Thumb Metacarpal"},
		{"cf_j_thumb02_L", "L. Thumb Proximal"},
		{"cf_j_thumb03_L", "L. Thumb Distal"},
		{"cf_j_bust01_R", "R. Chest 1"},
		{"cf_j_bust02_R", "R. Chest 2"},
		{"cf_j_bust03_R", "R. Chest 3"},
		{"cf_j_bust01_L", "L. Chest 1"},
		{"cf_j_bust02_L", "L. Chest 2"},
		{"cf_j_bust03_L", "L. Chest 3"},
		{"cf_j_bnip02root_L", "L. Areola"},
		{"cf_j_bnip02_L", "L. Nipple"},
		{"cf_j_bnip02root_R", "R. Areola"},
		{"cf_j_bnip02_R", "R. Nipple"},
	};

	internal static void AddComponent(Transform transform)
	{
		transform.gameObject.AddComponent<MouseOverNodeTooltip>();
	}

	private bool _calledTooltip;
	private string _textToTooltip;

	internal void Awake()
	{
		var currentNode = gameObject.GetComponent<GuideSelect>();
		var boneName = currentNode.guideObject.transformTarget.name;

		if (boneName.StartsWith("cha"))
		{
			var control = currentNode.guideObject.transformTarget.GetComponent<ChaControl>();

			if (control != null)
			{
				_textToTooltip = $"{control.chaFile.parameter.firstname} \"{control.chaFile.parameter.nickname}\" {control.chaFile.parameter.lastname}";
				_textToTooltip = _textToTooltip.Trim();
				return;
			}
		}

		_textToTooltip = BonesUserFriendlyNames.TryGetValue(boneName, out var newName) ? newName : boneName;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (StudioNodeTweaks._pluginInstance._nodeTooltip.Value == false)
		{
			return;
		}

		NodeTooltip.ShowTooltip_Static(_textToTooltip);
		_calledTooltip = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		NodeTooltip.HideTooltip_Static();
		_calledTooltip = false;
	}

	private void OnDisable()
	{
		if (!_calledTooltip) return;

		NodeTooltip.HideTooltip_Static();
		_calledTooltip = false;
	}
}