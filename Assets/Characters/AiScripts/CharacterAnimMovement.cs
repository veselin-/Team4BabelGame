using UnityEngine;
using System.Collections;
using Assets.Core.Configuration;

public class CharacterAnimMovement : MonoBehaviour {


	Animator anim;
	NavMeshAgent agent;
	Vector2 smoothDeltaPosition = Vector2.zero;
	Vector2 velocity = Vector2.zero;

	float m_TurnAmount;
	float m_ForwardAmount;

    private Footsteps footsteps;

	//[SerializeField] float m_MoveSpeedMultiplier = 1f;
	[SerializeField] float m_AnimSpeedMultiplier = 1f;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();

	    footsteps = GetComponentInChildren<Footsteps>();

		agent.updateRotation = true;
		agent.updatePosition = true;
		anim.applyRootMotion = true;
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 desiredVelocity = agent.desiredVelocity; 
		if (desiredVelocity != Vector3.zero) {

			CharacterMove(agent.desiredVelocity);
		} else {
			CharacterMove(Vector3.zero);
			anim.SetFloat("Turn", 0);
			//Debug.Log("else");

		}
	}

	void Idle()
	{

	}

	void CharacterMove(Vector3 move)
	{
		// convert the world relative moveInput vector into a local-relative
		// turn amount and forward amount required to head in the desired
		// direction.
		if (move.magnitude > 1f) move.Normalize();
		move = transform.InverseTransformDirection(move);

		//move = Vector3.ProjectOnPlane(move, m_GroundNormal);
		m_TurnAmount = Mathf.Atan2(move.x, move.z);
		m_ForwardAmount = move.z;


		UpdateAnimator(move);
	}

	void UpdateAnimator(Vector3 move)
	{
		// update the animator parameters
		anim.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
		anim.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);

		// the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
		// which affects the movement speed because of the root motion.
		if (move.magnitude > 0)
		{
			anim.speed = m_AnimSpeedMultiplier;
		}

	}

    public void StartAdjustPosition(GameObject interactable)
    {
        StartCoroutine(AdjustPosition(interactable));
    }

   IEnumerator AdjustPosition(GameObject interactable)
   {
       Vector3 fwd;
       Vector3 target;


        agent.updateRotation = false;

            fwd = transform.forward.normalized;
            fwd.y = 0f;

            target = (new Vector3(interactable.transform.position.x, transform.position.y, interactable.transform.position.z) - transform.position).normalized;

            while (Vector3.Dot(fwd, target) < 0.98f)
            {

                Debug.DrawLine(fwd, fwd*10, Color.red, 1f);

                fwd = transform.forward.normalized;
                fwd.y = 0f;

                target = (new Vector3(interactable.transform.position.x, transform.position.y, interactable.transform.position.z) - transform.position).normalized;

                float step = Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards(fwd, target, 6 * step, 0.0F);
                newDir.y = 0;
                transform.rotation = Quaternion.LookRotation(newDir);
                Debug.Log("running coroutine " + Vector3.Dot(fwd, target));
                yield return new WaitForEndOfFrame();
            }
       
        agent.updateRotation = true;
    }


    public void Footstep()
    {
        if(m_ForwardAmount > 0.1)
        footsteps.RandomFootsteps();
    }
}
