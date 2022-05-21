using UnityEngine;
using System.Collections;
using Spine.Unity;

namespace Spine.Unity.Examples
{
	public class SceletonView : MonoBehaviour
	{

		#region Inspector
		[Header("Components")]
		public SpineboyBeginnerModel model;
		public SkeletonAnimation skeletonAnimation;

		public AnimationReferenceAsset damaged, idle, attackSimple, attackDouble, attackSplash;
		public EventDataReferenceAsset footstepEvent;

		#endregion

		SpineBeginnerBodyState previousViewState;

		void Start()
		{
			if (skeletonAnimation == null) return;
		}


		void Update()
		{
			if (skeletonAnimation == null) return;
			if (model == null) return;

			if ((skeletonAnimation.skeleton.ScaleX < 0) != model.facingLeft)
			{   // Detect changes in model.facingLeft
				Turn(model.facingLeft);
			}

			// Detect changes in model.state
			var currentModelState = model.state;

			if (previousViewState != currentModelState)
			{
				PlayNewStableAnimation();
			}

			previousViewState = currentModelState;
		}

		void PlayNewStableAnimation()
		{
			var newModelState = model.state;
			Animation nextAnimation;

			// Add conditionals to not interrupt transient animations.


			if (newModelState == SpineBeginnerBodyState.Jumping)
			{
				nextAnimation = attackDouble;
			}
			else
			{
				if (newModelState == SpineBeginnerBodyState.Running)
				{
					nextAnimation = attackDouble;
				}
				else
				{
					nextAnimation = idle;
				}
			}

			skeletonAnimation.AnimationState.SetAnimation(0, nextAnimation, true);
		}


		[ContextMenu("Check Tracks")]
		void CheckTracks()
		{
			var state = skeletonAnimation.AnimationState;
			Debug.Log(state.GetCurrent(0));
			Debug.Log(state.GetCurrent(1));
		}

		#region Transient Actions

		public void Turn(bool facingLeft)
		{
			skeletonAnimation.Skeleton.ScaleX = facingLeft ? -1f : 1f;
			// Maybe play a transient turning animation too, then call ChangeStableAnimation.
		}
		#endregion

		#region Utility
		public float GetRandomPitch(float maxPitchOffset)
		{
			return 1f + Random.Range(-maxPitchOffset, maxPitchOffset);
		}
		#endregion
	}

}
