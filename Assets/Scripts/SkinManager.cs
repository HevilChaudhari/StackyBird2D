using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinManager : MonoBehaviour
{

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private SpriteRenderer spriteRenderer;

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    [SerializeField] private GameObject LockImg;
    [SerializeField] private TextMeshProUGUI totalCoinText;
    [SerializeField] private Button buyBtn;
    [SerializeField] private SO_Color SO_colors;
    [SerializeField]private Color[] availableColors = new Color[]
    {
        Color.yellow,
        Color.green,
        Color.blue,
        new Color(1f, 0.5f, 0f),
        Color.red,
    };
    private int currentIndex;
    private int totalCoins;
    public ColorShop[] color;

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += ChangePlayerSkin;
        GameManager.OnGameStateChanged += ShopOpned;
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void ShopOpned(GameManager.GameState state)
    {
       if(state == GameManager.GameState.shopopned)
        {
            currentIndex = PlayerPrefs.GetInt("currentColorIndex",0);
            spriteRenderer.color = availableColors[currentIndex];
            UpdateUI();
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void ChangePlayerSkin(GameManager.GameState state)
    {
        if(state == GameManager.GameState.start)
        {
            ApplyColor();
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= ChangePlayerSkin;
        GameManager.OnGameStateChanged -= ShopOpned;
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {

        totalCoins = PlayerPrefs.GetInt("totalCoins", 0);
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentIndex = PlayerPrefs.GetInt("currentColorIndex", 0);
        foreach(ColorShop cs in color)
        {
            if(cs.price == 0)
            {
                cs.isUnloacked = true;
            }
            else
            {
                cs.isUnloacked = PlayerPrefs.GetInt(cs.colorName, 0) == 0 ? false : true;
            }
        }
        UpdateUI();
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------
    // Start is called before the first frame update

    void Start()
    {   
        ApplyColor();
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    public void NextColor()
    {
        spriteRenderer.color = availableColors[currentIndex];
        int nextIndex = (currentIndex + 1) % availableColors.Length;

        currentIndex = nextIndex;
            
        spriteRenderer.color = availableColors[currentIndex];

        if (color[currentIndex].isUnloacked)
        {
            PlayerPrefs.SetInt("currentColorIndex",currentIndex);
        }


        UpdateUI();
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    public void PreviousColor()
    {
        spriteRenderer.color = availableColors[currentIndex];
        int previousIndex = (currentIndex - 1 + availableColors.Length) % availableColors.Length;
        currentIndex = previousIndex;
        spriteRenderer.color = availableColors[currentIndex];

        if (color[currentIndex].isUnloacked)
        {
            PlayerPrefs.SetInt("currentColorIndex",currentIndex);
        }

        UpdateUI();
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void ApplyColor()
    {
        if (color[currentIndex].isUnloacked)
        {

            spriteRenderer.color = availableColors[currentIndex];
            PlayerPrefs.SetInt("currentColorIndex", currentIndex);

        }
        else
        {
            int lastUnlockedColor = PlayerPrefs.GetInt("currentColorIndex", 0);
            spriteRenderer.color = SO_colors.ApplyColor[lastUnlockedColor];
            currentIndex = lastUnlockedColor;
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    private void UpdateUI()
    {
        LockImg.gameObject.SetActive(!color[currentIndex].isUnloacked);
        if(color[currentIndex].isUnloacked)
        {
            buyBtn.gameObject.SetActive(false);
        }
        else
        {
            buyBtn.GetComponentInChildren<TextMeshProUGUI>().text = color[currentIndex].price.ToString() + "$";

            if(totalCoins < color[currentIndex].price)
            {
                buyBtn.gameObject.SetActive(true);
                buyBtn.interactable = false;
            }
            else
            {
                buyBtn.gameObject.SetActive(true);
                buyBtn.interactable = true;
            }
        }
        totalCoinText.text = totalCoins.ToString();
    }

    //------------------------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------

    public void UnlockColor()
    {
        int coins = totalCoins;
        int colorPrice = color[currentIndex].price;

        totalCoins = coins - colorPrice;
        PlayerPrefs.SetInt("totalCoins", totalCoins);
        PlayerPrefs.SetInt(color[currentIndex].colorName, 1);
        color[currentIndex].isUnloacked = true;
        ApplyColor();
        UpdateUI();
        AudioManager.Instance.playSound("BoughtSkin");
    }
}
