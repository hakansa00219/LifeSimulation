using System;
using Life.ScriptableObjects;
using UnityEngine;

namespace Life.Controllers
{
    [RequireComponent(typeof(Camera))]
    [ExecuteAlways]
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private SimulationSettings simulationSettings;
        
        private Camera _camera = null;

        private void OnEnable()
        {
            _camera ??= gameObject.GetComponent<Camera>();
            simulationSettings.OrthographicSizeChanged += OnOrthographicSizeChanged;
        }

        private void OnDisable()
        {
            simulationSettings.OrthographicSizeChanged -= OnOrthographicSizeChanged;
        }

        private void OnOrthographicSizeChanged(float newOrthoSize)
        {
            _camera.orthographicSize = newOrthoSize;
        }
    }
}