using System;
using Enums;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector3 mousePosition;
    private Camera _cam;
    private ActorController _actorController;
    public CursorController cursorController;
    
    private void Awake()
    {
        TryGetComponent(out _actorController);
        _cam = Camera.main;
        if (cursorController == null) cursorController = GameObject.FindObjectsOfType<CursorController>()[0];
    }

    public void OnFire(InputValue value)
    {
        HandleMouseClick();
    }
    public void OnPause(InputValue value)
    {
        HandleTogglePause();
    }
    public void OnMousePosition(InputValue value)
    {
        var pos = value.Get<Vector2>();
        if (_cam != null)
        {
            var objectPos = _cam.ScreenToWorldPoint(pos);
            mousePosition = objectPos;
            cursorController.mousePosition = mousePosition;
        }
    }


    private void HandleTogglePause()
    {
        if (GameManager.GameState != GameState.Pause)
        {
            GameManager.SetGameState(GameState.Pause);
        }
        else
        {
            GameManager.SetGameState(GameState.Play);
        }
    }
    private void HandleMouseClick()
    {
        var hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        var objectTag = hit.collider.gameObject.tag;
        if (objectTag == "Ground")
        {
            _actorController.Act(GameActions.Move, mousePosition);
            return;
        }
    }
}