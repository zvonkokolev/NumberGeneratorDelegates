namespace NumberGenerator.Logic
{
    public interface IObserver
    {
        void OnNextNumber(int number);
    }
}