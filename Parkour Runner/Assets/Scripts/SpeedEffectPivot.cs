using UnityEngine;

public class SpeedEffectPivot : MonoBehaviour
{
    public Rigidbody Player;
    	
	void Update ()
    {
        transform.rotation = Quaternion.LookRotation(-Player.velocity, Vector3.up);
    }
}
