﻿using System.Collections;
using UnityEngine;

namespace ShootAR.Enemies
{
	[RequireComponent(typeof(CapsuleCollider))]
	public class Drone : Pyoopyoo
	{
		[SerializeField] protected const float ShootDelay = 5f;
		/// <summary>
		/// The point in time that the next shot is allowed.
		/// </summary>
		protected float nextShot;


		public override void ResetState() {
			throw new System.NotImplementedException();
		}

		protected override void Start() {
			base.Start();

			gameState = FindObjectOfType<GameState>();
		}

		protected void FixedUpdate() {
			if (!gameState.GameOver) {
				if (lastBullet == null && Time.time > nextShot) {
					Shoot();
					nextShot = Time.time + ShootDelay;
				}

				//OrbitAround();
			}
		}

		public override void Destroy() {
			base.Destroy();
			ReturnToPool<Drone>();
		}
	}
}
