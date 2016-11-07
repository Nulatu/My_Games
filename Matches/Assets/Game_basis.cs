using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Game_basis : MonoBehaviour
{
    public static Game_basis Pravila;
    public GameObject match;
    public List<GameObject> box_matches;
    public List<GameObject> matches_activ;
    public int n =2;
    public int kol_respaun_matches;
    Hod hodit = Hod.player;
    enum Hod { player, comp }


    public GameObject clean;
    public Text player_activ_matches;
    public Text comp_activ_matches;
    public GameObject comp_turn;
    public GameObject final_panel;
    public GameObject win_panel;
    public GameObject gameover_panel;
    public GameObject block_onmousedown;
    void Start ()
    {
        kol_respaun_matches = Random.Range(20,46);
        Pravila = GetComponent<Game_basis>();
        for (int i = 0,s=1; i < kol_respaun_matches; i++,s++) // отсчет i с нуля обзательно! для грамотного выравнивания положения респауна спичек
        {
            if (((i % 2) == 0)) // обязательно сначало деление а потом умножение чтобы не было пустоты между спичками в центре
                box_matches.Add(Instantiate(match, new Vector3(-0.5f * (s / 2), 1, 2.1f-1.4f*(i/13)), Quaternion.identity) as GameObject);
            else
                box_matches.Add(Instantiate(match, new Vector3(0.5f * (s / 2), 1, 2.1f-1.4f*(i/13)), Quaternion.identity) as GameObject);
            if (s == 13) s = 0;
        }
    }
    public void Clean() // кнопка снять спички.
    {
        StartCoroutine(Clean_mathes());

        if (box_matches.Count != 0)
            StartCoroutine(Hod_Comp());
    }
    IEnumerator Clean_mathes()
    {
        n = matches_activ.Count;

        if (hodit == Hod.player)
            player_activ_matches.text = (n + int.Parse(player_activ_matches.text)).ToString();
        else
            comp_activ_matches.text = (n + int.Parse(comp_activ_matches.text)).ToString();

        for (int i = 0; i < matches_activ.Count; i++)
        {
            Destroy(matches_activ[i]);
            box_matches.Remove(matches_activ[i]);
        }
        matches_activ.RemoveAll(item => item);
        while(matches_activ.Count!=0)
            yield return new WaitForSeconds(0.01f);
    }
    IEnumerator Hod_Comp()
    {
        hodit = Hod.comp;

        block_onmousedown.SetActive(true);
        int kol_activ = Random.Range(n - 1, n+2);
        if (kol_activ == 0) kol_activ = 1; // когда мы убираем одну спичку то n-1 просит 0 убрать

        if (n + 1 >= box_matches.Count) kol_activ = box_matches.Count; // если оставшиеся спички находятся в диапозоне n-1 n+1 то мы выбираем нужное кол-во чтобы не промахнуться из-за рандом.ранг и не проиграть

        for (int i = 0, kol = 0; i < box_matches.Count && kol < kol_activ; i++)
        {
            box_matches[i].GetComponent<Match_Behavior>().OnMouseDown();
            ++kol;
            yield return new WaitForSeconds(0.7f);
        }
        StartCoroutine(Clean_mathes());
        block_onmousedown.SetActive(false);

        if (box_matches.Count != 0)
            hodit = Hod.player;
    }
	public void Update ()
    { // интерфейс UI ,кнопки ,панели победы, проигрыша
        if (box_matches.Count == 0)
        {
            final_panel.SetActive(true);
            if (hodit == Hod.player)
                win_panel.SetActive(true);
            else
                gameover_panel.SetActive(true);

            comp_turn.SetActive(false);
        }
        if (box_matches.Count != 0)
        {
            if (hodit == Hod.player) // когда ходит компьютер не надо никаких  показов кнопок интерфейса игрока
            {
                if (matches_activ.Count == 0)
                    clean.SetActive(false);
                else if (matches_activ.Count > n - 2 || matches_activ.Count == box_matches.Count) // второе условие для случаев(последний ход игрока может снять 4-6 спичек а осталось на столе меньше 4-х)
                    clean.SetActive(true);

                comp_turn.SetActive(false);
            }
            else
                comp_turn.SetActive(true);
        }
    }
}
