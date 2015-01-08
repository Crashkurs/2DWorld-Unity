using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundSlider : MonoBehaviour {

	private Slider slider;

	public SliderType sliderType;

	public enum SliderType
	{
		Sound,
		Music
	}

	public void Start()
	{
		slider = gameObject.GetComponent<Slider> ();
		if (sliderType == SliderType.Music)
						slider.value = (float)Configuration.Properties.backgroundVolumneLevel;

		if (sliderType == SliderType.Sound)
						slider.value = (float)Configuration.Properties.foregroundVolumeLevel;
	}

	public void SoundValueChanged()
	{
		slider = gameObject.GetComponent<Slider> ();
		Configuration.Properties.foregroundVolumeLevel = (int)slider.value;
	}

	public void MusicValueChanged()
	{
		slider = gameObject.GetComponent<Slider> ();
		Configuration.Properties.backgroundVolumneLevel = (int)slider.value;
	}
}
