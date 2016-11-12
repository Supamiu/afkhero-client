namespace AFKHero.Core.Tools
{
    public class CryptoService
	{
		private const string key = "zAlUuA5FjOQifdp0Q2kBnlxeaRyFo8il";

		public static string Xor (string baseString)
		{
			var src = baseString.ToCharArray ();
			var k = key.ToCharArray ();
			var res = "";
			for (var i = 0; i < src.Length; i++) {
				res += (char)(src [i] ^ k [i % k.Length]);
			}
			return res;
		}
	}
}
