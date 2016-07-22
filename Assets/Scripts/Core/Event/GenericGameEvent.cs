namespace AFKHero.Core.Event
{
    /// <summary>
    /// Event générique qui est un GameEvent + des datas de type T.
    /// </summary>
    public class GenericGameEvent<T> : GameEvent {


		public GenericGameEvent(T data){
            Data = data;
		}


		/// <summary>
		/// Les datas contenus dans l'event.
		/// </summary>
		/// <value>The data.</value>
		public T Data { get; set; }
	}
}
