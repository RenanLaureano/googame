using System.Collections.Generic;
using System;



[Serializable]
public class SaveData
{
    public DataGame dataGame;
    public Medicamentos medicamentos;
    public DataComportamento dataComportamento;

    public SaveData()
    {
        dataGame = new DataGame();
        dataComportamento = new DataComportamento();
        medicamentos = new Medicamentos();
    }
}

[Serializable]
public class DataGame
{
    public List<FoodItem> foods;
    public int coins;
    public float saude;
    public float energia;
    public bool dormindo;
    public float alimentacao;
    public float diversao;
    public float higiene;
    public string lastTimeOn;

    public DataGame()
    {
        foods = new List<FoodItem>();
        coins = 200;
        saude = 100;
        energia = 100;
        dormindo = false;
        alimentacao = 100;
        diversao = 100;
        higiene = 100;
        lastTimeOn = UnityEngine.JsonUtility.ToJson((JsonDateTime)DateTime.Now); 
    }
}

[Serializable]
public class FoodItem
{
    public string Tipo;
    public string Comida;
    public int Quantidade;
}


//SAUDE

[Serializable]
public class Medicamentos
{
    public List<MedicamentoObject> medicamentos;

    public Medicamentos()
    {
        medicamentos = new List<MedicamentoObject>();
        medicamentos.Add(new MedicamentoObject("Abraxane", 50));
        medicamentos.Add(new MedicamentoObject("Anastrozol", 30));
        medicamentos.Add(new MedicamentoObject("Adriblastina RD", 40));
    }
}

[Serializable]
public class MedicamentoObject
{
    public string Nome;
    public int Quantidade;
    public int Icone;

    public MedicamentoObject(string Nome, int Quantidade)
    {
        this.Nome = Nome;
        this.Quantidade = Quantidade;
        this.Icone = 0;
    }
}


//COMPORTAMENTO
[Serializable]
public class LogInteracao
{
    public string Objeto;
    public string Descricao;
    public string Local;
    public string Hora;

    public LogInteracao(string Objeto, string Descricao, string Local, DateTime Hora)
    {
        this.Objeto = Objeto;
        this.Descricao = Descricao;
        this.Local = Local;
        this.Hora = UnityEngine.JsonUtility.ToJson((JsonDateTime)Hora);
    }
}

[Serializable]
public class LogAcao
{
    public string Acao;
    public string Descricao;
    public string Hora;
    public Medidores Medidor;
    public bool correto;

    public LogAcao(string Acao, string Descricao, DateTime Hora, Medidores Medidor, bool correto = true)
    {
        this.Acao = Acao;
        this.Descricao = Descricao;
        this.Medidor = Medidor;
        this.correto = correto;
        this.Hora = UnityEngine.JsonUtility.ToJson((JsonDateTime)Hora);
    }
}

[Serializable]
public class LogLocais
{
    public string Local;
    public string LocalAnterior;
    public string Descricao;
    public string HoraEntrada;
    public string HoraSaida;
    public string Hora;

    public LogLocais(string Local, string LocalAnterior, string Descricao, DateTime HoraEntrada, DateTime HoraSaida)
    {
        long epochTicks = new DateTime(1970, 1, 1).Ticks;
        long unixTime = ((DateTime.UtcNow.Ticks - epochTicks) / TimeSpan.TicksPerSecond);

        this.Local = Local;
        this.LocalAnterior = LocalAnterior;
        this.Descricao = Descricao;
        this.HoraEntrada = HoraEntrada.ToLongString();
        this.HoraSaida = HoraSaida.ToLongString();
        this.Hora = DateTime.Now.ToLongString();
    }
}

[Serializable]
public class DataComportamento
{
    public List<LogInteracao> logInteracao;
    public List<LogLocais> logLocais;
    public List<LogAcao> logAcoes;

    public DataComportamento()
    {
        logInteracao = new List<LogInteracao>();
        logLocais = new List<LogLocais>();
        logAcoes = new List<LogAcao>();
    }
}