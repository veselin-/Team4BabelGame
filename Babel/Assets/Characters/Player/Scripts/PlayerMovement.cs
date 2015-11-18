using System.Linq;
using Assets.Characters.AiScripts;
using Assets.Characters.AiScripts.States;
using Assets.Core.Configuration;
using Assets.Core.InteractableObjects;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Characters.Player.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private AiMovement _ai;

        private UiController _uiControl;
		private float _touchCounter = 0;
		private bool _tap = false;			//ugly boolean for detecting tap
		private bool _canMove = false;		//ugly boolean for detecting when the player can move
		public bool _isTouchDown = false;	//ugly boolean which runs once needed to reset the Input.GetMouseButtonDown in the GetMouseButton state
		private CameraManager _cameraManager;
        // Use this for initialization
        void Start ()
        {
            _agent = GetComponent<NavMeshAgent>();
            _ai = GetComponent<AiMovement>();
			_cameraManager = GameObject.FindObjectOfType<CameraManager> ().GetComponent<CameraManager> ();
            _uiControl = GameObject.FindGameObjectWithTag(Constants.Tags.GameUI).GetComponent<UiController>();
        }
	
        // Update is called once per frame
        void Update ()
        {
			//ugly code fixing camera drag and player movement. 
			if (Input.GetMouseButton (0)) {
				_touchCounter += Time.deltaTime;
				if(_touchCounter < 0.3f)
				{
					_tap = true;
					_cameraManager.isCharacterMoving = true;
				}
				else
				{
					if(!_isTouchDown)
					{
						_isTouchDown = true;
						_tap = false;
						_cameraManager.isTouchDown = true;
					}
					_cameraManager.isCharacterMoving = false;
				}
			}

			if (Input.GetMouseButtonUp (0)) {
				_cameraManager.isCharacterMoving = false;
				_isTouchDown = false;
				if(_tap)
				{
					_canMove = true;
				}
				_touchCounter = 0;
			}

			if (!_canMove) return;

			_tap = false;
			_canMove = false;
            //if (!Input.GetMouseButtonDown(0)) return;

            var ts = Input.touches;
            if (ts.Length > 1 || (ts.Length > 0 && EventSystem.current.IsPointerOverGameObject(ts[0].fingerId))
                || EventSystem.current.IsPointerOverGameObject()) return;
            
            // Find all object in ray, and sort them by distance to object.
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, 100);
            hits = hits.OrderBy(h => h.distance).ToArray();

            var state = hits.Select(hit => GameObjectToState(hit.transform.gameObject, hit))
                    .FirstOrDefault(s => s != null);

            if (state != null)
                _ai.AssignNewState(state);
        }

        IState GameObjectToState(GameObject other, RaycastHit hit)
        {
            if (other.GetComponent<IInteractable>() != null)
            {
                return new InteractWithNearestState(_agent, other);
            }
            if (other.GetComponent<ICollectable>() != null)
            {
                return new PickupItemState(_agent, hit.transform.gameObject);
            }
            if(other.tag == Constants.Tags.Floor)
            {
                return new GoSomewhereAndWaitState(_agent, hit.point);
            }
            if (other.tag == Constants.Tags.AddNewSign)
            {
                _uiControl.NewSignCreation(other.GetComponent<NewSign>().ID);

                return new GoSomewhereAndWaitState(_agent, hit.point);
            }
            return null;
        }
    }
}
