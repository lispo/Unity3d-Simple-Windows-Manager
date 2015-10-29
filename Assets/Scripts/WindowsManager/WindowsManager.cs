using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WindowsManager : MonoBehaviour 
{
	private static WindowsManager _instance;

	//Layers
	public static string FIRST_LAYER = "FIRST_LAYER";
	public static string SECOND_LAYER = "SECOND_LAYER";

	//Windows
	public static string TEST_WINDOW = "TestWindow";
	public static string TEST_WINDOW_2 = "TestWindow2";

	private Dictionary<string, BaseLayer> _layers = new Dictionary<string, BaseLayer>();
	private Dictionary<string, KeyValuePair<string, string>> _windowsBindedWithLayers = new Dictionary<string, KeyValuePair<string, string>>();

	public static WindowsManager Instance
	{
		get { return _instance; }
	}
	
	void Awake()
	{
		if (_instance != null)
		{
			Destroy(this);
			return;
		}

		Init();
		_instance = this;
		DontDestroyOnLoad(transform.gameObject);
	}

	private void Init()
	{
		RegisterLayers();
		RegisterWindows();
	}

	#region Layers

	private void RegisterLayers()
	{
		AddLayer(FIRST_LAYER);
		AddLayer(SECOND_LAYER);
	}

	private void AddLayer(string layerName)
	{
		var go = new GameObject();
		go.name = layerName;
		go.transform.SetParent(transform, false);

		var rectTransform = go.AddComponent<RectTransform>();
		rectTransform.anchorMin = new Vector2(0, 0);
		rectTransform.anchorMax = new Vector2(1, 1);
		rectTransform.pivot = new Vector2(0.5f, 0.5f);

		var layer = go.AddComponent<BaseLayer>();
		_layers.Add(layerName, layer);

	}

	#endregion

	#region Windows
	
	private void RegisterWindows()
	{
		RegisterWindow(TEST_WINDOW, FIRST_LAYER, "Prefabs/Windows/");
		RegisterWindow(TEST_WINDOW_2, FIRST_LAYER, "Prefabs/Windows/");
	}

	private void RegisterWindow(string windowName, string layerName, string fullPath)
	{
		_windowsBindedWithLayers.Add(windowName, new KeyValuePair<string, string>(layerName, fullPath));
	}	

	public void AddWindow(string windowName, WindowDataVO data)
	{
		var layerName = _windowsBindedWithLayers[windowName].Key;
		var fullPath = _windowsBindedWithLayers[windowName].Value;
		_layers[layerName].AddWindow(windowName, fullPath, data);
	}

	public void SetWindow(string windowName, WindowDataVO data)
	{
		var layerName = _windowsBindedWithLayers[windowName].Key;
		var fullPath = _windowsBindedWithLayers[windowName].Value;
		_layers[layerName].SetWindow(windowName, fullPath, data);
	}
	
	#endregion
}
