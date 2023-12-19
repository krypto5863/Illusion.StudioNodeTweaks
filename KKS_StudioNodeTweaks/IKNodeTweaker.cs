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
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class IKNodeTweaker : BaseUnityPlugin
	{
		private static IKNodeTweaker _pluginInstance;
		private static ManualLogSource _pluginLogger => _pluginInstance.Logger;

		private ConfigEntry<bool> _animateNodes;

		private ConfigEntry<float> _unselectedAlpha;
		private ConfigEntry<bool> _respectColorValues;

		private ConfigEntry<Color> _leftHand;
		private ConfigEntry<Color> _rightHand;
		private ConfigEntry<Color> _leftElbow;
		private ConfigEntry<Color> _rightElbow;
		private ConfigEntry<Color> _leftShoulder;
		private ConfigEntry<Color> _rightShoulder;

		private ConfigEntry<Color> _hips;
		private ConfigEntry<Color> _leftWaist;
		private ConfigEntry<Color> _rightWaist;

		private ConfigEntry<Color> _leftKnee;
		private ConfigEntry<Color> _rightKnee;
		private ConfigEntry<Color> _leftLeg;
		private ConfigEntry<Color> _rightLeg;

		//private static List<GuideObject> ModifiedGuideObjects = new List<GuideObject>();

		private void Awake()
		{
			_pluginInstance = this;
			// IKNodeTweaker startup logic
			//Logger.LogInfo($"IKNodeTweaker {PluginInfo.PLUGIN_GUID} is loaded!");
			Harmony.CreateAndPatchAll(typeof(IKNodeTweaker));

			_animateNodes = Config.Bind("Node Settings", "Animate Nodes With Pulse", true);

			_unselectedAlpha = Config.Bind("Color Settings", "Unselected Alpha", 0.75f);
			_respectColorValues = Config.Bind("Color Settings", "Respect Input Color Values", true);

			_leftHand = Config.Bind("Colors", "Left Hand", new Color(0f, 0f, 1f));
			//_leftHand.SettingChanged += OnSettingChanged;
			_rightHand = Config.Bind("Colors", "Right Hand", new Color(1f, 1.0f, 0f));
			//_rightHand.SettingChanged += OnSettingChanged;
			_leftElbow = Config.Bind("Colors", "Left Elbow", new Color(0f, 1f, 0f));
			//_leftElbow.SettingChanged += OnSettingChanged;
			_rightElbow = Config.Bind("Colors", "Right Elbow", new Color(1f, 0f, 1.0f));
			//_rightElbow.SettingChanged += OnSettingChanged;
			_leftShoulder = Config.Bind("Colors", "Left Shoulder", new Color(1f, 0.5f, 0f));
			//_leftShoulder.SettingChanged += OnSettingChanged;
			_rightShoulder = Config.Bind("Colors", "Right Shoulder", new Color(0f, 1f, 1f));
			//_rightShoulder.SettingChanged += OnSettingChanged;

			_hips = Config.Bind("Colors", "Waist", new Color(0.75f, 1f, 0f));
			//_hips.SettingChanged += OnSettingChanged;
			_leftWaist = Config.Bind("Colors", "Left Hip", new Color(1f, 0f, 0f));
			//_leftWaist.SettingChanged += OnSettingChanged;
			_rightWaist = Config.Bind("Colors", "Right Hip", new Color(0f, 0.5f, 0.5f));
			//_rightWaist.SettingChanged += OnSettingChanged;

			_leftKnee = Config.Bind("Colors", "Left Knee", new Color(0.5f, 0f, 0.5f));
			//_leftKnee.SettingChanged += OnSettingChanged;
			_rightKnee = Config.Bind("Colors", "Right Knee", new Color(1.0f, 0.5f, 0.5f));
			//_rightKnee.SettingChanged += OnSettingChanged;
			_leftLeg = Config.Bind("Colors", "Left Ankle", new Color(1f, 0.84f, 0f));
			//_leftLeg.SettingChanged += OnSettingChanged;
			_rightLeg = Config.Bind("Colors", "Right Ankle", new Color(0.7f, 0f, 0.7f));
			//_rightLeg.SettingChanged += OnSettingChanged;
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

		private static void AssignNodeColor(GuideObject guideObject)
		{
			if (_pluginInstance == null)
			{
				return;
			}

			_pluginLogger.LogInfo("Colorizing node for: " + guideObject.transformTarget.name);

			if (guideObject.transformTarget.name.Contains("hand_L"))
			{
				guideObject.guideSelect.color = _pluginInstance._leftHand.Value;
			}
			else if (guideObject.transformTarget.name.Contains("hand_R"))
			{
				guideObject.guideSelect.color = _pluginInstance._rightHand.Value;
			}
			else if (guideObject.transformTarget.name.Contains("elbo_L"))
			{
				guideObject.guideSelect.color = _pluginInstance._leftElbow.Value;
			}
			else if (guideObject.transformTarget.name.Contains("elbo_R"))
			{
				guideObject.guideSelect.color = _pluginInstance._rightElbow.Value;
			}
			else if (guideObject.transformTarget.name.Contains("shoulder_L"))
			{
				guideObject.guideSelect.color = _pluginInstance._leftShoulder.Value;
			}
			else if (guideObject.transformTarget.name.Contains("shoulder_R"))
			{
				guideObject.guideSelect.color = _pluginInstance._rightShoulder.Value;
			}
			else if (guideObject.transformTarget.name.Contains("hips"))
			{
				guideObject.guideSelect.color = _pluginInstance._hips.Value;
			}
			else if (guideObject.transformTarget.name.Contains("waist_L"))
			{
				guideObject.guideSelect.color = _pluginInstance._leftWaist.Value;
			}
			else if (guideObject.transformTarget.name.Contains("waist_R"))
			{
				guideObject.guideSelect.color = _pluginInstance._rightWaist.Value;
			}
			else if (guideObject.transformTarget.name.Contains("knee_L"))
			{
				guideObject.guideSelect.color = _pluginInstance._leftKnee.Value;
			}
			else if (guideObject.transformTarget.name.Contains("knee_R"))
			{
				guideObject.guideSelect.color = _pluginInstance._rightKnee.Value;
			}
			else if (guideObject.transformTarget.name.Contains("leg_L"))
			{
				guideObject.guideSelect.color = _pluginInstance._leftLeg.Value;
			}
			else if (guideObject.transformTarget.name.Contains("leg_R"))
			{
				guideObject.guideSelect.color = _pluginInstance._rightLeg.Value;
			}
			else
			{
				return;
			}

			//ModifiedGuideObjects.Add(guideObject);
		}

		[HarmonyPatch(typeof(AddObjectAssist), "AddIKTarget", typeof(OCIChar), typeof(IKCtrl), typeof(int),
			typeof(Transform), typeof(bool), typeof(Transform), typeof(bool))]
		[HarmonyPostfix]
		private static void ColorizeNewNode(ref OCIChar.IKInfo __result) => AssignNodeColor(__result.guideObject);

		[HarmonyPatch(typeof(GuideBase), "ConvertColor")]
		[HarmonyPrefix]
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
				PulseEffect.AddEffect(__result.guideSelect.transform);
			}
		}
	}
}