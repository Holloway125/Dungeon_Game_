using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MixerController : MonoBehaviour
{
    //Serialized Sliders
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider BGMVolumeSlider;
    [SerializeField] private Slider SFXVolumeSlider;
    [SerializeField] private Slider ambientVolumeSlider;
    [SerializeField] private Slider dialogueVolumeSlider;
    
    //Serialize Volume Values
    [SerializeField] private TextMeshProUGUI masterVolumeValue;
    [SerializeField] private TextMeshProUGUI BGMVolumeValue;
    [SerializeField] private TextMeshProUGUI SFXVolumeValue;
    [SerializeField] private TextMeshProUGUI ambientVolumeValue;
    [SerializeField] private TextMeshProUGUI dialogueVolumeValue;
  
//to load playerprefs of audio
    private void Start()
    {
//Master load methods on start
            masterVolumeSlider.onValueChanged.AddListener((mv) => {
            masterVolumeValue.text = mv.ToString("0.0");
        });
                if (!PlayerPrefs.HasKey("MasterVolume"))
        {
            PlayerPrefs.SetFloat("MasterVolume", 1);
            MasterLoad();
        }
        else
        {
            MasterLoad();
        }
//BGM load methods on start
        BGMVolumeSlider.onValueChanged.AddListener((bv) => {
            BGMVolumeValue.text = bv.ToString("0.0");
        });
                if (!PlayerPrefs.HasKey("BGMVolume"))
        {
            PlayerPrefs.SetFloat("BGMVolume", 1);
            BGMLoad();
        }
        else
        {
            BGMLoad();
        }
//SFX load methods on start
                SFXVolumeSlider.onValueChanged.AddListener((sv) => {
            SFXVolumeValue.text = sv.ToString("0.0");
        });
                if (!PlayerPrefs.HasKey("SFXVolume"))
        {
            PlayerPrefs.SetFloat("SFXVolume", 1);
            SFXLoad();
        }
        else
        {
            SFXLoad();
        }
//Ambient load methods on start
                ambientVolumeSlider.onValueChanged.AddListener((av) => {
            ambientVolumeValue.text = av.ToString("0.0");
        });
                if (!PlayerPrefs.HasKey("AmbientVolume"))
        {
            PlayerPrefs.SetFloat("AmbientVolume", 1);
            AmbientLoad();
        }
        else
        {
            AmbientLoad();
        }
//Dialogue load methods on start
                dialogueVolumeSlider.onValueChanged.AddListener((dv) => {
            dialogueVolumeValue.text = dv.ToString("0.0");
        });
                if (!PlayerPrefs.HasKey("DialogueVolume"))
        {
            PlayerPrefs.SetFloat("DialogueVolume", 1);
            DialogueLoad();
        }
        else
        {
            DialogueLoad();
        }
    }
      
//Serialized Mixers
[SerializeField] private AudioMixer masterMixer;
[SerializeField] private AudioMixer BGMMixer;
[SerializeField] private AudioMixer SFXMixer;
[SerializeField] private AudioMixer ambientMixer;
[SerializeField] private AudioMixer dialogueMixer;
    public void SetMasterVolume(float masterSliderValue)
    {
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(masterSliderValue) * 20);
        MasterSave();
    }
        public void SetBGMVolume(float BGMSliderValue)
    {
        BGMMixer.SetFloat("BGMVolume", Mathf.Log10(BGMSliderValue) * 20);
        BGMSave();
    }
        public void SetSFXVolume(float SFXSliderValue)
    {
        SFXMixer.SetFloat("SFXVolume", Mathf.Log10(SFXSliderValue) * 20);
        SFXSave();
    }
        public void SetAmbientVolume(float ambientSliderValue)
    {
        ambientMixer.SetFloat("AmbientVolume", Mathf.Log10(ambientSliderValue) * 20);
        AmbientSave();
    }
        public void SetDialogueVolume(float dialogueSliderValue)
    {
        dialogueMixer.SetFloat("DialogueVolume", Mathf.Log10(dialogueSliderValue) * 20);
        DialogueSave();
    }
//Master Volume Save and Load methods
    private void MasterLoad()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
    }

        public void MasterSave()
    {   
       PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
    }
//BGMVolume Save and Load methods
        private void BGMLoad()
    {
        BGMVolumeSlider.value = PlayerPrefs.GetFloat("BGMVolume");
    }

        public void BGMSave()
    {   
       PlayerPrefs.SetFloat("BGMVolume", BGMVolumeSlider.value);
    }
//SFX Volume Save and Load methods   
        private void SFXLoad()
    {
        SFXVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }

        public void SFXSave()
    {   
       PlayerPrefs.SetFloat("SFXVolume", SFXVolumeSlider.value);
    }
//Ambient Volume Save and Load methods  

    private void AmbientLoad()
    {
        ambientVolumeSlider.value = PlayerPrefs.GetFloat("AmbientVolume");
    }

        public void AmbientSave()
    {   
       PlayerPrefs.SetFloat("AmbientVolume", ambientVolumeSlider.value);
    }
//Dialogue Volume Save and Load methods
        private void DialogueLoad()
    {
        dialogueVolumeSlider.value = PlayerPrefs.GetFloat("DialogueVolume");
    }

        public void DialogueSave()
    {   
       PlayerPrefs.SetFloat("DialogueVolume", dialogueVolumeSlider.value);
    }
    
}
