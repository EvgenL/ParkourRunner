using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Pick_Ups
{
    public abstract class PickUp : MonoBehaviour
    {
        protected abstract void Pick();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Pick();
            }
        }

    }
}
