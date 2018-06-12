using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
	#region Constants
	private float LIFETIME = 2;
	private float SPEED = 20;
	#endregion Constants
	
	#region Fields
	private Vector3 direction;
	private float timer;
	#endregion Fields

	#region Properties
	public Vector3 Direction { set { direction = value; } }
	#endregion Properties

	#region Methods
	public void Update ()
	{
		timer += Time.deltaTime;

		if (timer >= LIFETIME)
		{
			GameObject.Destroy(this.gameObject);
		}

		float angle = direction.y;
		this.transform.position = new Vector3
		(
			this.transform.position.x + SPEED * Time.deltaTime * Mathf.Cos(-(angle - 90) * Mathf.Deg2Rad), 
			this.transform.position.y,
			this.transform.position.z + SPEED * Time.deltaTime * Mathf.Sin(-(angle - 90) * Mathf.Deg2Rad)
		);
	}
	#endregion Methods
}