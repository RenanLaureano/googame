using System;
using System.Collections.Generic;

[Serializable]
public class DefaultPost
{
    public string type;
}

[Serializable]
public class DefaultResponse
{
    public int code;
    public string data;
}


[Serializable]
public class PostLogin : DefaultPost
{
    public Data data;

    public PostLogin(string login, string password)
    {
        data = new Data();
        data.login = login;
        data.password = password;
        type = "login-crianca";
    }

    [Serializable]
    public class Data
    {
        public string login;
        public string password;
    }
}

[Serializable]
public class PostDataGame : DefaultPost
{
    public int id;
    public DataGame data;

    public PostDataGame(int id, DataGame data)
    {
        type = "save-data";
        this.id = id;
        this.data = data;
    }
}

[Serializable]
public class PostLogInteracao : DefaultPost
{
    public int id;
    public List<LogInteracao> data;

    public PostLogInteracao(int id, List<LogInteracao> data)
    {
        type = "set-log-interacao";
        this.id = id;
        this.data = data;
    }
}

[Serializable]
public class PostLogLocais : DefaultPost
{
    public int id;
    public List<LogLocais> data;

    public PostLogLocais(int id, List<LogLocais> data)
    {
        type = "set-log-locais";
        this.id = id;
        this.data = data;
    }
}

[Serializable]
public class DataUser
{
    public int id;
    public string name;
    public string email;
}