using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MixerController : MonoBehaviour
{
       [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private TextMeshProUGUI VolumeValue;

    private void Start()
    {

        volumeSlider.onValueChanged.AddListener((v) => {
            VolumeValue.text = v.ToString("0.0");
        });
                if (!PlayerPrefs.HasKey("BGMVolume"))
        {
            PlayerPrefs.SetFloat("BGMVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }
      

    [SerializeField] private AudioMixer mainMixer;
    
    public void SetVolume(float sliderValue)
    {
        mainMixer.SetFloat("BGMVolume", Mathf.Log10(sliderValue) * 20);
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("BGMVolume");
    }

        public void Save()
    {   
       PlayerPrefs.SetFloat("BGMVolume", volumeSlider.value);
    }
    
}