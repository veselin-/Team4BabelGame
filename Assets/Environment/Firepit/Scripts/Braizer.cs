using System.Linq;
using Assets.Core.Configuration;
using Assets.Core.InteractableObjects;
using UnityEngine;

namespace Assets.Environment.Braizer.Scripts
{
    public class Braizer : MonoBehaviour, IInteractable
    {
        public GameObject TorchPrefab;
        public GameObject StickPrefab;
        public bool StartLighted;

        public GameObject[] InteractPoint;

        public bool OnFire {
            set { _psystem.enableEmission = value; }
            get { return _psystem.enableEmission; }
        }

        private ParticleSystem _psystem;

        void Awake()
        {
            _psystem = transform.FindChild("fire").GetComponent<ParticleSystem>();
            OnFire = StartLighted;
			if(StartLighted)
			{
				if(!GetComponent<AudioSource> ().isPlaying)
				{
					GetComponent<AudioSource> ().Play ();
				}

			}
        }

        public bool HasBeenActivated()
        {
            return OnFire;
        }

        public GameObject Interact(GameObject pickup)
        {
            if (!CanThisBeInteractedWith(pickup)) return pickup;

            if (OnFire && pickup.tag == Constants.Tags.Stick)
            {
                Destroy(pickup);
                return Instantiate(TorchPrefab);
            }

            if (!OnFire && pickup.tag == Constants.Tags.Torch)
            {
                OnFire = true;
                return pickup;
            }

            return pickup;
        }

        public bool CanThisBeInteractedWith(GameObject pickup)
        {
            if (pickup == null) return false;
            return OnFire && pickup.tag == Constants.Tags.Stick || 
                !OnFire && pickup.tag == Constants.Tags.Torch;
        }

        Vector3 IInteractable.InteractPosition(Vector3 ai)
        {
            return InteractPoint.OrderBy(g => Vector3.Distance(g.transform.position, ai)).
                ToArray()[0].transform.position;
        }
    }
}
