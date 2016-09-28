using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class chat_ui : Photon.MonoBehaviour  
{
    public List<string> messages = new List<string>();
    public  int i = 0;
    public Text input_text;
    public Text output_text;
    public string bufer;
    public bool odno_nagatie;//чтобы после нажатия интера не вызывалось несколько раз send();
    public InputField inp;

    public IEnumerator Zadergka()
    {
        yield return new WaitForSeconds(0.5f);
        odno_nagatie = false;
    }
    public void Zadergka_()
    {
        StartCoroutine(Zadergka());
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Return)) // интер
        {
            if (odno_nagatie == false)
            {
                send();
                odno_nagatie = true;
            }
        }
    }
	public void Messeg ()
    {
        Zadergka_();

        for (int i = 0; i < messages.Count; i++)
        {
            bufer = bufer + messages[i];
        }
        output_text.text=bufer;
        bufer = "";
	}
    public void send()
    {
        photonView.RPC("Chat", PhotonTargets.All, input_text.text);
    }
    [PunRPC]
    public void Chat(string newLine, PhotonMessageInfo mi)
    {
        if (string.IsNullOrEmpty(newLine))
        {
            //если отправляем пустое сообщение(не отправлять его)
            return;
        }
        output_text.text = mi.sender.name + ": " + newLine + "\n";
        messages.Add(output_text.text);
        Messeg();
        inp.text = "";
    }
    public void AddLine(string newLine)
    {
        this.messages.Add(newLine);
    }
}
