﻿using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine;
using System.Collections;
using ShootAR;
using ShootAR.TestTools;

public class GameStateTests
{
	[UnityTest]
	public IEnumerator UseLastShotToHitCapsuleAndTakeBullets()
	{
		var gameState = GameState.Create(0);

		Player player = Player.Create(
			health: Player.MAXIMUM_HEALTH,
			camera: new GameObject().AddComponent<Camera>(),
			bullet: Bullet.Create(10),
			ammo: 1,
			gameState: gameState);
		Capsule capsule = Capsule.Create(
			type: Capsule.CapsuleType.Bullet,
			speed: 0,
			player: player,
			gameState: gameState);

		player.transform.LookAt(capsule.transform);
		player.Shoot();

		yield return new WaitWhile(() => player.Ammo > 0);

		Assert.That(!gameState.GameOver,
				"The game must not end, if restocked on bullets.");
	}

	[UnityTest]
	public IEnumerator UseLastShotToKillLastEnemy()
	{
		GameState gameState = GameState.Create(0);
		Camera camera = new GameObject("Camera").AddComponent<Camera>();
		Player player = Player.Create(
			health: 1,
			camera: camera,
			bullet: Bullet.Create(100f),
			ammo: 1,
			gameState: gameState);
		TestEnemy enemy = TestEnemy.Create(0, 0, 0, 10, 10, 10, gameState);
		GameManager.Create(player, gameState);
		
		camera.transform.LookAt(enemy.transform);
		var firedBullet = player.Shoot();
		firedBullet.gameObject.SetActive(true);

		yield return new WaitUntil(() => enemy == null);

		Assert.False(gameState.GameOver,
			"The game must not end if the last enemy dies by the last bullet.");
		Assert.True(gameState.RoundWon,
			"The round should be won when the last enemy dies by the last bullet.");
	}

	[TearDown]
	public void CleanUp()
	{
		var objects = Object.FindObjectsOfType<GameObject>();
		foreach (var o in objects) Object.Destroy(o.gameObject);
	}
}