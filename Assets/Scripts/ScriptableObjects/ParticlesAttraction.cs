using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Life.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ParticleAttractionTable", menuName = "Scriptable Objects/ParticleAttractionTable")]
    public class ParticlesAttraction : SerializedScriptableObject
    {
        [TableMatrix(HorizontalTitle = "Particle Types", VerticalTitle = "Particle Types", Labels = "GetLabels", SquareCells = true)]
        public float[,] AttractionTable = new float[1,1];
        


#if UNITY_EDITOR
        private (string, LabelDirection) GetLabels(float[,] array, TableAxis axis, int index)
        {
            string[] particles = new string[]
            {
                "White",
                "Red",
                "Brown",
                "Green",
                "Yellow",
                "Cyan"
            };

            return (particles[index].ToString(),
                axis is TableAxis.X ? LabelDirection.TopToBottom : LabelDirection.LeftToRight);
        }
        #endif
    }
}