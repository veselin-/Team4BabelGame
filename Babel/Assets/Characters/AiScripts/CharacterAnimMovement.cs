using UnityEngine;
using System.Collections;

public class CharacterAnimMovement : MonoBehaviour {


	Animator anim;
	NavMeshAgent agent;
	Vector2 smoothDeltaPosition = Vector2.zero;
	Vector2 velocity = Vector2.zero;

	float m_TurnAmount;
	float m_ForwardAmount;

	//[SerializeField] float m_MoveSpeedMultiplier = 1f;
	[SerializeField] float m_AnimSpeedMultiplier = 1f;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();

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
}
