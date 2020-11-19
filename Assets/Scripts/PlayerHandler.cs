﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
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
    [SerializeField] private Text castlesNeeded;

    private AudioHandler audiohandler;
    public static int PlayerLevel { get; private set; }
    private bool clickedYes, clickedNo, ascending;
    private static string PlayerLevelKey => "Player_level";

    public void Ascend()
    {
        //don't run again if we are already waiting for an answer
        if (ascending) return;
        
        //0. check if user has enuff resources to ascend
        if (!HasCastlesRequired())
        {
            return;
        }
        
        ascending = true;
        upgradeText.text = "ascending...";
        StartCoroutine(waitYesNoAnswer());
    }

    IEnumerator waitYesNoAnswer()
    {
        //1. ask user if they really want to zap all data and start over, to level up
        clickedYes = false;
        clickedNo = false;
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
        upgradeText.text = "really?!";
        
        //1.5 implement yes/no logic
        //Wait for the button to be pressed
        while (clickedYes == false && clickedNo == false)
        {
            yield return 0;
        }
        if (clickedNo)
        {
            upgradeText.text = "Ascend";
            yesButton.gameObject.SetActive(false);
            noButton.gameObject.SetActive(false);
            ascending = false;
            yield break;
        }
        //don't click more buttons, and level up
        yesButton.interactable = false;
        noButton.interactable = false;
        upgradeText.text = "leveling up!";
        
        //music and sfx
        audiohandler.Play("newLevel");
        audiohandler.StopMusic();
        yield return new WaitForSeconds(5);
        audiohandler.PlayMusic();
        
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
        if (castleReference.NumberOwned < Fib(PlayerLevel+2))
        {
            return false;
        }

        return true;
    }
    
    private int Fib(int aIndex)
    {
        var n1 = 0;
        var n2 = 1;
        for(var i = 0; i < aIndex; i++)
        {
            var tmp = n1 + n2;
            n1 = n2;
            n2 = tmp;
        }
        return n1;
    }
    private void FixedUpdate()
    {
        ascendButton.image.color = HasCastlesRequired() ? Color.green : Color.red;
    }

    private void Start()
    {
        //get audioHandler
        audiohandler = FindObjectOfType<AudioHandler>();
        
        //init ascending logics and UI 
        ascending = false;
        PlayerLevel = PlayerPrefs.GetInt(PlayerLevelKey, 1);
        playerLevelText.text = $"Player Level:{PlayerLevel}";
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        castlesNeeded.text = $"Needed: {Fib(PlayerLevel + 2)}";
        
        var basePrice = startingStore.GetActualPrice(0);
        if (resource.CurrentAmount < basePrice)
            resource.CurrentAmount = basePrice;
    }
}
