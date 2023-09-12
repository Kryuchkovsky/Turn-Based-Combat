using System.Collections;
using UnityEngine;

namespace _GameLogic.Gameplay.Units
{
    public class UnitRepresentation : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _modelMeshRenderer;
        [SerializeField] private float _damageColorPaintingDuration = 0.5f;
        [SerializeField] private float _baseColorPaintingDuration = 0.5f;

        private Coroutine _paintingCoroutine;
        private float _generalPaintingDuration;
        private float _paintingDuration;

        private void Awake()
        {
            _generalPaintingDuration = _damageColorPaintingDuration + _baseColorPaintingDuration;
        }

        public void OnDamageGot()
        {
            if (_paintingCoroutine != null)
            {
                StopCoroutine(_paintingCoroutine);
                _paintingDuration = 0;
            }

            _paintingCoroutine = StartCoroutine(PaintDamageColor());
        }

        private IEnumerator PaintDamageColor()
        {
            while (_paintingDuration <= _generalPaintingDuration)
            {
                _paintingDuration += Time.deltaTime;
                var mixValue = _paintingDuration <= _damageColorPaintingDuration
                    ? _paintingDuration / _damageColorPaintingDuration
                    : 1 - (_paintingDuration - _damageColorPaintingDuration) / _baseColorPaintingDuration;
                _modelMeshRenderer.material.SetFloat("_DamageColorMixValue", Mathf.Clamp01(mixValue));
                yield return null;
            }
        }
        
        public void SetModelVisibility(bool isVisible) => _modelMeshRenderer.gameObject.SetActive(isVisible);
    }
}