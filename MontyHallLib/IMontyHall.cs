namespace MontyHallLib
{
	public class CarsChangedEventArgs : EventArgs
	{
		public int NewCarWinValue { get; set; }
	}
	public class GoatsChangedEventArgs : EventArgs
	{
		public int NewGoatWinValue { get; set; }
	}

	public interface IMontyHall
	{
		bool MontyHallPick(int pickedDoor, bool changeDoor, int carDoor, int goatDoorToRemove);
		void Simulate(bool changeDoor = false, int attemptsCount = 1000000);
		event EventHandler<CarsChangedEventArgs> CarsChanged;
		event EventHandler<GoatsChangedEventArgs> GoatsChanged;		
	}
}