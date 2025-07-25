using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class WinningCombination
{
    public string combinationName;
    public List<string> symbolNames;
    public int payoutAmount;
}

public class GameManager : MonoBehaviour
{
    [Header("Game Configuration")]
    [SerializeField] private int playerCurrency = 1000;
    [SerializeField] private int currentBet = 10;
    [SerializeField] private List<WinningCombination> combinations;
    
    [Header("UI Elements")]
    [SerializeField] private Button spinButton;

    [SerializeField] private List<ReelController> reels;
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private TextMeshProUGUI winText;
    
    [Header("Timings")]
    [SerializeField] private float preSpinDelay = 2.0f;

    [SerializeField] private float delayBetweenReelStops = 1.0f;
    [SerializeField] private float postSpinDelay = 2.0f;
    [SerializeField] private float winTextDisplayTime = 2.0f;
    

    void Start()
    {
        spinButton.onClick.AddListener(OnSpinButtonPressed);
        UpdateCurrencyDisplay();
        winText.text = "";
    }

    private void OnSpinButtonPressed()
    {
        if (playerCurrency >= currentBet)
        {
            StartCoroutine(MainGameSequence());
        }
    }

    private IEnumerator MainGameSequence()
    {
        PrepareToSpin();
        StartAllReelsSpinning();
        
        yield return StopReelsSequentially();

        ProcessSpinResults();
    }

    private void PrepareToSpin()
    {
        spinButton.interactable = false;
        winText.text = "";
        playerCurrency -= currentBet;
        UpdateCurrencyDisplay();
    }

    private void StartAllReelsSpinning()
    {
        foreach (var reel in reels)
        {
            reel.isSpinning = true;
        }
    }

    private IEnumerator StopReelsSequentially()
    {
        yield return new WaitForSeconds(preSpinDelay);

        foreach (var reel in reels)
        {
            reel.StartStopping();
            yield return new WaitForSeconds(delayBetweenReelStops);
        }
    }

    private void ProcessSpinResults()
    {
        List<string> finalResult = reels.Select(reel => reel.finalSymbolName).ToList();
        
        WinningCombination winningCombo = combinations.FirstOrDefault(combo => finalResult.SequenceEqual(combo.symbolNames));

        if (winningCombo != null)
        {
            HandleWin(winningCombo);
        }
        
        spinButton.interactable = true;
    }

    private void HandleWin(WinningCombination winningCombo)
    {
        playerCurrency += winningCombo.payoutAmount;
        UpdateCurrencyDisplay();
        StartCoroutine(ShowWinTextCoroutine(winningCombo));
    }

    private IEnumerator ShowWinTextCoroutine(WinningCombination combo)
    {
        winText.text = $"WIN! +{combo.payoutAmount}";
        yield return new WaitForSeconds(winTextDisplayTime);
        winText.text = "";
    }
    
    void UpdateCurrencyDisplay()
    {
        currencyText.text = "Currency: " + playerCurrency.ToString();
    }
}
