using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour
{
    [Header("Effects")]
    public GameObject waterShower;

    public static EffectsController Instance;

    private void Awake()
    {
        if (EffectsController.Instance == null)
            Instance = this;
    }

    public void ActiveEffect(Effects effect, bool active)
    {
        switch (effect)
        {
            case Effects.water_shower:
                waterShower.SetActive(active);
                break;
        }
    }
}
