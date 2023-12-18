using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Studio;
using UnityEngine;

namespace KKS_IKNodeColors
{
	[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
	public class IKNodeColorer : BaseUnityPlugin
	{
		private static ConfigEntry<Color> _leftHand;
		private static ConfigEntry<Color> _rightHand;
		private static ConfigEntry<Color> _leftElbow;
		private static ConfigEntry<Color> _rightElbow;
		private static ConfigEntry<Color> _leftShoulder;
		private static ConfigEntry<Color> _rightShoulder;

		private static ConfigEntry<Color> _hips;
		private static ConfigEntry<Color> _leftWaist;
		private static ConfigEntry<Color> _rightWaist;

		private static ConfigEntry<Color> _leftKnee;
		private static ConfigEntry<Color> _rightKnee;
		private static ConfigEntry<Color> _leftLeg;
		private static ConfigEntry<Color> _rightLeg;

		private static ManualLogSource _pluginLogger;

		private void Awake()
		{
			// IKNodeColorer startup logic
			//Logger.LogInfo($"IKNodeColorer {PluginInfo.PLUGIN_GUID} is loaded!");
			Harmony.CreateAndPatchAll(typeof(IKNodeColorer));

			_leftHand = Config.Bind("Colors", "Left Hand", new Color(0f, 1f, 0.333f));
			_rightHand = Config.Bind("Colors", "Right Hand", new Color(0.666f, 1f, 0f));
			_leftElbow = Config.Bind("Colors", "Left Elbow", new Color(0f, 1f, 0f));
			_rightElbow = Config.Bind("Colors", "Right Elbow", new Color(1f, 1f, 0f));
			_leftShoulder = Config.Bind("Colors", "Left Shoulder", new Color(0.333f, 1f, 0f));
			_rightShoulder = Config.Bind("Colors", "Right Shoulder", new Color(1f, 0.666f, 0f));

			_hips = Config.Bind("Colors", "Hips", new Color(0.666f, 0f, 1f));
			_leftWaist = Config.Bind("Colors", "Left Waist", new Color(0f, 0.333f, 1f));
			_rightWaist = Config.Bind("Colors", "Right Waist", new Color(0f, 1f, 0.666f));

			_leftKnee = Config.Bind("Colors", "Left Knee", new Color(0f, 0f, 1f));
			_rightKnee = Config.Bind("Colors", "Right Knee", new Color(0f, 1f, 1f));
			_leftLeg = Config.Bind("Colors", "Left Ankle", new Color(0.333f, 0f, 1f));
			_rightLeg = Config.Bind("Colors", "Right Ankle", new Color(0f, 0.666f, 1f));

			_pluginLogger = Logger;
		}

		[HarmonyPatch(typeof(AddObjectAssist), "AddIKTarget", typeof(OCIChar), typeof(IKCtrl), typeof(int), typeof(Transform), typeof(bool), typeof(Transform), typeof(bool))]
		[HarmonyPostfix]
		private static void DoColors(ref OCIChar.IKInfo __result)
		{
			_pluginLogger.LogInfo($"Coloring node for bone {__result.guideObject.transformTarget.name}");

			if (__result.guideObject.transformTarget.name.Contains("hand_L"))
			{
				__result.guideObject.guideSelect.color = _leftHand.Value;
			}
			else if (__result.guideObject.transformTarget.name.Contains("hand_R"))
			{
				__result.guideObject.guideSelect.color = _rightHand.Value;
			}
			else if (__result.guideObject.transformTarget.name.Contains("elbo_L"))
			{
				__result.guideObject.guideSelect.color = _leftElbow.Value;
			}
			else if (__result.guideObject.transformTarget.name.Contains("elbo_R"))
			{
				__result.guideObject.guideSelect.color = _rightElbow.Value;
			}
			else if (__result.guideObject.transformTarget.name.Contains("shoulder_L"))
			{
				__result.guideObject.guideSelect.color = _leftShoulder.Value;
			}
			else if (__result.guideObject.transformTarget.name.Contains("shoulder_R"))
			{
				__result.guideObject.guideSelect.color = _rightShoulder.Value;
			}
			else if (__result.guideObject.transformTarget.name.Contains("hips"))
			{
				__result.guideObject.guideSelect.color = _hips.Value;
			}
			else if (__result.guideObject.transformTarget.name.Contains("waist_L"))
			{
				__result.guideObject.guideSelect.color = _leftWaist.Value;
			}
			else if (__result.guideObject.transformTarget.name.Contains("waist_R"))
			{
				__result.guideObject.guideSelect.color = _rightWaist.Value;
			}
			else if (__result.guideObject.transformTarget.name.Contains("knee_L"))
			{
				__result.guideObject.guideSelect.color = _leftKnee.Value;
			}
			else if (__result.guideObject.transformTarget.name.Contains("knee_R"))
			{
				__result.guideObject.guideSelect.color = _rightKnee.Value;
			}
			else if (__result.guideObject.transformTarget.name.Contains("leg_L"))
			{
				__result.guideObject.guideSelect.color = _leftLeg.Value;
			}
			else if (__result.guideObject.transformTarget.name.Contains("leg_R"))
			{
				__result.guideObject.guideSelect.color = _rightLeg.Value;
			}
		}
	}
}