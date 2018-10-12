﻿using UnityEngine;
using UnityEngine.UI;

namespace ShootAR.Menu
{
	/// <summary>
	/// A button used in the round selection menu.
	/// (Attach it to a button)
	/// </summary>
	[RequireComponent(typeof(Button))]
	public class RoundSelectButton : MonoBehaviour
	{
		/// <summary>
		/// the number labelled on the button
		/// </summary>
		private int numberOnLabel;

		[SerializeField] private RoundSelectMenu menu;

		private void Start()
		{
			if (menu == null) throw new UnityException("Round Select Menu not found!");

			string label = GetComponentInChildren<Text>().text;
			if (int.TryParse(label, out numberOnLabel))
			{
				GetComponent<Button>().onClick.AddListener(ChangeLevelIndex);
			}
			else
				Debug.LogError($"{label} is not an acceptable number. Try labeling" +
					" the button with an integer and without using spaces, letters " +
					"and other symbols. ('+', '-', '.' and 'e' are allowed)");
		}

		/// <summary>
		/// Adds the number on the label of the button to the level index.
		/// </summary>
		public void ChangeLevelIndex()
		{
			menu.RoundToPlay += numberOnLabel;
		}
	}
}