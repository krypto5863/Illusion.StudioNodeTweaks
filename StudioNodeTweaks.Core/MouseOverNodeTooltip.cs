using Studio;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

#if HS2
using AIChara;
#endif

namespace StudioNodeTweaks
{

	internal class MouseOverNodeTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		private static readonly Dictionary<string, string> BonesUserFriendlyNames = new Dictionary<string, string>()
		{
#if HS2
			{ "f_t_shoulder_r(work)", "R. Shoulder (IK)" },
			{ "f_t_shoulder_l(work)", "L. Shoulder (IK)" },
			{ "f_t_elbo_r(work)", "R. Elbow (IK)" },
			{ "f_t_elbo_l(work)", "L. Elbow (IK)" },
			{ "f_t_arm_r(work)", "R. Hand (IK)" },
			{ "f_t_arm_l(work)", "L. Hand (IK)" },
			{ "f_t_hips(work)", "Waist (IK)" },
			{ "f_t_thigh_r(work)", "R. Hips (IK)" },
			{ "f_t_thigh_l(work)", "L. Hips (IK)" },
			{ "f_t_knee_r(work)", "R. Knee (IK)" },
			{ "f_t_knee_l(work)", "L. Knee (IK)" },
			{ "f_t_leg_r(work)", "R. Foot (IK)" },
			{ "f_t_leg_l(work)", "L. Foot (IK)" },

			{ "cf_j_hand_little01_r", "R. Small Proximal" },
			{ "cf_j_hand_little02_r", "R. Small Middle" },
			{ "cf_j_hand_little03_r", "R. Small Distal" },
			{ "cf_j_hand_ring01_r", "R. Ring Proximal" },
			{ "cf_j_hand_ring02_r", "R. Ring Middle" },
			{ "cf_j_hand_ring03_r", "R. Ring Distal" },
			{ "cf_j_hand_middle01_r", "R. Middle Proximal" },
			{ "cf_j_hand_middle02_r", "R. Middle Middle" },
			{ "cf_j_hand_middle03_r", "R. Middle Distal" },
			{ "cf_j_hand_index01_r", "R. Index Proximal" },
			{ "cf_j_hand_index02_r", "R. Index Middle" },
			{ "cf_j_hand_index03_r", "R. Index Distal" },
			{ "cf_j_hand_thumb01_r", "R. Thumb Metacarpal" },
			{ "cf_j_hand_thumb02_r", "R. Thumb Proximal" },
			{ "cf_j_hand_thumb03_r", "R. Thumb Distal" },
			{ "cf_j_hand_little01_l", "L. Small Proximal" },
			{ "cf_j_hand_little02_l", "L. Small Middle" },
			{ "cf_j_hand_little03_l", "L. Small Distal" },
			{ "cf_j_hand_ring01_l", "L. Ring Proximal" },
			{ "cf_j_hand_ring02_l", "L. Ring Middle" },
			{ "cf_j_hand_ring03_l", "L. Ring Distal" },
			{ "cf_j_hand_middle01_l", "L. Middle Proximal" },
			{ "cf_j_hand_middle02_l", "L. Middle Middle" },
			{ "cf_j_hand_middle03_l", "L. Middle Distal" },
			{ "cf_j_hand_index01_l", "L. Index Proximal" },
			{ "cf_j_hand_index02_l", "L. Index Middle" },
			{ "cf_j_hand_index03_l", "L. Index Distal" },
			{ "cf_j_hand_thumb01_l", "L. Thumb Metacarpal" },
			{ "cf_j_hand_thumb02_l", "L. Thumb Proximal" },
			{ "cf_j_hand_thumb03_l", "L. Thumb Distal" },

			{ "cf_j_mune01_r", "R. Chest 1" },
			{ "cf_j_mune02_r", "R. Chest 2" },
			{ "cf_j_mune03_r", "R. Chest 3" },
			{ "cf_j_mune01_l", "L. Chest 1" },
			{ "cf_j_mune02_l", "L. Chest 2" },
			{ "cf_j_mune03_l", "L. Chest 3" },
			{ "cf_j_mune_nip01_l", "L. Areola" },
			{ "cf_j_mune_nip02_l", "L. Nipple" },
			{ "cf_j_mune_nip01_r", "R. Areola" },
			{ "cf_j_mune_nip02_r", "R. Nipple" },
			
			{"cf_j_hand_r", "R. Hand"},
			{"cf_j_hand_l", "L. Hand"},
			{"cf_j_armlow01_r", "R. Elbow"},
			{"cf_j_armlow01_l", "L. Elbow"},
			{"cf_j_armup00_r", "R. Shoulder"},
			{"cf_j_armup00_l", "L. Shoulder"},
			{"cf_j_shoulder_r", "R. Clavicle"},
			{"cf_j_shoulder_l", "L. Clavicle"},

			{"cf_j_spine03", "Upper Spine"},
			{"cf_j_spine02", "Middle Spine"},
			{"cf_j_spine01", "Lower Spine"},
			{"cf_j_kosi01", "Waist"},
			{"cf_j_kosi02", "Hips"},
			{"cf_j_hips", "Root"},

			{"cf_j_legup00_r", "R. Thigh"},
			{"cf_j_legup00_l", "L. Thigh"},

			{"cf_j_leglow01_r", "R. Knee"},
			{"cf_j_leglow01_l", "L. Knee"},

			{"cf_j_foot01_r", "R. Foot"},
			{"cf_j_foot01_l", "L. Foot"},

			{"cf_j_foot02_r", "R. Ankle"},
			{"cf_j_foot02_l", "L. Ankle"},

			{"cf_j_toes01_r", "R. Toes"},
			{"cf_j_toes01_l", "L. Toes"},
#else

			{ "cf_t_shoulder_r(work)", "R. Shoulder (IK)" },
			{ "cf_t_shoulder_l(work)", "L. Shoulder (IK)" },
			{ "cf_t_elbo_r(work)", "R. Elbow (IK)" },
			{ "cf_t_elbo_l(work)", "L. Elbow (IK)" },
			{ "cf_t_hand_r(work)", "R. Hand (IK)" },
			{ "cf_t_hand_l(work)", "L. Hand (IK)" },
			{ "cf_t_hips(work)", "Waist (IK)" },
			{ "cf_t_waist_r(work)", "R. Hips (IK)" },
			{ "cf_t_waist_l(work)", "L. Hips (IK)" },
			{ "cf_t_knee_r(work)", "R. Knee (IK)" },
			{ "cf_t_knee_l(work)", "L. Knee (IK)" },
			{ "cf_t_leg_r(work)", "R. Foot (IK)" },
			{ "cf_t_leg_l(work)", "L. Foot (IK)" },

			{ "cf_j_little01_r", "R. Small Proximal" },
			{ "cf_j_little02_r", "R. Small Middle" },
			{ "cf_j_little03_r", "R. Small Distal" },
			{ "cf_j_ring01_r", "R. Ring Proximal" },
			{ "cf_j_ring02_r", "R. Ring Middle" },
			{ "cf_j_ring03_r", "R. Ring Distal" },
			{ "cf_j_middle01_r", "R. Middle Proximal" },
			{ "cf_j_middle02_r", "R. Middle Middle" },
			{ "cf_j_middle03_r", "R. Middle Distal" },
			{ "cf_j_index01_r", "R. Index Proximal" },
			{ "cf_j_index02_r", "R. Index Middle" },
			{ "cf_j_index03_r", "R. Index Distal" },
			{ "cf_j_thumb01_r", "R. Thumb Metacarpal" },
			{ "cf_j_thumb02_r", "R. Thumb Proximal" },
			{ "cf_j_thumb03_r", "R. Thumb Distal" },
			{ "cf_j_little01_l", "L. Small Proximal" },
			{ "cf_j_little02_l", "L. Small Middle" },
			{ "cf_j_little03_l", "L. Small Distal" },
			{ "cf_j_ring01_l", "L. Ring Proximal" },
			{ "cf_j_ring02_l", "L. Ring Middle" },
			{ "cf_j_ring03_l", "L. Ring Distal" },
			{ "cf_j_middle01_l", "L. Middle Proximal" },
			{ "cf_j_middle02_l", "L. Middle Middle" },
			{ "cf_j_middle03_l", "L. Middle Distal" },
			{ "cf_j_index01_l", "L. Index Proximal" },
			{ "cf_j_index02_l", "L. Index Middle" },
			{ "cf_j_index03_l", "L. Index Distal" },
			{ "cf_j_thumb01_l", "L. Thumb Metacarpal" },
			{ "cf_j_thumb02_l", "L. Thumb Proximal" },
			{ "cf_j_thumb03_l", "L. Thumb Distal" },

			{ "cf_j_bust01_r", "R. Chest 1" },
			{ "cf_j_bust02_r", "R. Chest 2" },
			{ "cf_j_bust03_r", "R. Chest 3" },
			{ "cf_j_bust01_l", "L. Chest 1" },
			{ "cf_j_bust02_l", "L. Chest 2" },
			{ "cf_j_bust03_l", "L. Chest 3" },
			{ "cf_j_bnip02root_l", "L. Areola" },
			{ "cf_j_bnip02_l", "L. Nipple" },
			{ "cf_j_bnip02root_r", "R. Areola" },
			{ "cf_j_bnip02_r", "R. Nipple" },

			{"cf_j_hand_r", "R. Hand"},
			{"cf_j_hand_l", "L. Hand"},
			{"cf_j_forearm01_r", "R. Elbow"},
			{"cf_j_forearm01_l", "L. Elbow"},
			{"cf_j_arm00_r", "R. Shoulder"},
			{"cf_j_arm00_l", "L. Shoulder"},
			{"cf_j_shoulder_r", "R. Clavicle"},
			{"cf_j_shoulder_l", "L. Clavicle"},

			{"cf_j_spine02", "Upper Spine"},
			{"cf_j_spine01", "Lower Spine"},
			{"cf_j_waist01", "Waist"},
			{"cf_j_hips", "Root"},

			{"cf_j_thigh00_r", "R. Thigh"},
			{"cf_j_thigh00_l", "L. Thigh"},

			{"cf_j_leg01_r", "R. Knee"},
			{"cf_j_leg01_l", "L. Knee"},

			{"cf_j_leg03_r", "R. Foot"},
			{"cf_j_leg03_l", "L. Foot"},

			{"cf_j_toes_r", "R. Toes"},
			{"cf_j_toes_l", "L. Toes"},
#endif

			{ "cf_j_neck", "Neck" },
			{ "cf_j_head", "Head" },
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
#if HS2
					_textToTooltip = control.chaFile.parameter.fullname;
#else
					_textToTooltip =
						$"{control.chaFile.parameter.firstname} \"{control.chaFile.parameter.nickname}\" {control.chaFile.parameter.lastname}";
#endif

					_textToTooltip = _textToTooltip.Trim();
					return;
				}
			}

			//HS2 uses the same IK bone names as KKS just without the first letter.
			_textToTooltip = (BonesUserFriendlyNames.TryGetValue(boneName.ToLower(), out var newName)) ? newName : boneName;
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
}