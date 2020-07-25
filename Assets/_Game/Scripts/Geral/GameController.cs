using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Locais Config")]
    public float timeToChange = 0.5f;
    public RectTransform mainTween;
    public Button buttonLeft;
    public Button buttonRight;

    [Header("Coins Settings")]
    //Coins
    public int coins = 100;
    public Text textCoins;

    //FIM COINS

    [Header("Actions Game")]
    //Actions
    public GameObject efeitosPia;
    //FIM Actions

    [Header("Saude Settings")]
    //SAUDE
    public string tempoSaude;
    public BarScale barSaude;

    private bool doente;
    public float saude = 100;
    private float gastSaude;
    //FIM SAUDE

    [Header("Energia Settings")]
    //ENERGIA
    public string tempoEnergia;
    public BarScale barEnergia;
    public string tempoRecuperarEnergia;

    private bool sono;
    public bool dormindo;
    public GameObject alphaSleep;
    public float energia = 100;
    private float gastEnergia;
    private float gainEnergia;
    //FIM ENERGIA
    [Header("Alimentacao Settings")]
    // ALIMENTACAO
    public string tempoAlimentacao;
    public BarScale barAlimentacao;

    public float alimentacao = 100;
    private float gastAlimentacao;
    //FIM ALIMENTACAO

    [Header("Higiene Settings")]
    // HIGIENE
    public string tempoHigiene;
    public BarScale barHigiene;
    public Image[] dirts;
    public float higiene = 100;
    public float timeToPP = 2;
    public Transform spawnPP;
    public Transform prefabPP;
    public Transform leftLimitPP, rightLimitPP, downLimitPP, upLimitPP;
    private List<Transform> pps = new List<Transform>();
    private float gastHigiene;
    private bool sujo;
    //FIM HIGIENE

    private bool triste;
    [Header("Divercao Settings")]
    // DIVERCAO
    public string tempoDiversao;
    public BarScale barDivercao;

    private bool feliz;
    public float diversao = 100;
    private float gastDiversao;
    //FIM DIVERCAO

    [Header("Freezer Settings")]
    // FREEZER
    public Image imageFreezer;
    public Sprite spriteFreezerOpen;
    public Sprite spriteFreezerClose;
    private TipoComida[] tiposComida;
    private bool freezerOpen;
    //FIM FREEZER

    //LADO DE FORA
    private bool inOut = false;
    //FIM LADO DE FORA


    private float contadorMedidores = 0;

    private int current_local = 2;
    private static LogLocais lastLocal = null;

    public static GameController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private bool inGame = false;

    void Start()
    {
        //UnityEngine.iOS.LocalNotification.SendNotification(1, 5000, "Bem vindo!", "Esperamos que goste.", new Color32(0xff, 0x44, 0x44, 255));

        gastSaude = CalculaGastoTempo(tempoSaude);
        gastEnergia = CalculaGastoTempo(tempoEnergia);
        gainEnergia = CalculaGastoTempo(tempoRecuperarEnergia);
        gastAlimentacao = CalculaGastoTempo(tempoAlimentacao);
        gastHigiene = CalculaGastoTempo(tempoHigiene);
        gastDiversao = CalculaGastoTempo(tempoDiversao);

        tiposComida = Resources.LoadAll<TipoComida>("Comidas");

        SetLocal(Locais.Sala, true);

        this.ActionLoop(timeToPP * 3600, InstantiatePP);

        this.ActionAfter(1f, LoadGameSaved);
    }

    private float CalculaGastoTempo(string tempo)
    {
        tempo = tempo.ToUpper();

        string[] t = tempo.Split(' ');
        float time = float.Parse(t[0]);


        time *= tempo.Contains("M") ? 60f : 3600f;
        return 100 / time;
    }

    void Update()
    {
        if(inGame)
        MedidoresUpdate();
    }

    public void LoadGameSaved()
    {

        DataGame dt = SaveGameController.Instance.dataGame.dataGame;

        int coins = dt.coins;
        float saude = dt.saude;
        float energia = dt.energia;
        bool dormindo = dt.dormindo;
        float alimentacao = dt.alimentacao;
        float diversao = dt.diversao;
        float higiene = dt.higiene;
        DateTime lastTimeOn = JsonUtility.FromJson<JsonDateTime>(dt.lastTimeOn);

        Debug.Log("Goo Log || Last time on :" + lastTimeOn);
        float pastSeconds = (float)((DateTime.Now - lastTimeOn).TotalSeconds);

        this.coins = coins;

        this.saude = saude - (pastSeconds * gastSaude);
        if (!dormindo)
            this.energia = energia - (pastSeconds * gastEnergia);
        else
            this.energia = energia + (pastSeconds * gainEnergia);
        this.alimentacao = alimentacao - (pastSeconds * gastAlimentacao);
        this.diversao = diversao - (pastSeconds * gastDiversao);
        this.higiene = higiene - (pastSeconds * gastHigiene);

        this.dormindo = dormindo;
        alphaSleep.SetActive(dormindo);
        if (dormindo)
        {
            Player.instance.p_animator.SetTrigger("goSleep");
            SetLocal(Locais.Quarto);
        }
        else
        {
            Player.instance.p_animator.SetTrigger("wakeUp");
        }

        this.textCoins.text = this.coins.ToString();
        OnLoadInstatiatePP(pastSeconds);
        SetBarras();
        inGame = true;
        FadeInOut.instance.FadeOut(1, 1);
    }

    private void MedidoresUpdate()
    {
        contadorMedidores += Time.deltaTime;

        if (contadorMedidores >= 1)
        {

            if (saude > 0)
            {
                saude -= (saude - gastSaude >= 0) ? gastSaude : saude;


                if (saude < 50 && !doente)
                {
                    doente = true;
                    Player.instance.p_animator.SetBool("doente", true);
                }
                else if (saude >= 50 && doente)
                {
                    doente = false;
                    Player.instance.p_animator.SetBool("doente", false);
                }

            }

            if (energia > 0 && !dormindo)
            {
                energia -= (energia - gastEnergia >= 0) ? gastEnergia : energia;
            }
            else if (energia < 100 && dormindo)
            {
                energia += (energia + gainEnergia < 100) ? gainEnergia : 100 - energia;
            }

            if (energia < 30 && !sono)
            {
                sono = true;
                Player.instance.p_animator.SetBool("sleep", true);
            }
            else if (energia >= 30 && sono)
            {
                sono = false;
                Player.instance.p_animator.SetBool("sleep", false);
            }

            if (alimentacao > 0)
            {
                alimentacao -= (alimentacao - gastAlimentacao >= 0) ? gastAlimentacao : alimentacao;
            }

            if (higiene > 0)
            {
                higiene -= (higiene - gastHigiene >= 0) ? gastHigiene : higiene;
            }
            SujeuraUpdate();

            if (diversao > 0)
            {
                diversao -= (diversao - gastDiversao >= 0) ? gastDiversao : diversao;
            }

            if (diversao < 80 && feliz)
            {
                feliz = false;
                Player.instance.p_animator.SetBool("happy", false);
            }
            else if (diversao >= 80 && !feliz)
            {
                feliz = true;
                Player.instance.p_animator.SetBool("happy", true);
            }

            if ((diversao < 40 || higiene < 30 || alimentacao < 40) && !triste)
            {
                triste = true;
                Player.instance.p_animator.SetBool("sad", true);
            }
            else if (diversao >= 40 && higiene >= 30 && alimentacao >= 40 && triste)
            {
                triste = false;
                Player.instance.p_animator.SetBool("sad", false);
            }

            saude = saude < 0 ? 0 : saude;
            energia = energia < 0 ? 0 : energia;
            alimentacao = alimentacao < 0 ? 0 : alimentacao;
            higiene = higiene < 0 ? 0 : higiene;
            diversao = diversao < 0 ? 0 : diversao;

            SetBarras();

            contadorMedidores = 0;

        }
    }

    private void SujeuraUpdate()
    {

        int div = (int)(50 / dirts.Length);

        for (int x = 0; x < dirts.Length; x++)
        {
            bool isDirt = higiene <= 50 - x * div;
            if (isDirt && dirts[x].color == new Color(1, 1, 1, 0))
            {
                dirts[x].DOColor(new Color(1, 1, 1, 0.4f), 0.5f);

            }
            else if (!isDirt && dirts[x].color == new Color(1, 1, 1, 0.4f))
            {
                dirts[x].DOColor(new Color(1, 1, 1, 0), 0.5f);
            }
        }
    }

    private void SetBarras()
    {
        barSaude.SetScale(100, saude);
        barEnergia.SetScale(100, energia);
        barAlimentacao.SetScale(100, alimentacao);
        barDivercao.SetScale(100, diversao);
        barHigiene.SetScale(100, higiene);
    }

    /// <summary>
    /// Salvar Comportamento de um clique em um item
    /// </summary>
    public void SaveComportamento(ComportamentosType comportamentosType, object comportamento)
    {
        switch (comportamentosType)
        {
            case ComportamentosType.item:
                SaveGameController.Instance.AddComportamento(comportamentosType, comportamento);
                break;
            default:
                Debug.LogError("TIPO NÃO ENCONTRADO!!!!");
                break;
        }
    }

    public string GetLocal()
    {
        return ((Locais)current_local).ToString();
    }

    private void OnLoadInstatiatePP(float pastSeconds)
    {
        int qnt = (int)(pastSeconds / (3600 * timeToPP));
        qnt = qnt > 15 ? 15 : qnt;
        for (int x = 0; x < qnt; x++)
            InstantiatePP();
    }

    private void InstantiatePP()
    {
        if (pps.Count > 15)
            return;
        float x = UnityEngine.Random.Range(leftLimitPP.position.x, rightLimitPP.position.x);
        float y = UnityEngine.Random.Range(downLimitPP.position.y, upLimitPP.position.y);
        Vector3 pos = new Vector3(x, y, 0);
        pps.Add(Instantiate(prefabPP, pos, Quaternion.identity, spawnPP));
        GastarMedidor(Medidores.Higiene, 8);
        sujo = pps.Count > 0;
    }

    public void RemovePP(Transform pp)
    {
        pps.Remove(pp);
        Destroy(pp.gameObject);
        sujo = pps.Count > 0;
    }

    public void SetLocal(Locais local, bool first = false)
    {

        if (lastLocal != null && (lastLocal.Local == Locais.LadoFora.ToString() || local == Locais.LadoFora))
        {
            SetLocalOut(local);
            return;
        }

        if (lastLocal != null)
            lastLocal.HoraSaida = DateTime.Now.ToLongString();

        string localAnterior = ((Locais)current_local).ToString();
        current_local = (int)local;

        buttonRight.interactable = !(current_local == (int)Locais.TOTAL - 2);
        buttonLeft.interactable = !(current_local == 0);

        mainTween.DOAnchorPosX(-(current_local * mainTween.rect.width), timeToChange);

        LogLocais LogLocais = new LogLocais(
            first ? ((Locais)current_local).ToString() : ((Locais)current_local).ToString(),
            first ? "Inicio do jogo" : localAnterior,
            first ? "Local inicial do jogo" : "Mudança de tela por meio do menu de locais",
            System.DateTime.Now,
            System.DateTime.Now            
         );

        lastLocal = LogLocais;

        //SaveGameController.Instance.AddComportamento(ComportamentosType.troca_tela, LogLocais, inGame);
    }

    public void SetLocalOut(Locais local)
    {
        if (lastLocal != null)
            lastLocal.HoraSaida = DateTime.Now.ToLongString();

        FadeInOut.instance.FadeIn(0.5f, 0, () =>
        {
            if (local == Locais.LadoFora)
                ChangeGoOut(true);
            else if (inOut == true)
                ChangeGoOut(false);


            string localAnterior = ((Locais)current_local).ToString();
            current_local = (int)local;

            buttonRight.interactable = !(current_local == (int)Locais.TOTAL - 2);
            buttonLeft.interactable = !(current_local == 0);

            mainTween.DOAnchorPosX(-(current_local * mainTween.rect.width), timeToChange);

            LogLocais LogLocais = new LogLocais(
            ((Locais)current_local).ToString(),
            localAnterior,
            "Mudança de tela por meio do menu de locais",
            System.DateTime.Now,
            System.DateTime.Now
            );

            lastLocal = LogLocais;

            SaveGameController.Instance.AddComportamento(ComportamentosType.troca_tela, LogLocais, inGame);

            FadeInOut.instance.FadeOut(0.3f, 0.5f);
        });
    }

    private void ChangeGoOut(bool inOut)
    {
        this.inOut = inOut;

        buttonLeft.gameObject.SetActive(!inOut);
        buttonRight.gameObject.SetActive(!inOut);
    }

    public void GastarMedidor(Medidores estado, float quantidade)
    {
        switch (estado)
        {
            case Medidores.Saude:
                saude -= (saude - quantidade >= 0) ? quantidade : saude;
                break;
            case Medidores.Energia:
                energia -= (energia - quantidade >= 0) ? quantidade : energia;
                break;
            case Medidores.Alimentacao:
                alimentacao -= (alimentacao - quantidade >= 0) ? quantidade : alimentacao;
                break;
            case Medidores.Higiene:
                higiene -= (higiene - quantidade >= 0) ? quantidade : higiene;
                break;
            case Medidores.Diversao:
                diversao -= (diversao - quantidade >= 0) ? quantidade : diversao;
                break;
            default:
                Debug.LogError("Medidor não encontrado!!!");
                break;
        }
        SetBarras();
    }

    public void AdicionarMedidor(Medidores estado, float quantidade)
    {
        switch (estado)
        {
            case Medidores.Saude:
                saude += (saude + quantidade <= 100) ? quantidade : (100 - saude);
                break;
            case Medidores.Energia:
                energia += (energia + quantidade <= 100) ? quantidade : (100 - energia);
                break;
            case Medidores.Alimentacao:
                alimentacao += (alimentacao + quantidade <= 100) ? quantidade : (100 - alimentacao);
                break;
            case Medidores.Higiene:
                if (sujo)
                {
                    higiene += (higiene + quantidade <= 80) ? quantidade : (80 - higiene);
                }
                else
                {
                    higiene += (higiene + quantidade <= 100) ? quantidade : (100 - higiene);
                }

                break;
            case Medidores.Diversao:
                diversao += (diversao + quantidade <= 100) ? quantidade : (100 - diversao);
                break;
            default:
                Debug.LogError("Medidor não encontrado!!!");
                break;
        }
        SetBarras();
    }

    public bool CanBuy(int quantidade)
    {
        return quantidade <= coins;
    }

    public bool GastarCoins(int quantidade)
    {
        if (quantidade > coins)
            return false;

        coins -= quantidade;
        textCoins.text = coins.ToString();
        SaveGameController.Instance.dataGame.dataGame.coins = coins;
        SaveGameController.Instance.SaveGame();
        return true;
    }

    public void AdicionarCoins(int quantidade)
    {
        coins += quantidade;
        textCoins.text = coins.ToString();
        SaveGameController.Instance.dataGame.dataGame.coins = coins;
        SaveGameController.Instance.SaveGame();
    }

    /// <summary>
    /// Adionar food comprado
    /// </summary>
    public void AddFood(Comida food, string tipo)
    {
        DataGame _game = SaveGameController.Instance.dataGame.dataGame;

        int i = GetFoodItem(food);


        if (i >= 0)
        {
            _game.foods[i].Quantidade++;
        }
        else
        {

            FoodItem _food = new FoodItem();
            _food.Comida = food.name;
            _food.Quantidade = 1;
            _food.Tipo = tipo;

            _game.foods.Add(_food);
        }

        SaveGameController.Instance.SaveGame();
    }

    public List<FreezerItens> GetFoods()
    {

        List<FreezerItens> freeze_itens = new List<FreezerItens>();
        List<FoodItem> _foods = SaveGameController.Instance.dataGame.dataGame.foods;

        foreach (FoodItem f in _foods)
        {
            TipoComida tipo = null;
            foreach (TipoComida t in tiposComida)
            {
                if (t.name == f.Tipo)
                {
                    tipo = t;
                    break;
                }
            }
            if (tipo == null)
                break;

            foreach (Comida c in tipo.comidas)
            {
                if (c.name == f.Comida)
                {
                    FreezerItens fi = new FreezerItens();
                    fi.comida = c;
                    fi.quantidade = f.Quantidade;
                    freeze_itens.Add(fi);
                    break;
                }
            }
        }


        return freeze_itens;
    }

    public List<MedicamentoObject> GetMedicamentos()
    {
        return SaveGameController.Instance.dataGame.medicamentos.medicamentos;
    }

    /// <summary>
    /// Use food
    /// </summary>
    public void UseFood(Comida food)
    {
        DataGame _game = SaveGameController.Instance.dataGame.dataGame;
        int i = GetFoodItem(food);
        if (i >= 0)
        {
            _game.foods[i].Quantidade--;
            if (_game.foods[i].Quantidade <= 0)
                _game.foods.RemoveAt(i);
        }
        else
            return;

        SaveGameController.Instance.SaveGame();
    }

    private int GetFoodItem(Comida food)
    {
        int index = 0;
        bool exits = false;

        foreach (FoodItem f in SaveGameController.Instance.dataGame.dataGame.foods)
        {
            if (f.Comida == food.name)
            {
                exits = true;
                break;
            }
            index++;
        }
        return exits ? index : -1;
    }

    public void CallAction(ActionGame action, float quantidade)
    {
        switch (action)
        {
            case ActionGame.EscovarDente:
                if (higiene < 80)
                {
                    float qnt = higiene + quantidade > 80 ? 80 - higiene : quantidade;
                    AdicionarMedidor(Medidores.Higiene, qnt);
                }
                break;
            case ActionGame.LavarMao:
                efeitosPia.SetActive(true);
                this.ActionAfter(2, () =>
                {
                    efeitosPia.SetActive(false);
                    if (higiene < 80)
                    {
                        float qnt = higiene + quantidade > 80 ? 80 - higiene : quantidade;
                        AdicionarMedidor(Medidores.Higiene, qnt);
                    }
                });
                break;
        }
    }

    public void OnClickChange(int i)
    {
        if (lastLocal != null && (lastLocal.Local == Locais.LadoFora.ToString() || (Locais)i == Locais.LadoFora))
        {
            SetLocalOut((Locais)i);
            return;
        }

        if (lastLocal != null)
            lastLocal.HoraSaida = DateTime.Now.ToLongString();

        string localAnterior = ((Locais)current_local).ToString();
        if (i > 0)
        {
            if (current_local + 1 < (int)Locais.TOTAL - 1)
                current_local++;
        }
        else
        {
            if (current_local - 1 >= 0)
                current_local--;
        }

        buttonRight.interactable = !(current_local == (int)Locais.TOTAL - 2);
        buttonLeft.interactable = !(current_local == 0);

        mainTween.DOAnchorPosX(-(current_local * mainTween.rect.width), timeToChange);

        LogLocais LogLocais = new LogLocais(
            ((Locais)current_local).ToString(),
            localAnterior,
            "Mudança de tela por meio dos botões",
            System.DateTime.Now,
            System.DateTime.Now
        );
        lastLocal = LogLocais;

        SaveGameController.Instance.AddComportamento(ComportamentosType.troca_tela, LogLocais);
    }

    public void OnClickPia()
    {
        LogAcao LogAcao = new LogAcao(
            "Lavar as maos",
            "Pia banheiro",
            System.DateTime.Now,
            Medidores.Higiene
        );

        SaveGameController.Instance.AddComportamento(ComportamentosType.acao, LogAcao);

        CallAction(ActionGame.LavarMao, 20);
    }

    public void OnClickSleeping()
    {
        dormindo = !dormindo;
        if (dormindo)
            Player.instance.p_animator.SetTrigger("goSleep");
        else
            Player.instance.p_animator.SetTrigger("wakeUp");

        LogAcao LogAcao = new LogAcao(
            dormindo ? "Dormir" : "Acordar",
            "",
            System.DateTime.Now,
            Medidores.Energia
        );

        SaveGameController.Instance.AddComportamento(ComportamentosType.acao, LogAcao);
    }

    public void OnClickMenuRooms()
    {
        PrefabsController.instance.InstancePrefab(PrefabsController.instance.popupRooms);
    }

    public void OnClickFridge()
    {
        if (!freezerOpen)
            PrefabsController.instance.InstancePrefab(PrefabsController.instance.popupFreezer);

        freezerOpen = !freezerOpen;
        imageFreezer.sprite = freezerOpen ? spriteFreezerOpen : spriteFreezerClose;
    }

    public void OnClickMedicamentos()
    {
        PrefabsController.instance.InstancePrefab(PrefabsController.instance.popupMedicamentos);
    }

    public void OnClickGoIn()
    {
        SetLocalOut(Locais.Sala);
    }

    public void OnClickGoOut()
    {
        SetLocalOut(Locais.LadoFora);
    }

    public void OnClickGames()
    {
        ChangeScene("MiniGameFallFood");
    }

    private void ChangeScene(string sceneName)
    {
        FadeInOut.instance.FadeIn(1, 1,()=> {
            SceneManager.LoadScene(sceneName);
        });
    }

    public void LastLocalUpdate()
    {
        if (lastLocal != null)
            lastLocal.HoraSaida = DateTime.Now.ToLongString();
    }
}
