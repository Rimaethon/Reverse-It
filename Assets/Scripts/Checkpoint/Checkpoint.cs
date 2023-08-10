﻿using System;
using Health_Damage;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Checkpoint
{
    public class Checkpoint : MonoBehaviour
    {
        private Vector3 m_RespawnLocation;
        private Animator m_CheckpointAnimator;
        [SerializeField] private string animatorActiveParameter = "isActive";
        [SerializeField] private GameObject checkpointActivationEffect;

        private void Awake()
        {
            m_RespawnLocation = gameObject.transform.position;
        }
        

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player") || collision.gameObject.GetComponent<Health>() == null) return;

            var playerHealth = collision.gameObject.GetComponent<Health>();
            playerHealth.SetRespawnPoint(m_RespawnLocation);

            if (CheckpointTracker.CurrentCheckpoint != null)
                CheckpointTracker.CurrentCheckpoint.m_CheckpointAnimator.SetBool(animatorActiveParameter, false);

            if (CheckpointTracker.CurrentCheckpoint != this && checkpointActivationEffect != null)
                Instantiate(checkpointActivationEffect, transform.position, Quaternion.identity, null);

            CheckpointTracker.CurrentCheckpoint = this;
            m_CheckpointAnimator.SetBool(animatorActiveParameter, true);
        }
    }
}