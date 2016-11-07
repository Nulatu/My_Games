using UnityEngine;

public class Match_Behavior : MonoBehaviour
{
    action_match match_behavior = action_match.not_activ;
    enum action_match { activ,not_activ,close_activ }

    public void OnMouseDown()
    {
        if (GetComponent<MeshRenderer>() == null) return; // когда игрок кликает во время хода компа
        Material match_col = GetComponent<MeshRenderer>().materials[0];

        switch (match_behavior)
        {
            case action_match.not_activ: // если жмем на неактивную спичку
                if (Game_basis.Pravila.matches_activ.Count == Game_basis.Pravila.n + 1)
                {
                    match_behavior = action_match.close_activ;
                    match_col.color = Color.red;
                    Invoke("OnMouseDown", 1); // переходим в неактивное состояние.
                }
                else
                {
                    match_behavior = action_match.activ;
                    Game_basis.Pravila.matches_activ.Add(gameObject);
                    match_col.color = Color.white;
                }
                break;
            case action_match.activ:
                match_behavior = action_match.not_activ;
                Game_basis.Pravila.matches_activ.Remove(gameObject);
                match_col.color = Color.grey;
                break;
            case action_match.close_activ:
                match_behavior = action_match.not_activ;
                match_col.color = Color.grey;
                break;
        }
    }
}
