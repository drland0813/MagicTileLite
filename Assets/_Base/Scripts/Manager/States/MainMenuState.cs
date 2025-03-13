using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.UI;
using Drland.MagicTileLite;

public class MainMenuState : State
{
    MainMenuView _view;
    

    protected override void OnEnter()
    {
        base.OnEnter();
        _view = GameFlow._instance.ShowUI<MainMenuView>("MainMenuView");
        _view.Init();
        _view.OnPlay = GotoSelectLevelUI;
    }

    private void GotoSelectLevelUI()
    {
        var selectLevel = UIManager.Instance.ShowUIOnTop<SelectLevelUI>("SelectLevelUI"); 
        selectLevel.OnPlayGame = (i =>
        {
            GameFlow.RequestStateChange(this, new GamePlayPayload());
            UIManager.Instance.ReleaseUI(selectLevel, true);
        });
        UIManager.Instance.ReleaseUI(_view, true);
    }

    protected override void OnExit()
    {
        base.OnExit();
        UIManager.Instance.ReleaseUI(_view, false);
    }
}
