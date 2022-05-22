using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderScript : MonoBehaviour
{
    [SerializeField]
    private Slider VolumeSlider;
    [SerializeField]
    private TextMeshProUGUI VolumeValue;


    void Start()
    {
        VolumeSlider.onValueChanged.AddListener((v) => {
            VolumeValue.text = v.ToString("0");
        });
    }

    void Update()
    {
        
    }
}
