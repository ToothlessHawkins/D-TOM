﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TeamUtility.IO;

public class ActiveReload : MonoBehaviour {

    private PlayerID gunner;

    //public Collider2D marker;
    //public Collider2D sweetSpot;
    //public GameObject activeReload;
    public GameObject marker;
    public GameObject sweetSpot;
    GameObject tank;
    rockets rockets;
    GameObject reloadOn;
    public bool reloading = true;
    private bool reloadAttempt = false;
    public int reloadSpeed;
    private float spotWidth = 20f;
    private float markerWidth = 7f;
	// Use this for initialization
	void Start () {
        tank = GameObject.Find("f");
        rockets = tank.GetComponent<rockets>();
        reloadOn = GameObject.FindGameObjectWithTag("Reloading");
        reloadOn.SetActive(false);
        reloadSpeed = 50;

        // this code is for managing player roles
        GameObject inputMngr = GameObject.Find("InputManager");
        gunner = inputMngr.GetComponent<PlayerRoles>().gunner;
	}

    // Update is called once per frame
    void Update()
    {
        if (reloading)
        {
            marker.transform.Translate(Vector3.right * Time.deltaTime * reloadSpeed);

            if (InputManager.GetAxis("Left Trigger", gunner) > 0 && !reloadAttempt) {

                if (marker.transform.position.x < sweetSpot.transform.position.x + spotWidth - 10 &&
                   marker.transform.position.x + markerWidth > sweetSpot.transform.position.x )
                {
                    //print("Good Reload");
                    rockets.rocketTrue = true;
                    marker.transform.position = new Vector3(175f, 46.68f, 0);//Vector3(74f, 68.89f, 0);
                    reloading = false;
                    //reloadOn.SetActive(false);
                    this.gameObject.SetActive(false);
                } else 
                {
                    reloadAttempt = true;
                    print("Reload Failed");
                    reloadSpeed = 25;
                }
            }
            if (marker.transform.position.x >= 309.0f)//220.0f)
            {   
                //print("Reload Complete" + marker.transform.position.x + "      " + marker.transform.position.y);
                rockets.rocketTrue = true;
                marker.transform.position = new Vector3(175f, 46.68f, 0);//Vector3(74f, 68.89f, 0);
                reloading = false;
                //reloadOn.SetActive(false);
                this.gameObject.SetActive(false);
                reloadAttempt = false;
                reloadSpeed = 50;
            }
        }

        else
        {
            // May want to draw line here and have active reload constantly being called?
        }
        

    }


    

}
