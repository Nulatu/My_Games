using System;
using System.Collections.Generic;
using ExitGames.Client.Photon.Chat;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChatGui : MonoBehaviour, IChatClientListener
{
    public string ChatAppId;                    // set in inspector. Your Chat AppId (don't mix it with Realtime/Turnbased Apps).
    public string UserName { get; set; }
    public string selectedChannelName="General";     // mainly used for GUI/input
 
    public ChatClient chatClient;

    public Text input_text;
    public InputField inp;
    public Text output_text;
    public bool odno_nagatie;//чтобы после нажатия интера не вызывалось несколько раз send();

    public void inic_chat()
    {
        DontDestroyOnLoad(gameObject);
        Application.runInBackground = true; // this must run in background or it will drop connection if not focussed.
        UserName = PhotonNetwork.playerName;
        if (string.IsNullOrEmpty(this.UserName))
        {
            this.UserName = "user" + Environment.TickCount%99; //made-up username
        }
        chatClient = new ChatClient(this);
        chatClient.Connect(ChatAppId, "1.0", UserName, null);
    }
    public IEnumerator Zadergka()
    {
        yield return new WaitForSeconds(0.5f);
        odno_nagatie = false;
    }
    public void Zadergka_()
    {
        StartCoroutine(Zadergka());
    }
    /// <summary>To avoid that the Editor becomes unresponsive, disconnect all Photon connections in OnApplicationQuit.</summary>
    public void OnApplicationQuit()
    {
        if (this.chatClient != null)
        {
            this.chatClient.Disconnect();
        }
    }

    public void Update()
    {
        if (this.chatClient != null)
        {
            this.chatClient.Service();  // make sure to call this regularly! it limits effort internally, so calling often is ok!
        }

        if (Input.GetKey(KeyCode.Return)) // интер
        {
            if (odno_nagatie == false)
            {
                GuiSendsMsg();
                odno_nagatie = true;
            }
        }        
    }
    public void GuiSendsMsg()
    {
        if (string.IsNullOrEmpty(input_text.text))
        {
            //если отправляем пустое сообщение(не отправлять его)
            return;
        }
        else
        {
            chatClient.PublishMessage(selectedChannelName, input_text.text);
        }

        inp.text = "";
    }
    public void OnConnected()
    {
        chatClient.Subscribe(new[] { selectedChannelName },-1);
    }
    
    public void OnDisconnected()
    {
    }
    public void OnChatStateChange(ChatState state)
    {
    }   
    public void OnSubscribed(string[] channels, bool[] results)
    {
    }    
    public void OnUnsubscribed(string[] channels)
    {
    }
    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        Zadergka_();
        for (int i = 0; i < senders.Length; i++)
        {
            output_text.text = output_text.text + senders[i] + ":" + messages[i] + "\n"; // отрисовка сообщений
        }
    }
    public void OnPrivateMessage(string sender, object message, string channelName)
    {
    }   
    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
    }
}
