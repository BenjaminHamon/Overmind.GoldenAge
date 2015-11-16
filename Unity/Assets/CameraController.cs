using Overmind.Unity;
using UnityEngine;

namespace Overmind.GoldenAge.Unity
{
	public class CameraController : MonoBehaviourBase
	{
		public override void Start()
		{
			transform = base.transform;
		}

		private new Transform transform;

		//public Transform Target;

		[SerializeField]
		private float distanceMininum = 10;
		[SerializeField]
		private float distanceMaximum = 100;
		[SerializeField]
		private float moveSpeed = 1;
		[SerializeField]
		private float zoomSpeed = 1;

		public override void Update()
		{
			Vector3 position = transform.localPosition;
			float scaledMoveSpeed = moveSpeed * position.y / distanceMaximum;

			if (Input.GetKey(KeyCode.UpArrow))
				position.z += scaledMoveSpeed;
			if (Input.GetKey(KeyCode.RightArrow))
				position.x += scaledMoveSpeed;
			if (Input.GetKey(KeyCode.DownArrow))
				position.z -= scaledMoveSpeed;
			if (Input.GetKey(KeyCode.LeftArrow))
				position.x -= scaledMoveSpeed;

			if (Input.GetAxis("Mouse ScrollWheel") > 0)
				position.y = Mathf.Max(position.y - zoomSpeed, distanceMininum);
			else if (Input.GetAxis("Mouse ScrollWheel") < 0)
				position.y = Mathf.Min(position.y + zoomSpeed, distanceMaximum);

			transform.localPosition = position;
		}
	}
}
