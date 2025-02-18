using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
	[SerializeField] private Slider slider;
	[SerializeField] private TextMeshProUGUI text;

	private void Start()
	{
		slider.onValueChanged.AddListener(value => text.text = Math.Round(value, 2).ToString());
	}
}
