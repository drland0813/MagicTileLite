
using System;
using Drland.MagicTileLite.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Drland.MagicTileLite
{
    public enum InteractType
    {
        Touch,
        Hold
    }
    
    public abstract class Tile : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] protected Transform _pointToCheckAccurateInteraction;
        protected InteractType _interactType;
        protected TileUI _tileUI;

        
        private bool _isInteracted;

        private RectTransform _rectTransform;
        private bool _enable;

        public bool IsInteracted  => _isInteracted;
        public bool Enable 
        {
            set => _enable = value;
        }

        private float _timeAlive;
        
        protected virtual void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _tileUI = GetComponent<TileUI>();
        }
        
        private float CalcAccurateInteraction()
        {
            var checkPointY = GamePlayController.Instance.GetCheckPointPosition().y;
            var pointToCheckY = _pointToCheckAccurateInteraction.position.y;
            var accurateInteraction = 0f;
            if (!(pointToCheckY < checkPointY)) return accurateInteraction;
            
            var relativeY = Math.Abs(pointToCheckY - checkPointY);
            var height =  Math.Abs(pointToCheckY - transform.position.y);
            accurateInteraction = relativeY / height;
            
            return accurateInteraction;
        }
        
        protected virtual void OnEnable()
        {
            _enable = true;
            _isInteracted = false;
            if (_rectTransform.anchoredPosition3D == Vector3.zero) return;
            
            _rectTransform.anchoredPosition3D = Vector3.zero;
        }
        

        protected bool CanInteract()
        {
            return _enable && !_isInteracted;
        }

        protected virtual void Interact()
        {
            _isInteracted = true;
            var accurateInteraction = CalcAccurateInteraction();
            GamePlayController.Instance.AddScore(accurateInteraction, _interactType);
            GamePlayController.Instance.Background.TriggerLight();
        }
        
        public void UpdatePosition(Vector3 newPos)
        {
            _timeAlive += Time.deltaTime;
            transform.position = newPos;
        }

        public InteractType GetInteractType()
        {
            return _interactType;
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (!CanInteract()) return;
            
            _tileUI.PlayTouchEffect();
            Interact();
        }
    }
}

