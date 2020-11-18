using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHandler : MonoBehaviour {
    [SerializeField] private ResourceProduction.Data startingStore;
    [SerializeField] private Resource.Resource resource;
    [SerializeField] private ulong resourcesRequired;
    [SerializeField] private Text upgradeText;
    [SerializeField] private Text playerLevelText;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    [SerializeField] private Button ascendButton;
    [SerializeField] private Castle.Castle castleReference;
    
    public static int PlayerLevel { get; private set; }
    private bool clickedYes, clickedNo, ascending;
    private static string PlayerLevelKey => "Player_level";

    public void Ascend()
    {
        //don't run again if we are already waiting for an answer
        if (ascending) return;
        
        //0. check if user has enuff resources to ascend
        //TODO change requirements to castles
        Debug.Log($"number castles owned{castleReference.NumberOwned}");
        if (!HasCastlesRequired())
        {
            return;
        }
        
        ascending = true;
        
        upgradeText.text = "ascending...";
        //1. ask user if they really want to zap all data and start over, to level up
        //1.5 implement yes/no logic
        //Wait for the button to be pressed
        StartCoroutine(waitYesNoAnswer());
    }

    IEnumerator waitYesNoAnswer()
    {
        clickedYes = false;
        clickedNo = false;
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
        upgradeText.text = "really?!";
 
        while (clickedYes == false && clickedNo == false)
        {
            yield return 0;
        }
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        if (clickedNo)
        {
            upgradeText.text = "Ascend";
            ascending = false;
            yield break;
        }

        //2. load players current level in local variable 
        PlayerLevel = PlayerPrefs.GetInt(PlayerLevelKey, 1);

        //3. zap all playerprefs
        PlayerPrefs.DeleteAll();

        //increment player level
        PlayerLevel++;
        
        //4. store  players current level
        PlayerPrefs.SetInt(PlayerLevelKey, PlayerLevel);
        
        //5. reload main scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void yesClick()
    {
        clickedYes = true;
    }

    public void noClick()
    {
        clickedNo = true;
    }

    private bool HasCastlesRequired()
    {
        if (castleReference.NumberOwned < Fib(PlayerLevel+3))
        {
            return false;
        }

        return true;
    }
    
    private int Fib(int aIndex)
    {
        var n1 = 0;
        var n2 = 1;
        for(var i = 1; i < aIndex; i++)
        {
            var tmp = n1 + n2;
            n1 = n2;
            n2 = tmp;
        }
        Debug.Log($"castles required:{n1}");
        return n1;
    }
    private void FixedUpdate()
    {
        //TODO change resourcesRequired to match GDD requirements for ascend
        ascendButton.image.color = HasCastlesRequired() ? Color.green : Color.red;
    }

    private void Start()
    {
        ascending = false;
        PlayerLevel = PlayerPrefs.GetInt(PlayerLevelKey, 1);
        playerLevelText.text = $"Player Level:{PlayerLevel}";

        var basePrice = startingStore.GetActualPrice(0);
        if (resource.CurrentAmount < basePrice)
            resource.CurrentAmount = basePrice;
        
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        ascendButton.image.color = Color.red;
    }
}
