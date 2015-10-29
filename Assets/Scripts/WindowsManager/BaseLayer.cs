using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BaseLayer : MonoBehaviour
{
	private LinkedList<BaseWindow> _queue = new LinkedList<BaseWindow>();
	private Dictionary<string, BaseWindow> _windows = new Dictionary<string, BaseWindow>();

	public void AddWindow(string windowName, string fullPath, WindowDataVO data)
	{
		var window = GetWindow(windowName, fullPath, data);
		AddToQueue(window, false);
	}

	public void SetWindow(string windowName, string fullPath, WindowDataVO data)
	{
		var window = GetWindow(windowName, fullPath, data);
		AddToQueue(window, true);
	}

	private BaseWindow GetWindow(string windowName, string fullPath, WindowDataVO data)
	{
		var windowNameWithHash = windowName + data.GetHashCode();

		if(_windows.ContainsKey(windowNameWithHash))
		{
			var window = _windows[windowNameWithHash];
			return window;
		}
		else
		{
			return CreateWindow(windowName, fullPath, data);
		}
	}

	private BaseWindow CreateWindow(string windowName, string fullPath, WindowDataVO data)
	{
		var windowNameWithHash = windowName + data.GetHashCode();
		var windowPrefab = Resources.Load(fullPath + windowName);
		GameObject go = Instantiate(windowPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		go.transform.localScale = Vector3.one;
		go.name = windowName;
		go.transform.SetParent(transform, false);
		go.SetActive(false);
		var window = go.GetComponent<BaseWindow>();
		window.SetData(data);
		_windows.Add(windowNameWithHash, window);
		return window;
	}

	private void AddToQueue(BaseWindow window, bool isShowImmidiatly)
	{
		if(isShowImmidiatly)
		{
			_queue.AddAfter(_queue.First, window);
			_queue.First.Value.OnCancel();
			_queue.First.Value.OnCompleteEndAnimate = null;
			_queue.First.Value.OnCompleteEndAnimate += OnCloseWindowImmediately;
		}
		else
		{
			_queue.AddLast(window);
			if(_queue.Count == 1)
				ShowWindow();
		}
	}

	private void ShowWindow()
	{
		if(_queue.Count > 0)
		{
			_queue.First.Value.gameObject.SetActive(true);
			_queue.First.Value.OnCompleteEndAnimate += OnCloseWindow;
		}
	}

	private void OnCloseWindow(BaseWindow window)
	{
		window.OnCompleteEndAnimate -= OnCloseWindow;
		window.gameObject.SetActive(false);
		_queue.RemoveFirst();
		ShowWindow();
	}

	private void OnCloseWindowImmediately(BaseWindow window)
	{
		window.OnCompleteEndAnimate -= OnCloseWindowImmediately;
		window.gameObject.SetActive(false);
		_queue.RemoveFirst();
		ShowWindow();
	}
}
