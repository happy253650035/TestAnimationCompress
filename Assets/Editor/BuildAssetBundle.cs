using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class BuildAssetBundle 
{
	[MenuItem ("Tools/BuildAnimationCompress")]
	static void BuildAnimationCompress()
	{
		BuildAb("Assets/AniCompress.prefab");
	}
	
	[MenuItem ("Tools/BuildAnimationUnCompress")]
	static void BuildAnimationUnCompress()
	{
		BuildAb("Assets/AniUnCompress.prefab");
	}
	
	[MenuItem ("Tools/BuildAnimationFakeCompress")]
	static void BuildAnimationFakeCompress()
	{
		BuildAb("Assets/AniFakeCompress.prefab");
	}
	
	[MenuItem ("Tools/BuildAnimationTrueCompress")]
	static void BuildAnimationTrueCompress()
	{
		BuildAb("Assets/AniTrueCompress.prefab");
	}

	static void BuildAb(string assetPath)
	{
		List<AssetBundleBuild> list = new List<AssetBundleBuild>();
		Object assetOb = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
		string path = AssetDatabase.GetAssetPath(assetOb);
		string[] needList = AssetDatabase.GetDependencies(new[] {path});
		foreach (string needPath in needList)
		{
			if (!needPath.Contains(".cs"))
			{
				AssetBundleBuild build = new AssetBundleBuild();
				string[] namesList = needPath.Split (new[]{ "." }, StringSplitOptions.RemoveEmptyEntries);
				build.assetBundleName = namesList[0];
				build.assetNames = new[] {needPath};
				list.Add(build);
			}
		}

		string filePath = Directory.GetCurrentDirectory() + "/AssetBundle";
		if (!Directory.Exists(filePath))
		{
			Directory.CreateDirectory(filePath);
		}

		BuildPipeline.BuildAssetBundles(Directory.GetCurrentDirectory() + "/AssetBundle", list.ToArray(),
			BuildAssetBundleOptions.ChunkBasedCompression | BuildAssetBundleOptions.DeterministicAssetBundle,
			BuildTarget.Android);
	}
}
