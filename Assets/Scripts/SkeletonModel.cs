using UnityEngine;
using Spine.Unity;

namespace Game
{
	public enum SkeletonBodyState
	{
		Idle,
		UnderAttack,
		AttackSimple,
		AttackDouble,
		AttackSplash
	}

	[SelectionBase]
	public class SkeletonModel : MonoBehaviour
	{
		#region Inspector
		[Header("Components")]
		public SkeletonAnimation skeletonAnimation;
		public AnimationReferenceAsset damaged, idle, attackSimple, attackDouble, attackSplash;
		[Header("Current State")]
		public SkeletonBodyState state;
		#endregion
		public event System.Action AttackEvent;  // Lets other scripts know when Spineboy is shooting. Check C# Documentation to learn more about events and delegates.



	}


}
