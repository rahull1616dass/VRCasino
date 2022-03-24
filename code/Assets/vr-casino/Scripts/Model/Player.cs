﻿using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    public Transform Transform { get; protected set; }
    public Hand Hand { get; protected set; }

    public bool IsHitting { get; set; }
    public int Score { get; set; }

    public List<ChipSO> currentChips = new List<ChipSO>();

    public int Bet { get; set; }
    public int CurrentBet { get; set; }

    private void Awake()
    {
        Transform = transform;

        Hand = new Hand();
    }

    public void Reset(PoolingSystem poolingSystem)
    {
        Hand.Reset(poolingSystem);

        IsHitting = true;
    }
}