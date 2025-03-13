using System;
using System.Collections;
using MagicTileLite.Scripts.Mics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Drland.MagicTileLite
{
    public class HoldTile : Tile, IPointerUpHandler
    {
        [SerializeField] private Transform  _holdArrow;
        [SerializeField] private Transform  _startPoint;
        [SerializeField] private Transform  _endPoint;

        private float _holdingTime;
        private bool _isHolding;
        private bool _isHoldProcessReachMaxPoint;
        private float _holdSpeed;

        protected override void Awake()
        {
            base.Awake();
            _interactType = InteractType.Hold;
        }

        protected override void OnEnable()
        {
            _isHolding = false;
            _holdingTime = 0;
            _isHoldProcessReachMaxPoint = false;
            _holdArrow.position = _startPoint.position;
            base.OnEnable();
        }

        protected override void Interact()
        {
            _tileUI.PlayChangeColorEffect();
            base.Interact();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!CanInteract()) return;
            
            PointerUpHandler();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (!CanInteract()) return;

            base.OnPointerDown(eventData);
            _isHolding = true;
            StartCoroutine(HoldCoroutine());
        }

        public void UpdateHoldSpeed(float tileSpeed)
        {
            _holdSpeed = tileSpeed / 2;
        }

        private void PointerUpHandler()
        {
            _isHolding = false;
            if (!_isHoldProcessReachMaxPoint) return;
            
            _tileUI.PlayFadeEffect();
            GamePlayController.Instance.AddBonusScore();
            GamePlayController.Instance.UI.Effect.ShowFloatTextUI(GameConstants.BONUS_HOLD_SCORE, transform.position);
        }

        private IEnumerator HoldCoroutine()
        {
            while (_isHolding)
            {
                _holdingTime += Time.deltaTime;

                var newPos = _holdArrow.transform.position;
                newPos.y += _holdingTime * _holdSpeed;
                var newY = Mathf.Clamp(newPos.y, _startPoint.position.y, _endPoint.position.y);
                newPos.y = newY;
                _holdArrow.transform.position = newPos;
                if (Math.Abs(_holdArrow.transform.position.y - _endPoint.position.y) < float.Epsilon)
                {
                    _isHoldProcessReachMaxPoint = true;
                    PointerUpHandler();
                    yield break;
                }
                yield return null;
            }
        }
    }
}