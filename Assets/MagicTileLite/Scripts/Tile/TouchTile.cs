using System;
using UnityEngine.EventSystems;

namespace Drland.MagicTileLite
{
    public class TouchTile : Tile
    {
        protected override void Awake()
        {
            base.Awake();
            _interactType = InteractType.Touch;
        }

        protected override void Interact()
        {
            _tileUI.DoInteract();
            base.Interact();
        }
    }
}