using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
	#region Delegates
	public delegate void OnHitEnemyAction();
	#endregion Delegates

	#region Evends
	public OnHitEnemyAction OnHitEnemy;
	#endregion Evends

	#region Unity Fields
	public GameObject bulletPrefab;
	#endregion Unity Fields

	#region Methods
	public void Update ()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject bullet = GameObject.Instantiate<GameObject>(bulletPrefab);
			bullet.transform.SetParent(this.transform.parent);
			bullet.transform.position = this.transform.position;
			bullet.GetComponent<BulletController>().Direction = this.transform.localEulerAngles;
		}
	}

	public void OnTriggerEnter (Collider collider)
	{
		if (collider.GetComponent<EnemyController>() != null)
		{
			if (OnHitEnemy != null)
			{
				OnHitEnemy();
			}

            print("git gud");
			//GameObject.Destroy(this);
			//GameObject.Destroy(this.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>());
		}
	}

    public static explicit operator PlayerController(GameObject v)
    {
        throw new NotImplementedException();
    }
    #endregion Methods
}
