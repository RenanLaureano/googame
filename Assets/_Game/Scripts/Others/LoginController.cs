using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    // Start is called before the first frame update
    public Text textError;
    public InputField inputUsuario;
    public InputField inputSenha;

    private void Start()
    {
        inputUsuario.text = PlayerPrefs.GetString("Login", "");
        inputSenha.text = PlayerPrefs.GetString("Password", "");
    }

    public void OnClickLogin()
    {
        PostLogin postLogin = new PostLogin(inputUsuario.text, inputSenha.text);
        string json = JsonUtility.ToJson(postLogin);
        ServerConnection.Instance.Post(json, FinishRequest);
    }

    void FinishRequest(DefaultResponse response)
    {
        if(response == null)
        {
            textError.text = "Erro de conexão!";
        } else
        {
            if (response.code == 1)
            {
                PlayerPrefs.SetString("Login", inputUsuario.text);
                PlayerPrefs.SetString("Password", inputSenha.text);
                UserInstance.Instance.User = JsonUtility.FromJson<DataUser>(response.data);
                UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");
            }
            else
            {
                textError.text = response.data;
            }
        }
    }
}
