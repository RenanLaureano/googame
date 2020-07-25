using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupGameOver : MonoBehaviour
{
    [Header("Popup")]
    [SerializeField]
    private Image m_Background;
    [SerializeField]
    private RectTransform m_Popup;

    [Header("Texts")]
    [SerializeField]
    private Text m_TextPoints;
    [SerializeField]
    private Text m_TextRecorde;
    [SerializeField]
    private Text m_TextCoins;

    private string m_CurrentScene;
    public void Config(int points, int recorde, int coins, string scene)
    {
        AnimDefault.AnimIn(m_Background,m_Popup);

        m_TextPoints.text = "Pontos: " + points.ToString();
        m_TextRecorde.text = "Recorde: " + recorde.ToString();
        m_TextCoins.text = coins.ToString();

        m_CurrentScene = scene;

        SaveGameController.Instance.dataGame.dataGame.coins += coins;
        SaveGameController.Instance.SaveGame(false);
    }

    public void OnClickReset()
    {
        SceneManager.LoadScene(m_CurrentScene);
    }

    public void OnClickHome()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
