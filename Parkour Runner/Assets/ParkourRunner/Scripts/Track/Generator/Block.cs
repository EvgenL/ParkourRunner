using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ParkourRunner.Scripts.Track.Generator
{
    [ExecuteInEditMode]
    public class Block : MonoBehaviour
    {
        public Building[] Buildings;

        //Автоматически расставляем референсы в эдиторе
        void Update()
        {
            if (!Application.isPlaying)
            {
                    Buildings = GetComponentsInChildren<Building>();
                    foreach (var building in Buildings)
                    {
                        building.UpdateReferences();
                    }
            }
        }

        public enum BlockType
        {
            Houses,
            Flat,
            Start
        }

        public BlockType Type;
        public Block Next;
        public Block Prev;
        public Block Left;
        public Block Right;


        public void Generate()
        {
            foreach (var building in Buildings)
            {
                building.Generate();
            }
        }

        public void Clear()
        {
            foreach (var building in Buildings)
            {
                building.Clear();
            }
        }
    }
}
