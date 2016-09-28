using System;
using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkApi : MonoBehaviour
{
    private static NetworkApi _instance;
    public static NetworkApi Instance { get { return _instance; } }

    public bool IsLoggedIn { get; private set; }

    public Text Kol_win_bd;
    public Text Kol_game_bd;

    void Start()
    {
        if (Application.loadedLevelName == "Igrovoe_pole")
        {
            Storage.Kol_game += 1;
            print(Storage.Kol_win);
            StartCoroutine(NetworkApi.Instance.UpdateInfo(Storage.Login, Storage.PlayerName, Storage.Kol_win, Storage.Kol_game,Storage.ban,Storage.ban_chat, Storage.kredits));
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator Login(string login)
    {
        //print(Storage.ServerUri);
        var www = new WWW(string.Format("{0}?method=login&login={1}", Storage.ServerUri, login));
        //print(www.text);
        if (!www.isDone)
            yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.LogException(new Exception("NetworkApi.Login error:" + www.error));
            IsLoggedIn = true;
            yield break;
        }

        //Debug.Log("result:" + www.text);
        
        var pars = www.text.Split(new[] {'&'}, StringSplitOptions.RemoveEmptyEntries);
        var dic = pars.Select(n => n.Split(new[] {'='}, StringSplitOptions.RemoveEmptyEntries))
            .ToDictionary(k => k[0], v => v[1]);
        Storage.PlayerName = dic["name"];
        Storage.Kol_win = int.Parse(dic["kol_win"]);
        Storage.Kol_game = int.Parse(dic["kol_game"]);
        if (dic["ban"][0] != '0') 
            Storage.ban = DateTime.Parse(dic["ban"], System.Globalization.CultureInfo.InvariantCulture);
        if (dic["ban_chat"][0] != '0')
            Storage.ban_chat = DateTime.Parse(dic["ban_chat"], System.Globalization.CultureInfo.InvariantCulture);
        Storage.kredits = int.Parse(dic["kredits"]);
        //print(Storage.ban.ToString("HH:mm:ss")); // "HH" 24- часовой формат времени
        // иф нужен из-за нулевой инициализации даты в sql тип си шарпа DateTime не понимает эти нули и не поймет DateTime.Parse
        Kol_win_bd.text = Storage.Kol_win.ToString();
        Kol_game_bd.text = Storage.Kol_game.ToString();

        IsLoggedIn = true;
    }

    public IEnumerator UpdateInfo(string login,string playerName, int kol_win, int kol_game, DateTime ban,DateTime ban_chat,int kredits)
    {
        var www = new WWW(string.Format("{0}?method=update&login={1}&name={2}&kol_win={3}&kol_game={4}&ban={5}&ban_chat={6}&kredits={7}",
            Storage.ServerUri,
            login,
            playerName,
            kol_win,
            kol_game,
            ban,
            ban_chat,
            kredits));

        if (!www.isDone)
            yield return www;
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.LogException(new Exception("NetworkApi.Login error:" + www.error));
            yield break;
        }

        //Debug.Log("NetworkApi.Update result: " + www.text);
        //print(www.text);
    }
}
