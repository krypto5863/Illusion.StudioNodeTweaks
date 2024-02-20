using Studio;
using UnityEngine;

namespace StudioNodeTweaks
{
	internal class NodeColorizer : MonoBehaviour
	{
		internal static NodeColorizer AddComponent(Transform transform)
		{
			return transform.gameObject.AddComponent<NodeColorizer>();
		}

		private static void AssignNodeColor(GuideObject guideObject)
		{
			if (StudioNodeTweaks._pluginInstance == null)
			{
				return;
			}

			StudioNodeTweaks._pluginLogger.LogInfo("Getting Transform target and name.");

			var transformName = guideObject?.transformTarget?.name;

			if (string.IsNullOrEmpty(transformName))
			{
				return;
			}

			StudioNodeTweaks._pluginLogger.LogInfo("Getting color");

			if (StudioNodeTweaks._pluginInstance.ColorConfigDictionary.TryGetValue(transformName.ToLower(),
				    out var config))
			{
				StudioNodeTweaks._pluginLogger.LogInfo("Color found! Applying...");

				guideObject.guideSelect.color = config.Value;
				config.SettingChanged += (sender, args) => { AssignNodeColor(guideObject); };
			}

			/*
			StudioNodeTweaks._pluginLogger.LogInfo("Nothing... gonna check if it's a root bone...");
	
			if (transformName.StartsWith("ChaF"))
			{
				guideObject.guideSelect.color = new Color(1f,0f,0.9f);
			}
			else if(transformName.StartsWith("ChaM"))
			{
				guideObject.guideSelect.color = new Color(0.3f, 1f, 1f);
			}
			*/
		}

		private GuideObject _guideObject;

		public void Awake()
		{
			var guideSelect = GetComponent<GuideSelect>();
			_guideObject = guideSelect.guideObject;
			AssignNodeColor(_guideObject);
		}
	}
}