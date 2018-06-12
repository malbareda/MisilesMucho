using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	#region Delegates
	public delegate void OnKillAction();
	#endregion Delegates

	#region Evends
	public OnKillAction OnKill;
	#endregion Evends

	#region Constants
	private float LIFETIME = 6;
	private float SPEED = -5;
	#endregion Constants

	#region Fields
	private Vector3 direction;
	private float timer;
	#endregion Fields

	#region Properties
	public Vector3 Direction { set { direction = value; } }
    #endregion Properties

    public NavMeshAgent agent;
    public GameSceneManager game;

	#region Methods
	public void Update ()
	{
		timer += Time.deltaTime;

		if (timer >= LIFETIME)
		{
			GameObject.Destroy(this.gameObject);
            return;
		}



        /*this.transform.position = new Vector3
		(
			this.transform.position.x + SPEED * Time.deltaTime * direction.x, 
			this.transform.position.y,
			this.transform.position.z + SPEED * Time.deltaTime * direction.z
		);*/
        ;
        agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
	}

	public void OnTriggerEnter (Collider collider)
	{
		if (collider.GetComponent<BulletController>() != null)
		{
			if (OnKill != null)
			{
				OnKill();
			}

			GameObject.Destroy(this.gameObject);
			GameObject.Destroy(collider.gameObject);
		}
	}
    
    #endregion Methods
}