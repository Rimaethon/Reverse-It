using Cinemachine;
using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Managers;
using UnityEngine;
public class CameraOffsetController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    private CinemachineFramingTransposer _cameraTransposer;

    private void Awake()
    {
        if (playerSpriteRenderer == null)
            playerSpriteRenderer = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        _cameraTransposer = GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<CinemachineFramingTransposer>();
    }
    private void OnEnable()
    {
        EventManager.Instance.AddHandler<bool>(GameEvents.OnPlayerDirectionChange, ChangeOffsetBasedOnPlayerDirection);
        EventManager.Instance.AddHandler<bool> (GameEvents.OnCameraOffset, ChangeYOffsetBasedOnGravity);
    }
    private void OnDisable()
    {
        if (EventManager.Instance == null) return;
        EventManager.Instance.RemoveHandler<bool>(GameEvents.OnPlayerDirectionChange,
            ChangeOffsetBasedOnPlayerDirection);
        EventManager.Instance.RemoveHandler<bool>(GameEvents.OnCameraOffset, ChangeYOffsetBasedOnGravity);
    }
    private void ChangeYOffsetBasedOnGravity(bool isPlayerGravitated)
    {
        _cameraTransposer.m_TrackedObjectOffset.y = isPlayerGravitated ? -2 : 2;
    }
    private void ChangeOffsetBasedOnPlayerDirection(bool isPlayerFacingRight)
    {
        _cameraTransposer.m_TrackedObjectOffset.x = isPlayerFacingRight ? -2 : 2;
    }
}