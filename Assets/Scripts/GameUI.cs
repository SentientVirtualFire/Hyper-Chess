using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TurnManager tm;
    public TextMeshProUGUI turn;
    public TextMeshProUGUI check;
    public RectTransform promotions;
    public RectTransform play;
    public RectTransform game;
    public RectTransform info;
    public RectTransform finish;
    public GameObject bg;
    public bool isPlaying = false;

    bool prevTurn = true;
    private void Awake()
    {
        Time.timeScale = 0;
    }

    void Update()
    {
        if (tm.turnWhite != prevTurn)
        {
            if (tm.turnWhite)
            {
                turn.text = "TURN: WHITE";
            }
            else
            {
                turn.text = "TURN: BLACK";
            }
            prevTurn = tm.turnWhite;
        }
        if (tm.isCheck)
        {
            check.enabled = true;
            check.gameObject.transform.parent.gameObject.SetActive(true);
            check.text = $"IN CHECK: {tm.checkedTeam}";
        }
        else
        {
            check.enabled = false;
            check.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
    public void PlayGame()
    {
        isPlaying = true;
        Time.timeScale = 1;
        bg.SetActive(false);
        play.gameObject.SetActive(false);
        game.gameObject.SetActive(true);
    }
    public void OpenInfo()
    {
        play.gameObject.SetActive(false);
        info.gameObject.SetActive(true);
    }
    public void CloseInfo()
    {
        info.gameObject.SetActive(false);
        play.gameObject.SetActive(true);
    }
    public void ResetScene()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void SetUpPromotion(GameObject promotee)
    {
        promotions.gameObject.SetActive(true);
        Button queen = promotions.GetChild(1).GetComponent<Button>();
        Button rook = promotions.GetChild(2).GetComponent<Button>();
        Button bishop = promotions.GetChild(3).GetComponent<Button>();
        Button knight = promotions.GetChild(4).GetComponent<Button>();
        queen.onClick.AddListener(delegate { promotee.GetComponent<Pawn>().PromoteSelf("Queen",queen); });
        rook.onClick.AddListener(delegate { promotee.GetComponent<Pawn>().PromoteSelf("Rook",rook); });
        bishop.onClick.AddListener(delegate { promotee.GetComponent<Pawn>().PromoteSelf("Bishop",bishop); });
        knight.onClick.AddListener(delegate { promotee.GetComponent<Pawn>().PromoteSelf("Knight",rook); });
    }
    public void CheckMate()
    {
        isPlaying = false;
        Time.timeScale = 0;
        if (tm.checkedIsWhite.Value)
        {
            finish.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "BLACK WINS";
        }
        else
        {
            finish.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "WHITE WINS";
        }
        finish.GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = $"moves: {tm.turnNum}";
        game.gameObject.SetActive(false);
        bg.SetActive(true);
        finish.gameObject.SetActive(true);
    }
}