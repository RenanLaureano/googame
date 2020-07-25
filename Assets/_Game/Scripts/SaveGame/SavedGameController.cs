using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using System;

public class SavedGameController : MonoBehaviour
{

    private string dataPathSaveGame = string.Empty;

    public SaveGameXML game;

    public static SavedGameController instance;


    private void Awake()
    {
        if (SavedGameController.instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {

        if (Application.platform == RuntimePlatform.Android)
            dataPathSaveGame = Application.persistentDataPath + "/savedgame.xml";
        else
            dataPathSaveGame = "savedgame.xml";

        LoadSavedGame();
        DontDestroyOnLoad(gameObject);
    }

    public void SaveXML()
    {
        game.lastTimeOn = DateTime.Now;
        GetMedidoresToSave();

        XmlSerializer serializer = new XmlSerializer(game.GetType());
        FileStream stream = new FileStream(dataPathSaveGame, FileMode.Create);
        serializer.Serialize(stream, game);
        stream.Close();
    }

    private void LoadSavedGame()
    {
        try
        {
            SaveGameXML _game = null;

            if (File.Exists(dataPathSaveGame))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SaveGameXML));
                StreamReader reader = new StreamReader(dataPathSaveGame);
                _game = (SaveGameXML)serializer.Deserialize(reader.BaseStream);
                reader.Close();
            }
            
            game = (_game != null) ? _game : InitializeComportamento();

        }
        catch (IOException e)
        {
            Debug.LogError(e);
            game = InitializeComportamento();
        };
    }

    private SaveGameXML InitializeComportamento()
    {
        SaveGameXML _game = new SaveGameXML();
        _game.foods = new List<FoodItemXML>();
        _game.coins = 100;
        _game.saude = 100;
        _game.energia = 100;
        _game.dormindo = true;
        _game.alimentacao = 100;
        _game.diversao = 100;
        _game.higiene = 100;
        _game.lastTimeOn = DateTime.Now;
        return _game;
    }

    private void GetMedidoresToSave()
    {
        GameController gc = GameController.instance;

        game.saude = gc.saude;
        game.energia = gc.energia;
        game.dormindo = gc.dormindo;
        game.alimentacao = gc.alimentacao;
        game.diversao = gc.diversao;
        game.higiene = gc.higiene;
    }

    private void OnApplicationPause(bool pause)
    {
        if (Application.platform == RuntimePlatform.Android)
            this.SaveXML();
    }
    public void OnApplicationQuit()
    {
        if (Application.platform != RuntimePlatform.Android)
            this.SaveXML();
    }
}

