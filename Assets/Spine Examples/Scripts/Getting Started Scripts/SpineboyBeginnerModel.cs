/******************************************************************************
 * Spine Runtimes License Agreement
 * Last updated May 1, 2019. Replaces all prior versions.
 *
 * Copyright (c) 2013-2019, Esoteric Software LLC
 *
 * Integration of the Spine Runtimes into software or otherwise creating
 * derivative works of the Spine Runtimes is permitted under the terms and
 * conditions of Section 2 of the Spine Editor License Agreement:
 * http://esotericsoftware.com/spine-editor-license
 *
 * Otherwise, it is permitted to integrate the Spine Runtimes into software
 * or otherwise create derivative works of the Spine Runtimes (collectively,
 * "Products"), provided that each user of the Products must obtain their own
 * Spine Editor license and redistribution of the Products in any form must
 * include this license and copyright notice.
 *
 * THIS SOFTWARE IS PROVIDED BY ESOTERIC SOFTWARE LLC "AS IS" AND ANY EXPRESS
 * OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
 * OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN
 * NO EVENT SHALL ESOTERIC SOFTWARE LLC BE LIABLE FOR ANY DIRECT, INDIRECT,
 * INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
 * BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES, BUSINESS
 * INTERRUPTION, OR LOSS OF USE, DATA, OR PROFITS) HOWEVER CAUSED AND ON ANY
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
 * EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *****************************************************************************/

using UnityEngine;
using System.Collections;

namespace Spine.Unity.Examples {
	[SelectionBase]
	public class SpineboyBeginnerModel : MonoBehaviour {

		#region Inspector
		[Header("Current State")]
		public SkeletonBodyState state;
		public bool facingLeft;
		[Range(-1f, 1f)]
		public float currentSpeed;

		[Header("Balance")]
		public float shootInterval = 0.12f;
		#endregion

		float lastShootTime;
		public event System.Action ShootEvent;	// Lets other scripts know when Spineboy is shooting. Check C# Documentation to learn more about events and delegates.

		#region API
		public void TryJump () {
			StartCoroutine(JumpRoutine());
		}

		public void TryShoot () {
			float currentTime = Time.time;

			if (currentTime - lastShootTime > shootInterval) {
				lastShootTime = currentTime;
				if (ShootEvent != null) ShootEvent();	// Fire the "ShootEvent" event.
			}
		}

		public void TryMove (float speed) {
			currentSpeed = speed; // show the "speed" in the Inspector.

			if (speed != 0) {
				bool speedIsNegative = (speed < 0f);
				facingLeft = speedIsNegative; // Change facing direction whenever speed is not 0.
			}

			if (state != SkeletonBodyState.Jumping) {
				state = (speed == 0) ? SkeletonBodyState.Idle : SkeletonBodyState.Running;
			}

		}
		#endregion

		IEnumerator JumpRoutine () {
			if (state == SkeletonBodyState.Jumping) yield break;	// Don't jump when already jumping.

			state = SkeletonBodyState.Jumping;

			// Fake jumping.
			{
				var pos = transform.localPosition;
				const float jumpTime = 1.2f;
				const float half = jumpTime * 0.5f;
				const float jumpPower = 20f;
				for (float t = 0; t < half; t += Time.deltaTime) {
					float d = jumpPower * (half - t);
					transform.Translate((d * Time.deltaTime) * Vector3.up);
					yield return null;
				}
				for (float t = 0; t < half; t += Time.deltaTime) {
					float d = jumpPower * t;
					transform.Translate((d * Time.deltaTime) * Vector3.down);
					yield return null;
				}
				transform.localPosition = pos;
			}

			state = SkeletonBodyState.Idle;
		}

	}

	public enum SkeletonBodyState {
		Idle,
		Running,
		Jumping
	}
}
