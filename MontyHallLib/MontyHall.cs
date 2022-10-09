namespace MontyHallLib
{
	public class MontyHall : IMontyHall
	{
		public event EventHandler<CarsChangedEventArgs> CarsChanged;
		public event EventHandler<GoatsChangedEventArgs> GoatsChanged;
		public int Wins { get; set; }
		public int Losses { get; set; }
		Random random = new Random();

		public void Simulate(bool changeDoor , int attemptsCount = 1000000)
		{
			Wins = 0;
			Losses = 0;

			// iterate MontyHall routine
			for (int i = 0; i < attemptsCount; i++)
			{
				bool result = MontyHallPick(random.Next(3), changeDoor,
											random.Next(3), random.Next(1));
				if (result)
					Wins++;				
				else
					Losses++;
			}

			if(Wins > 0)
				CarsChanged?.Invoke(this, new CarsChangedEventArgs { NewCarWinValue = Wins });
		
			if(Losses > 0)
				GoatsChanged?.Invoke(this, new GoatsChangedEventArgs { NewGoatWinValue = Losses });
		}

		public bool MontyHallPick(int pickedDoor, bool changeDoor,
									  int carDoor, int goatDoorToRemove)
		{
			bool win = false;

			// randomly remove one of the *goat* doors, but not the "contestants picked" ONE!
			int leftGoat = 0;
			int rightGoat = 2;
			switch (pickedDoor)
			{
				case 0: leftGoat = 1; rightGoat = 2; break;
				case 1: leftGoat = 0; rightGoat = 2; break;
				case 2: leftGoat = 0; rightGoat = 1; break;
			}

			int keepGoat = goatDoorToRemove == 0 ? rightGoat : leftGoat;

			// would the contestant win with the switch or the stay?
			if (!changeDoor)
			{
				// not changing the initially picked door
				win = carDoor == pickedDoor;
			}
			else
			{
				// changing picked door to the other door remaining
				win = carDoor != keepGoat;
			}

			return win;
		}
	}
}