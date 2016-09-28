using UnityEngine;
//using System.Collections;
using System;

public class Storage
{
    public const string ServerUri = "http://monopoly-vk.ru/localhost/test/index.php";
    public static string Login = "";

    public static string PlayerName = "Player" + UnityEngine.Random.Range(0, 1000);
    public static int Kol_win = 0;
    public static int Kol_game = 0;
    public static DateTime ban;
    public static DateTime ban_chat;
    public static int kredits;

}
