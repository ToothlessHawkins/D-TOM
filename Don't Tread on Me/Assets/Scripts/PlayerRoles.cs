﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TeamUtility.IO;

public class PlayerRoles : MonoBehaviour {

    public PlayerID commander;
    public PlayerID gunner;
    public PlayerID driver;
    public PlayerID engineer;

    public GameObject player;

    public Texture a;
    public Texture b;
    public Texture x;
    public Texture y;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetComboTextures(Dictionary<List<string>, GameObject[]> combos)
    {
        foreach (KeyValuePair<List<string>, GameObject[]> combo in combos)
        {
            if (combo.Key.Count != combo.Value.Length)
            {
                print("YOU DONE FUCKED UP!! Size of button collection and combo collection do not match");
                return;
            }

            for (int i = 0; i < combo.Value.Length; i++)
            {
                switch (combo.Key[i])
                {
                    case "Button A":
                        combo.Value[i].GetComponent<RawImage>().texture = a;
                        break;
                    case "Button B":
                        combo.Value[i].GetComponent<RawImage>().texture = b;
                        break;
                    case "Button X":
                        combo.Value[i].GetComponent<RawImage>().texture = x;
                        break;
                    case "Button Y":
                        combo.Value[i].GetComponent<RawImage>().texture = y;
                        break;
                    default:
                        print("Check the values of your ability combo, unrecognized type found");
                        break;
                }
            }
        }
    }

    public void DisplayPanel(Animator anim, params GameObject[] panels)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].gameObject.SetActive(true);
        }
        anim.Play("panelSlideIn");
    }

    public void HidePanel(Animator anim, params GameObject[] panels)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].gameObject.SetActive(false);
        }
        anim.Play("panelSlideOut");
    }

    public int SelectAmmo(List<string> currentCombo, Dictionary<Gunner.AmmoTypes, List<string>> collection)
    {
        foreach (KeyValuePair<Gunner.AmmoTypes, List<string>> combo in collection)
        {
            if (combo.Value.SequenceEqual(currentCombo))
            {
                return (int)combo.Key;
            }
        }

        return -1;
    }

    public int SelectAmmo(List<string> currentCombo, Dictionary<Commander.AmmoTypes, List<string>> collection)
    {
        foreach (KeyValuePair<Commander.AmmoTypes, List<string>> combo in collection)
        {
            if (combo.Value.SequenceEqual(currentCombo))
            {
                return (int)combo.Key;
            }
        }

        return -1;
    }

    public void DisplayCombo(List<string> currentCombo, Dictionary<List<string>, GameObject[]> collection)
    {
        foreach (KeyValuePair<List<string>, GameObject[]> buttonCombo in collection)
        {
            for (int i = 0; i < currentCombo.Count; i++)
            {
                if (currentCombo[i] == buttonCombo.Key[i])
                {
                    Color color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    buttonCombo.Value[i].GetComponent<RawImage>().color = color;
                }
                else
                {
                    buttonCombo.Value[i].transform.parent.gameObject.SetActive(false); // need to clean this
                }
            }
        }
    }

    public void ResetCombo(Dictionary<List<string>, GameObject[]> collection)
    {
        Color color = new Color(1.0f, 1.0f, 1.0f, 0.39f);
        foreach (KeyValuePair<List<string>, GameObject[]> buttonCombo in collection)
        {
            buttonCombo.Value[0].transform.parent.gameObject.SetActive(true); // only need to do this once but should clean up

            for (int i = 0; i < buttonCombo.Value.Length; i++)
            {
                buttonCombo.Value[i].GetComponent<RawImage>().color = color;
            }
        }
    }
    
    public void SwapPanels(Transform currentPanel, Transform targetPanel)
    {   
        // Save references to the position of the panels to avoid issues
        Vector3 currentPanelPos = currentPanel.position;
        Vector3 targetPanelPos = targetPanel.position;

        currentPanel.position = targetPanelPos;
        targetPanel.position = currentPanelPos;
    }

    #region SwapToGunner
    public void SwapToGunner(Engineer currentRole)
    {
        Gunner targetRole = GetComponent<Gunner>();
        targetRole.playerID = currentRole.playerID;
        currentRole.playerID = gunner;

        gunner = targetRole.playerID;
        engineer = currentRole.playerID;
    }

    public void SwapToGunner(Driver currentRole)
    {
        Gunner targetRole = GetComponent<Gunner>();
        targetRole.playerID = currentRole.playerID;
        currentRole.playerID = gunner;

        gunner = targetRole.playerID;
        driver = currentRole.playerID;
    }

    public void SwapToGunner(Commander currentRole)
    {
        Gunner targetRole = GetComponent<Gunner>();
        targetRole.playerID = currentRole.playerID;
        currentRole.playerID = gunner;

        gunner = targetRole.playerID;
        commander = currentRole.playerID;
    }
    #endregion

    #region SwapToEnginner
    public void SwapToEngineer(Gunner currentRole)
    {
        Engineer targetRole = GetComponent<Engineer>();
        targetRole.playerID = currentRole.playerID;
        currentRole.playerID = engineer;

        engineer = targetRole.playerID;
        gunner = currentRole.playerID;
    }

    public void SwapToEngineer(Driver currentRole)
    {
        Engineer targetRole = GetComponent<Engineer>();
        targetRole.playerID = currentRole.playerID;
        currentRole.playerID = engineer;

        engineer = targetRole.playerID;
        driver = currentRole.playerID;
    }

    public void SwapToEngineer(Commander currentRole)
    {
        Engineer targetRole = GetComponent<Engineer>();
        targetRole.playerID = currentRole.playerID;
        currentRole.playerID = engineer;

        engineer = targetRole.playerID;
        commander = currentRole.playerID;
    }
    #endregion

    #region SwapToDriver
    public void SwapToDriver(Gunner currentRole)
    {
        Driver targetRole = GetComponent<Driver>();
        targetRole.playerID = currentRole.playerID;
        currentRole.playerID = driver;

        driver = targetRole.playerID;
        gunner = currentRole.playerID;
    }

    public void SwapToDriver(Engineer currentRole)
    {
        Driver targetRole = GetComponent<Driver>();
        targetRole.playerID = currentRole.playerID;
        currentRole.playerID = driver;

        driver = targetRole.playerID;
        engineer = currentRole.playerID;
    }

    public void SwapToDriver(Commander currentRole)
    {
        Driver targetRole = GetComponent<Driver>();
        targetRole.playerID = currentRole.playerID;
        currentRole.playerID = driver;

        driver = targetRole.playerID;
        commander = currentRole.playerID;
    }
    #endregion

    #region SwapToCommander
    public void SwapToCommander(Gunner currentRole)
    {
        Commander targetRole = GetComponent<Commander>();
        targetRole.playerID = currentRole.playerID;
        currentRole.playerID = commander;

        commander = targetRole.playerID;
        gunner = currentRole.playerID;
    }

    public void SwapToCommander(Engineer currentRole)
    {
        Commander targetRole = GetComponent<Commander>();
        targetRole.playerID = currentRole.playerID;
        currentRole.playerID = commander;

        commander = targetRole.playerID;
        engineer = currentRole.playerID;
    }

    public void SwapToCommander(Driver currentRole)
    {
        Commander targetRole = GetComponent<Commander>();
        targetRole.playerID = currentRole.playerID;
        currentRole.playerID = commander;

        commander = targetRole.playerID;
        driver = currentRole.playerID;
    }
    #endregion
}
