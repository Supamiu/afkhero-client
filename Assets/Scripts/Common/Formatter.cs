
using System;
using System.Globalization;

namespace AFKHero.Common
{
    public static class Formatter {

		/// <summary>
		/// Alphabet pour les suffix
		/// </summary>
		private static readonly string[] digits = {
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
			var length = number == 0 ? 1 : (int) Math.Floor(Math.Log10(Math.Abs(number)) + 1);

			// Aucun traitement si le nombre est trop grand pour notre alphabet
			if (length > 3 * digits.Length) {
				result = number.ToString (CultureInfo.CurrentCulture);
			} else {
			    // On travaille que sur les 5 premiers digits
				var firstDigits = length > 5 ? Math.Floor (number / Math.Pow (10, length - 5)).ToString (CultureInfo.CurrentCulture) : number.ToString (CultureInfo.CurrentCulture);

				var index = (int) Math.Ceiling((double) length / 3) - 1;
				var suffix = digits [index];

				// Découpage pour conserver 5 digits max dont 2 après la virgule
				if (length > 3)
				{
				    var rest = length % 3;
				    switch (rest)
				    {
				        case 1:
				            result = firstDigits.Substring (0, 1) + "." + firstDigits.Substring (1, 2) + suffix;
				            break;
				        case 2:
				            result = firstDigits.Substring (0, 2) + "." + firstDigits.Substring (2, 2) + suffix;
				            break;
				        default:
				            result = firstDigits.Substring (0, 3) + "." + firstDigits.Substring (3, 2) + suffix;
				            break;
				    }
				} else {
					result = firstDigits;
				}
			}

			return result;
		}

		/// <summary>
		/// Permet de formatter simplement une distance avec son unité.
		/// </summary>
		/// <returns>The distance string.</returns>
		/// <param name="distance">Distance.</param>
		public static string ToDistanceString(float distance)
		{
		    if (distance > 999) {
				return string.Format ("{0:0.0#}km", distance / 1000f) ;
			}
		    return distance > 999999 ? string.Format ("{0:0.0#}Mm", distance / 1000000f) : string.Format ("{0:0.0#}m", distance);
		}
	}
}
