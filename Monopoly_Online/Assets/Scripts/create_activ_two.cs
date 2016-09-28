using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class create_activ_two : MonoBehaviour 
{
    int i1 = 0;
    int i2 = 0;
    public Button create;
    public void Activ_one() 
    {
        i1++;
        Active();
	}
    public void Activ_two() 
    {
        i2++;
        Active();
	}
    void Active()
    {
        if( i1>=1 && i2>=1 )
            create.interactable = true;
    }
}
