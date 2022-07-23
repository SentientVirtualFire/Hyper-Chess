using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TurnManager turnManager;
    public TextMeshProUGUI turn;
    public TextMeshProUGUI check;
    public RectTransform promotions;
    public RectTransform play;
    public RectTransform info;
    public bool isPlaying = false;

    bool prevTurn = true;
    private void Awake()
    {
        Time.timeScale = 0;
    }
    void Start()
    {

    }

    void Update()
    {
        if (turnManager.turnWhite != prevTurn)
        {
            if (turnManager.turnWhite)
            {
                turn.text = "TURN: WHITE";
            }
            else
            {
                turn.text = "TURN: BLACK";
            }
            prevTurn = turnManager.turnWhite;
        }
        if (turnManager.isCheck)
        {
            check.enabled = true;
            check.gameObject.transform.parent.gameObject.SetActive(true);
            check.text = $"IN CHECK: {turnManager.checkedTeam}";
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
        play.gameObject.SetActive(false);
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
}