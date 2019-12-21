namespace NumberGenerator.Logic
{
	public interface IObservable
	{
		public delegate void NextNumberHandler(int number);
		public NextNumberHandler NextNumberHandlerChanged { get; set; }
	}
}