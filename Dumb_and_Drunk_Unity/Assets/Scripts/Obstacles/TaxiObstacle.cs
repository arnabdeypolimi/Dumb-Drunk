﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxiObstacle : MonoBehaviour
{
    [SerializeField]
    private float AlarmRange, LightsTimer;

    [SerializeField]
    GameObject Lights;

    bool AlreadyActivated = false, LightsON = false;
    public LayerMask[] PlayersLayers = new LayerMask[4];

    private void OnCollisionEnter(Collision collision)
    {
        if (!AlreadyActivated && collision.gameObject.layer >= 9 && collision.gameObject.layer <= 12)
        {
            DebugText.instance.Audio("Alarm");

            for (int i = 0; i < PlayersLayers.Length; i++)
            {
                Collider[] Player = new Collider[1];
                if(Physics.OverlapSphereNonAlloc(transform.position, AlarmRange, Player, PlayersLayers[i]) > 0)
                    Player[0].gameObject.GetComponent<PlayerObstacleManager>().Taxi(transform.position);

            }

            AlreadyActivated = true;
            LightSwitch();
        }
    }

    void LightSwitch()
    {
        LightsON = !LightsON;
        Lights.SetActive(LightsON);
        Invoke("LightSwitch", LightsTimer);

    }
}
