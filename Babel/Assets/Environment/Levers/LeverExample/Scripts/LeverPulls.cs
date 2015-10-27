﻿using System.Collections;
using Assets.Core.InteractableObjects;
using UnityEngine;

namespace Assets.Environment.Levers.LeverExample.Scripts
{
    public class LeverPulls : MonoBehaviour, IInteractable
    {

        private Color _oldColor;
        private bool _hasBeenPulled;

        void Start()
        {
            _oldColor = GetComponent<Renderer>().material.color;
        }

        public bool HasBeenActivated()
        {
            return _hasBeenPulled;
        }

        public void Interact()
        {
            if(!_hasBeenPulled)
                StartCoroutine(ChangeColor());
        }

        IEnumerator ChangeColor()
        {
            GetComponent<Renderer>().material.color = Color.red;
            _hasBeenPulled = true;
            yield return new WaitForSeconds(5);
            GetComponent<Renderer>().material.color = _oldColor;
            _hasBeenPulled = false;
        }
    }
}
