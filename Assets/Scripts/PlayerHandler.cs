﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private Resource.Resource resource;
    [SerializeField] private int resourcesRequired;
    [SerializeField] private Text upgradeText;
    [SerializeField] private Text playerLevelText;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    [SerializeField] private Button ascendButton;
    
    private int playerLevel;
    private bool clickedYes, clickedNo, ascending;
    private static string PlayerLevelKey => "Player_level";
    
    
    public void Ascend()
    {
        //don't run again if we are already waiting for an answer
        if (ascending) return;
        
        //0. check if user has enuff resources to ascend
        if (resource.CurrentAmount < resourcesRequired)
        {
            return;
        }
        
        ascending = true;
        
        upgradeText.text = "ascending...";
        Debug.Log("ascending...");
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
        playerLevel = PlayerPrefs.GetInt(PlayerLevelKey, 0);

        //3. zap all playerprefs
        PlayerPrefs.DeleteAll();

        //increment player level
        playerLevel++;
        
        //4. store  players current level
        PlayerPrefs.SetInt(PlayerLevelKey, playerLevel);
        
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

    private void FixedUpdate()
    {
        if (resource.CurrentAmount < resourcesRequired)
        {
            ascendButton.image.color = Color.red;
        }
        else
        {
            ascendButton.image.color = Color.green; 
        }
    }

    private void Start()
    {
        ascending = false;
        playerLevel = PlayerPrefs.GetInt(PlayerLevelKey, 0);
        playerLevelText.text = $"Player Level:{playerLevel}";
        
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        ascendButton.image.color = Color.red;
    }
}