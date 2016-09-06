﻿using UnityEngine;
using System.Collections;

public abstract class PlayerWeapon : Weapon {

    public Vector3 sideFireOffset;
    public int[] expForLevel = new int[_MAX_LEVEL - 1];
    public Color color;

    public int experience { get; private set; }
    public int level { get; private set; }

    private const int _MAX_LEVEL = 5;

    protected Vector3 _posRightFire;
    protected Vector3 _posLeftFire;

    void Start()
    {
        // initial properties
        experience = 0;
        level = 1;
    }

    public void addExperience(int exp)
    {
        // ignore any exp after maxed out
        if (isMaxLevel())
            return;

        // add
        experience += exp;

        // check upgrade
        if (level < _MAX_LEVEL)
        {
            int expForNextLevel = getExpForNextLevel();
            if (experience >= expForNextLevel)
            {
                experience -= expForNextLevel;
                level += 1;

                // reach max level
                if (isMaxLevel())
                    experience = getExpForLevel(_MAX_LEVEL);
            }
         }
    }

    public int getExpForLevel(int lv)
    {
        int exp = 0;
        if (lv >= 2 && lv <= _MAX_LEVEL)
            exp = expForLevel[lv - 2];
        else
            Debug.LogWarning("Invalid requested level: " + lv.ToString());

        return exp;
    }

    public int getExpForNextLevel()
    {
        return getExpForLevel(level + 1);
    }

    public bool isMaxLevel()
    {
        return level == _MAX_LEVEL;
    }

    protected void updateSideFirePosition()
    {
        // positions of side fires
        _posRightFire = transform.position + sideFireOffset;
        _posLeftFire = transform.position + new Vector3(-sideFireOffset.x, 0.0f, sideFireOffset.z);
    }
}