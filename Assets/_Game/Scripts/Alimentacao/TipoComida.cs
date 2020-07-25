using UnityEngine;

[CreateAssetMenu(fileName = "TipoComidaData", menuName = "Tipo Comida")]
[System.Serializable]
public class TipoComida : ScriptableObject
{
    public string nome = "Novo Tipo Comida";
    public Comida[] comidas;
}

[System.Serializable]
public class Comida
{
    public string name = "Comida";
    public int value = 0;
    public Sprite icon;
    public Gain[] gains;
    public bool bom = true;
}

[System.Serializable]
public class Gain {
    public Medidores gainType;
    public int value = 0;
    public Sprite icon;
}

