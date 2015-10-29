using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		var data = new WindowDataVO("Hello!", "Hey! It's a first window!", "OK");
		var data2 = new WindowDataVO("Hello!", "Hey! It's a second window!", "OK");
		WindowsManager.Instance.AddWindow(WindowsManager.TEST_WINDOW, data);
		WindowsManager.Instance.AddWindow(WindowsManager.TEST_WINDOW, data);
		WindowsManager.Instance.AddWindow(WindowsManager.TEST_WINDOW, data2);
	}
}
