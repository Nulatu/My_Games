using UnityEngine;
[RequireComponent(typeof(ChatGui))]
public class NamePickGui : MonoBehaviour
{
    private ChatGui chatComponent;
    public void Awake()
    {
        this.chatComponent = this.GetComponent<ChatGui>();
        
        if (this.chatComponent != null && chatComponent.enabled)
        {
            this.chatComponent.enabled = false;
        }
    } 
    
    public void OnGUI()
    {
        this.StartChat();
    }
    
    private void StartChat()
    {
        this.chatComponent.enabled = true;
        this.enabled = false;
    }
}
