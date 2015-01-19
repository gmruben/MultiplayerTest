using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class UIButton : MonoBehaviour
{
	public event Action onClick;

	private Button _button;
	private Text _text;

	private Color normalTextColor = Color.white;
	private Color highlightedTextColor = Color.yellow;
	private Color pressedTextColor = Color.grey;
	private Color disabledTextColor = Color.grey;

	private bool isActive = true;
	private bool hasText = true;

	public void setText(string text)
	{
		this.text.text = text;
	}

	public void setActive(bool isActive)
	{
		this.isActive = isActive;

		if (text != null && hasText)
		{
			text.color = isActive ? normalTextColor : disabledTextColor;
		}

		button.interactable = isActive;
	}

	public void onButtonClick()
	{
		if (onClick != null) onClick();
	}

	public void onMouseEnter()
	{
		if (text != null && hasText && isActive)
		{
			text.color = highlightedTextColor;
		}
	}

	public void onMouseLeave()
	{
		if (text != null && hasText && isActive)
		{
			text.color = normalTextColor;
		}
	}

	public void onMouseDown()
	{
		if (text != null && hasText && isActive)
		{
			text.color = pressedTextColor;
		}
	}
	
	public void onMouseUp()
	{
		if (text != null && hasText && isActive)
		{
			text.color = normalTextColor;
		}
	}

	private Button button
	{
		get
		{
			if (_button == null) _button = GetComponent<Button>();
			return _button;
		}
		
		set
		{
			if (_button == null) _button = GetComponent<Button>();
			_button = value;
		}
	}

	private Text text
	{
		get
		{
			if (_text == null && hasText) 
			{
				_text = GetComponentInChildren<Text>();
				if (_text == null) hasText = false;
			}

			return _text;
		}

		set
		{
			if (_text == null && hasText)
			{
				_text = GetComponentInChildren<Text>();
				if (_text == null) hasText = false;
			}

			_text = value;
		}
	}
}