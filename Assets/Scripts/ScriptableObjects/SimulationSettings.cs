using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Life.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SimulationSettings", menuName = "Scriptable Objects/SimulationSettings")]
    public class SimulationSettings : ScriptableObject
    {
        public float orthographicSize;
    
        public int ParticleAmount;
        public float ParticleSize;
    
        [ReadOnly]
        public Vector2 xBounds;
        [ReadOnly]
        public Vector2 yBounds;

        public event Action<float> OrthographicSizeChanged;

        private void OnValidate()
        {
            
            CalculateBounds();
            UpdateSimulation();
        }

        private void UpdateSimulation()
        {
            OrthographicSizeChanged?.Invoke(orthographicSize);
        }

        private void CalculateBounds()
        {
            float aspectRatio = 16f / 9f;

            yBounds = new Vector2(-orthographicSize, orthographicSize);
            xBounds = new Vector2(-orthographicSize * aspectRatio, orthographicSize * aspectRatio);
        }
    }
}


