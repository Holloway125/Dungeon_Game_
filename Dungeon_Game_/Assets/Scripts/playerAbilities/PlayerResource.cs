using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResource : MonoBehaviour
{
    public Slider Health;
    public Slider Energy;
    public Slider Stamina;

    void FixedUpdate()
    {
        Stamina.value += .2f;
    }

}
