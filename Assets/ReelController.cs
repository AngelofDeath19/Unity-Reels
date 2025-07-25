using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ReelController : MonoBehaviour
{
    
    [Header("Tuning Parameters")]
    public float spinSpeed = 8000f;
    public float loopHeight;  
    public float stoppingSpeed = 10f;
    
    [Header("Setup")]
    public List<RectTransform> symbols;
    
    // Internal state
    public bool isSpinning { get; set; } = false;
    public string finalSymbolName { get; private set; }

    private Coroutine stoppingCoroutine;
    private RectTransform rectTransform;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpinning) return;
        
        //Move up Y, symbols going down
        rectTransform.anchoredPosition += new Vector2(0, spinSpeed * Time.deltaTime);
        
        //Check and reset pos for illusion of infinite spin
        if (rectTransform.anchoredPosition.y > loopHeight)
        { 
            rectTransform.anchoredPosition -= new Vector2(0, loopHeight);
        }
    }

    public void StartStopping()
    {
        if (stoppingCoroutine != null) return;
        stoppingCoroutine = StartCoroutine(StopReelCoroutine());
    }

    private IEnumerator StopReelCoroutine()
    {
        isSpinning = false;

        int randomIndex = Random.Range(0, symbols.Count);
        RectTransform targetSymbol = symbols[randomIndex];
        finalSymbolName = targetSymbol.gameObject.name;
        
        float finalYPosition = -targetSymbol.anchoredPosition.y - (rectTransform.rect.height / 2f);
        Vector2 targetPosition = new Vector2(rectTransform.anchoredPosition.x, finalYPosition);

        while (Mathf.Abs(rectTransform.anchoredPosition.y - finalYPosition) > 1f)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(
                rectTransform.anchoredPosition,
                targetPosition,
                stoppingSpeed * Time.deltaTime
                );
            yield return null;
        }
        
        rectTransform.anchoredPosition = targetPosition;
        stoppingCoroutine = null;
    }
}
