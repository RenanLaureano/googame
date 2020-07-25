using UnityEngine;
using UnityEngine.UI;

public class GainPrefab : MonoBehaviour {
    public Text value;
    public Image icone;

    public void config(Gain gain)
    {
        value.text = (gain.value > 0 ? "+" : "") + gain.value.ToString();
        icone.sprite = gain.icon;
    }
}
