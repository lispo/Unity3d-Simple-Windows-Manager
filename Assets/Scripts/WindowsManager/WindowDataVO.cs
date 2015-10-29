using UnityEngine;
using System.Collections;

public class WindowDataVO  
{

	public WindowDataVO (string titleText, string messageText, string okText)
	{
		TitleText = titleText;		
		MessageText = messageText;		
		OkText = okText;		
	}

	public string TitleText { get; set; }
	public string MessageText { get; set; }
	public string OkText { get; set; }
}
