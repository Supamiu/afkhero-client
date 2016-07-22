namespace AFKHero.Core.Tools
{
    public class CryptoService : Singleton<CryptoService>
	{
		private const string key = "zAlUuA5FjOQifdp0Q2kBnlxeaRyFo8il";

		protected CryptoService ()
		{
		}

		public string Xor (string baseString)
		{
			char[] src = baseString.ToCharArray ();
			char[] k = key.ToCharArray ();
			string res = "";
			for (int i = 0; i < src.Length; i++) {
				res += (char)(src [i] ^ k [i % k.Length]);
			}
			return res;
		}
	}
}