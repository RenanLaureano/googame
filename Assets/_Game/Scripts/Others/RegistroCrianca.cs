using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegistroCrianca : MonoBehaviour
{

    public GameObject formEstudo;

    private string nome;
    private int idade;
    private string sexo;
    private string hospital;
    private bool estuda;
    private string nomeEscola;
    private int ano;
    private string entrada;
    private string saida;

    public void setNome(InputField text)
    {
        nome = text.text;
    }

    public void setIdade(InputField text)
    {
        idade = int.Parse(text.text);
    }

    public void setSexo(string text)
    {
        sexo = text;
        print("sexu: " + sexo);
    }

    public void setHospital(InputField text)
    {
        hospital = text.text;
    }

    public void setEstuda(Toggle toggle)
    {
        estuda = toggle.isOn;
        formEstudo.SetActive(estuda);
    }

    public void setNomeEscola(InputField text)
    {
        nomeEscola = text.text;
    }

    public void setAno(InputField text)
    {
        ano = int.Parse(text.text);
    }

    public void setEntrada(InputField text)
    {
        entrada = text.text;
    }

    public void setSaida(InputField text)
    {
        saida = text.text;
    }

}
