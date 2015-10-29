using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using DG.Tweening;
using DG.Tweening.Core.Easing;

public class BaseWindow : MonoBehaviour 
{
	public Button OkButton;
	public Button CancelButton;
	public Text Title;
	public Text Subtitle;
	public GameObject AnimatedPart;

	public WindowDataVO Data;

	public Action<GameObject> OnOkAction;
	public Action<GameObject> OnCancelAction;
	public Action<BaseWindow> OnCompleteStartAnimate;
	public Action<BaseWindow> OnCompleteEndAnimate;

	public void Awake()
	{
		ResetWindow();
		StartAnimate();
	}

	void OnEnable()
	{
		ResetWindow();
		StartAnimate();
	}

	public void OnOk()
	{
		EndAnimate();
		if(OnOkAction != null)
			OnOkAction(gameObject);
	}

	public void OnCancel()
	{
		EndAnimate();
		if(OnCancelAction != null)
			OnCancelAction(gameObject);
	}

	private void StartAnimate()
	{
		AnimatedPart.transform.DOLocalMoveY(0, 1f).SetEase(Ease.InOutBack).OnComplete(() => {
			if(OnCompleteStartAnimate != null)
				OnCompleteStartAnimate(this);
		});
	}
	private void EndAnimate()
	{
		AnimatedPart.transform.DOMoveY(-500f, 1f).SetEase(Ease.InOutBack).OnComplete(() => {
			if(OnCompleteEndAnimate != null)
				OnCompleteEndAnimate(this);
		});
	}

	private void ResetWindow()
	{
		AnimatedPart.transform.position = new Vector2(transform.position.x, -500f);
	}

	public void SetData(WindowDataVO data)
	{
		Data = data;
		Title.text = data.TitleText;
		Subtitle.text = data.MessageText;
		OkButton.name = data.OkText;
	}
}
