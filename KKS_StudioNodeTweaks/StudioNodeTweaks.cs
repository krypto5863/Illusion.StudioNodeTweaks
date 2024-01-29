using System.Collections.Generic;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Studio;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace KKS_StudioNodeTweaks
{
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	[BepInProcess("CharaStudio")]
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class StudioNodeTweaks : BaseUnityPlugin
	{
		internal static StudioNodeTweaks _pluginInstance;
		internal static ManualLogSource _pluginLogger => _pluginInstance.Logger;

		internal ConfigEntry<bool> _animateNodes;
		internal ConfigEntry<bool> _nodeTooltip;

		internal ConfigEntry<bool> _enableCustomColors;
		internal ConfigEntry<float> _unselectedAlpha;
		internal ConfigEntry<bool> _respectColorValues;

		internal Dictionary<string, ConfigEntry<Color>> ColorConfigDictionary;

		private void Awake()
		{
			_pluginInstance = this;
			Harmony.CreateAndPatchAll(typeof(StudioNodeTweaks));

			_animateNodes = Config.Bind("Node Settings", "Animate Nodes With Pulse", true, "Restart Required");
			_nodeTooltip = Config.Bind("Node Settings", "Show Node Tooltip", true);

			_enableCustomColors = Config.Bind("Color Settings", "Use Custom IK Colors", true, "Restart Required");
			_unselectedAlpha = Config.Bind("Color Settings", "Unselected Alpha", 0.50f, "Restart Required");
			_respectColorValues = Config.Bind("Color Settings", "Respect Input Color Values", true, "Restart Required");

			ColorConfigDictionary = new Dictionary<string, ConfigEntry<Color>>()
			{
				{ "cf_t_hand_R(work)", Config.Bind("Colors", "Right Hand", new Color(1f, 1.0f, 0f), "Restart Required") },
				{ "cf_t_hand_L(work)", Config.Bind("Colors", "Left Hand", new Color(0f, 0f, 1f), "Restart Required") },
				{ "cf_t_elbo_R(work)", Config.Bind("Colors", "Right Elbow", new Color(1f, 0f, 1.0f), "Restart Required") },
				{ "cf_t_elbo_L(work)", Config.Bind("Colors", "Left Elbow", new Color(0f, 1f, 0f), "Restart Required") },
				{ "cf_t_shoulder_R(work)", Config.Bind("Colors", "Right Shoulder", new Color(0f, 1f, 1f), "Restart Required") },
				{ "cf_t_shoulder_L(work)", Config.Bind("Colors", "Left Shoulder", new Color(1f, 0.5f, 0f), "Restart Required") },
				{ "cf_t_hips(work)", Config.Bind("Colors", "Waist", new Color(0.75f, 1f, 0f), "Restart Required") },
				{ "cf_t_waist_R(work)", Config.Bind("Colors", "Right Hip", new Color(0f, 0.5f, 0.5f), "Restart Required") },
				{ "cf_t_waist_L(work)", Config.Bind("Colors", "Left Hip", new Color(1f, 0f, 0f), "Restart Required") },
				{ "cf_t_knee_R(work)", Config.Bind("Colors", "Right Knee", new Color(1.0f, 0.5f, 0.5f), "Restart Required") },
				{ "cf_t_knee_L(work)", Config.Bind("Colors", "Left Knee", new Color(0.5f, 0f, 0.5f), "Restart Required") },
				{ "cf_t_leg_R(work)", Config.Bind("Colors", "Right Foot", new Color(0.7f, 0f, 0.7f), "Restart Required") },
				{ "cf_t_leg_L(work)", Config.Bind("Colors", "Left Foot", new Color(1f, 0.84f, 0f), "Restart Required") },
			};

			NodeTooltip.InitTooltip();
		}

		/*
		private void OnSettingChanged(object sender, EventArgs e)
		{
			for (var i = ModifiedGuideObjects.Count - 1; i >= 0; i--)
			{
				if (ModifiedGuideObjects[i] == null)
				{
					ModifiedGuideObjects.RemoveAt(i);
					continue;
				}

				AssignNodeColor(ModifiedGuideObjects[i]);
			}
		}
		*/

		[HarmonyPatch(typeof(AddObjectAssist), "AddIKTarget", typeof(OCIChar), typeof(IKCtrl), typeof(int),
			typeof(Transform), typeof(bool), typeof(Transform), typeof(bool))]
		[HarmonyPostfix]
		private static void ColorizeNewNode(ref OCIChar.IKInfo __result)
		{
			if (_pluginInstance._enableCustomColors.Value)
			{
				NodeColorizer.AddComponent(__result.guideObject.guideSelect.transform);
			}
		}


		/*
		[HarmonyPatch(typeof(AddObjectAssist), "AddBoneGuide")]
		[HarmonyPostfix]
		private static void ColorizeNewBone(ref GuideObject __result) => NodeColorizer.AddComponent(__result.guideSelect.transform);
		*/

		[HarmonyPatch(typeof(GuideBase), "ConvertColor")]
		[HarmonyPrefix]
		// ReSharper disable once RedundantAssignment
		private static bool RespectColors(ref Color __0, ref Color __result)
		{
			if (_pluginInstance._respectColorValues.Value == false)
			{
				__0.r *= 0.75f;
				__0.g *= 0.75f;
				__0.b *= 0.75f;
			}

			__0.a = _pluginInstance._unselectedAlpha.Value;

			__result = __0;

			return false;
		}

		[HarmonyPatch(typeof(GuideObjectManager), "Add")]
		[HarmonyPostfix]
		private static void AddPulseToObj(ref GuideObject __result)
		{
			if (_pluginInstance._animateNodes.Value)
			{
				NodePulseEffect.AddComponent(__result.guideSelect.transform);
			}

			MouseOverNodeTooltip.AddComponent(__result.guideSelect.transform);
		}
	}
}