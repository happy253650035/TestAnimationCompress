using UnityEngine;
using UnityEditor;
using System;

public class ToolAnimationCompress : MonoBehaviour {
	[MenuItem("Tools/AnimationTool/CompressClip")]
	private static void Execute()
	{
		int number = 0;
		DateTime time = DateTime.Now;
		foreach (UnityEngine.Object o in Selection.GetFiltered(typeof(AnimationClip), SelectionMode.DeepAssets))
		{
			number++;
			ForFun(Instantiate(o) as AnimationClip, AssetDatabase.GetAssetPath(o));
		}
		AssetDatabase.SaveAssets();
		Debug.Log("一共压缩了"+number+"个动画文件!");
		Debug.Log("耗时:"+(DateTime.Now-time).TotalMilliseconds/1000+"秒.");
	}

	private static void ForFun(AnimationClip clip, string clipName)
	{
		EditorCurveBinding[] curveBindings = AnimationUtility.GetCurveBindings(clip);
		AnimationClipCurveData[] curves = new AnimationClipCurveData[curveBindings.Length];
		for (int index = 0; index < curves.Length; ++index)
		{
			curves[index] = new AnimationClipCurveData(curveBindings[index]);
			curves[index].curve = AnimationUtility.GetEditorCurve(clip, curveBindings[index]);
		}
		foreach(AnimationClipCurveData curveDate in curves)
		{
			var keyFrames = curveDate.curve.keys;
			for(int i=0;i<keyFrames.Length;i++)
			{
				var key = keyFrames[i];
				key.value = float.Parse(key.value.ToString("f3"));
				key.inTangent = float.Parse(key.inTangent.ToString("f3"));
				key.outTangent = float.Parse(key.outTangent.ToString("f3"));
				keyFrames[i] = key;
			}
			curveDate.curve.keys = keyFrames;
			clip.SetCurve(curveDate.path,curveDate.type,curveDate.propertyName,curveDate.curve);
		}
		AssetDatabase.CreateAsset(clip, clipName);
	}
}