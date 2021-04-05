using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreensManager : MonoBehaviour {

    public static ScreensManager instance;

    private GameObject currentScreen;

    public GameObject endScreen;
    public GameObject gameScreen;
    public GameObject mainScreen;
    public GameObject returnScreen;

    public Button lengthButton;
    public Button strengthButton;
    public Button offlineButton;

    public TextMeshProUGUI gameScreenMoney;
    public TextMeshProUGUI lengthCostText;
    public TextMeshProUGUI lengthValueText;
    public TextMeshProUGUI strengthCostText;
    public TextMeshProUGUI strengthValueText;
    public TextMeshProUGUI offlineCostText;
    public TextMeshProUGUI offlineValueText;
    public TextMeshProUGUI endScreenMoney;
    public TextMeshProUGUI returnScreenMoney;

    private int gameCount;

	void Awake ()
    {
        if (ScreensManager.instance)
            Destroy(base.gameObject);
        else
            ScreensManager.instance = this;

        currentScreen = mainScreen;
	}
	
    void Start()
    {
        CheckIdles();
        UpdateTexts();
    }

    public void ChangeScreen(Screens screen)
    {
        currentScreen.SetActive(false);
        switch (screen)
        {
            case Screens.MAIN:
                currentScreen = mainScreen;
                UpdateTexts();
                CheckIdles();
                break;

            case Screens.GAME:
                currentScreen = gameScreen;
                gameCount++;
                break;

            case Screens.END:
                currentScreen = endScreen;
                SetEndScreenMoney();
                break;

            case Screens.RETURN:
                currentScreen = returnScreen;
                SetReturnScreenMoney();
                break;
        }
        currentScreen.SetActive(true);
    }

    public void SetEndScreenMoney()
    {
        endScreenMoney.text = "$" + IdleManager.instance.totalGain;
    }

    public void SetReturnScreenMoney()
    {
        returnScreenMoney.text = "$" + IdleManager.instance.totalGain + " gained while waiting!";
    }

    public void UpdateTexts()
    {
        gameScreenMoney.text = "$" + IdleManager.instance.wallet;
        lengthCostText.text = "$" + IdleManager.instance.lengthCost;
        lengthValueText.text = -IdleManager.instance.length + "m";
        strengthCostText.text = "$" + IdleManager.instance.strengthCost;
        strengthValueText.text = IdleManager.instance.strength + " fishes.";
        offlineCostText.text = "$" + IdleManager.instance.offlineEarningsCost;
        offlineValueText.text = "$" + IdleManager.instance.offlineEarnings + "/min";
    }

    public void CheckIdles()
    {
        int lengthCost = IdleManager.instance.lengthCost;
        int strengthCost = IdleManager.instance.strengthCost;
        int offlineEarningsCost = IdleManager.instance.offlineEarningsCost;
        int wallet = IdleManager.instance.wallet;

        if (wallet < lengthCost)
            lengthButton.interactable = false;
        else
            lengthButton.interactable = true;

        if (wallet < strengthCost)
            strengthButton.interactable = false;
        else
            strengthButton.interactable = true;

        if (wallet < offlineEarningsCost)
            offlineButton.interactable = false;
        else
            offlineButton.interactable = true;
    }
}
