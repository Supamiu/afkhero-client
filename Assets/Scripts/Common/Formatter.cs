using UnityEngine;
using System.Collections;
using AFKHero.Core.Event;

using System;

namespace AFKHero.Common{
	public static class Formatter {

		/// <summary>
		/// Alphabet pour les suffix
		/// </summary>
		private static readonly string[] digits = new string[30] {
			"",
			"K",
			"M",
			"B",
			"aa",
			"bb",
			"cc",
			"dd",
			"ee",
			"ff",
			"gg",
			"hh",
			"ii",
			"jj",
			"kk",
			"ll",
			"mm",
			"nn",
			"oo",
			"pp",
			"qq",
			"rr",
			"ss",
			"tt",
			"uu",
			"vv",
			"ww",
			"xx",
			"yy",
			"zz"
		};

		/// <summary>
		/// Formatte un Double pour un affichage basé sur un alphabet, plus lisible pour les grands nombres
		/// </summary>
		public static string Format(double number) {
			
			string result;

			// Nombre de digits dans le nombre (6 pour 1 million)
			int length = number == 0 ? 1 : (int) Math.Floor(Math.Log10(Math.Abs(number)) + 1);

			// Aucun traitement si le nombre est trop grand pour notre alphabet
			if (length > 3 * digits.Length) {
				result = number.ToString ();
			} else {
				string firstDigits;

				// On travaille que sur les 5 premiers digits
				if (length > 5) {
					firstDigits = Math.Floor (number / Math.Pow (10, length - 5)).ToString ();
				} else {
					firstDigits = number.ToString ();
				}

				int index = (int) Math.Ceiling((double) length / 3) - 1;
				string suffix = digits [index];

				// Découpage pour conserver 5 digits max dont 2 après la virgule
				if (length > 3) {
					int rest = (length % 3);
					if (rest == 1) {
						result = firstDigits.Substring (0, 1) + "." + firstDigits.Substring (1, 2) + suffix;
					} else if (rest == 2) {
						result = firstDigits.Substring (0, 2) + "." + firstDigits.Substring (2, 2) + suffix;
					} else {
						result = firstDigits.Substring (0, 3) + "." + firstDigits.Substring (3, 2) + suffix;
					}
				} else {
					result = firstDigits;
				}
			}

			return result;
		}
	}
}
