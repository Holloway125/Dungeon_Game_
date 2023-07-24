using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MyUILibrary;

[RequireComponent(typeof(UIDocument))]
public class RadialProgressComponent : MonoBehaviour
{

    RadialProgress m_RadialProgress;
    [SerializeField] private LevelSystem levelSystem;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        m_RadialProgress = new RadialProgress() {
            style = {
                position = Position.Absolute,
                
            }
        };

        root.Add(m_RadialProgress);
        m_RadialProgress.progress = 0;
    }

    void Update()
    {
        // For demo purpose, give the progress property dynamic values.
        m_RadialProgress.progress = (float)levelSystem.GetXpToNextLvl() / (float)levelSystem.GetTotalXp();
    }
}