using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveGameController : MonoBehaviour
{

    //Path to save data game
    private string dataPathSaveGame = string.Empty;

    //Object save data game
    public SaveData dataGame;
    //Instance dataController
    public static SaveGameController Instance = null;

    private void Awake()
    {
        if (SaveGameController.Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);       
    }

    void Start()
    {
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
        {
            dataPathSaveGame = "savedgame.json";
        }
        else
        {
            dataPathSaveGame = Application.persistentDataPath + "/savedgame.json";
        }

        LoadSavedGame();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        DataGame dt = dataGame.dataGame;
    }
    public void SaveGame(bool preSaveGameData = true)
    {
        if (dataPathSaveGame == string.Empty)
            return;

        if(preSaveGameData)
            PreSaveGameData();
        string dataAsJson = JsonUtility.ToJson(dataGame);
        File.WriteAllText(dataPathSaveGame, dataAsJson);
    }
    
    private void PreSaveGameData()
    {
        DataGame dt = dataGame.dataGame;
        GameController gc = GameController.instance;

        dt.lastTimeOn = JsonUtility.ToJson((JsonDateTime)DateTime.Now);

        dt.saude = gc.saude;
        dt.energia = gc.energia;
        dt.dormindo = gc.dormindo;
        dt.alimentacao = gc.alimentacao;
        dt.diversao = gc.diversao;
        dt.higiene = gc.higiene;
    }

    public void ResetData()
    {
        dataGame = CreateData();
        SaveGame();
    }

    private void LoadSavedGame()
    {
        if (File.Exists(dataPathSaveGame))
        {
            string dataAsJson = File.ReadAllText(dataPathSaveGame);
            dataGame = JsonUtility.FromJson<SaveData>(dataAsJson);
        }
        else
        {
            dataGame = CreateData();
        }
    }

    private SaveData CreateData()
    {
        SaveData _game = new SaveData();
        return _game;
    }

    /// <summary>
    /// Adionar comportamento a lista de comportamentos, setar o tipo e o comportamento
    /// </summary>
    public void AddComportamento(ComportamentosType comportamentosType, object comportamento, bool inGame = true)
    {
        switch (comportamentosType)
        {
            case ComportamentosType.item:
                dataGame.dataComportamento.logInteracao.Add((LogInteracao)comportamento);
                break;
            case ComportamentosType.troca_tela:
                dataGame.dataComportamento.logLocais.Add((LogLocais)comportamento);
                break;
            case ComportamentosType.acao:
                dataGame.dataComportamento.logAcoes.Add((LogAcao)comportamento);
                break;
            default:
                Debug.LogError("TIPO NÃO ENCONTRADO!!!!");
                break;
        }
        if(inGame)
            SaveGame();
    }

    private void OnApplicationPause(bool pause)
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (GameController.instance != null)
            {
                GameController.instance.LastLocalUpdate();
            }
            ServerConnection.Instance.SaveBD();
            this.SaveGame();
        }
    }
    
    public void OnApplicationQuit()
    {
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
        {
            if(GameController.instance != null)
            {
                GameController.instance.LastLocalUpdate();
            }
            ServerConnection.Instance.SaveBD();
            this.SaveGame();
        }
    }
}

struct JsonDateTime
{
    public long value;
    public static implicit operator DateTime(JsonDateTime jdt)
    {
        return DateTime.FromFileTimeUtc(jdt.value).ToLocalTime();
    }
    public static implicit operator JsonDateTime(DateTime dt)
    {
        JsonDateTime jdt = new JsonDateTime();
        jdt.value = dt.ToFileTimeUtc();
        return jdt;
    }
}