﻿using UnityEngine;
using System.Collections;

public class Movement : Objective
{
    private GameObject player;
    private float initial_x;

    public override IEnumerator startObjective()
    {
        player = GameObject.FindWithTag("Player");
        initial_x = player.transform.position.x;
        GameControl.control.playerAgl = 2;
        yield return new WaitForSeconds(1);
        while (!isCompleted())
        {
            yield return new WaitForSeconds(1);
        }
        GameControl.control.playerAgl = 0;
    }

    public override bool isCompleted()
    {
        float current_x = player.transform.position.x;
        return (current_x > initial_x) || (initial_x > current_x);
    }

    public override string getDescription()
    {
        return "Use joystick to move player.";
    }
}
