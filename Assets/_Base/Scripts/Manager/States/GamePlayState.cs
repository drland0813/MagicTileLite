using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Drland.MagicTileLite;
using MagicTileLite.Scripts.Mics;
using UIManager = Common.UI.UIManager;

public class GamePlayState : State
{
    GamePlayController _controller;
    GamePlayView _view;

    protected override void OnEnter()
    {
        base.OnEnter();
        _view = GameFlow._instance.ShowUI<GamePlayView>("GamePlayView");
        _controller = _view.GetComponent<GamePlayController>();
        _controller.Init(PlayerPrefs.GetInt(GameConstants.KEY_PREF_GAME_SPEED, 1));
    }

    void OnEndGame()
    {
        GameFlow.RequestStateChange(this, new MainMenuPayload());
    }

    protected override void OnExit()
    {
        base.OnExit();
        UIManager.Instance.ReleaseUI(_view, true);
    }
}
