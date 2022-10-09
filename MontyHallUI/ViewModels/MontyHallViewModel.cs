using MontyHallLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MontyHallUI.ViewModels
{
	public class MontyHallViewModel : INotifyPropertyChanged
	{
		private int noSwitchCarsCount;
		public int NoSwitchCarsCount
		{
			get => noSwitchCarsCount;
			set
			{
				if (noSwitchCarsCount != value)
				{
					noSwitchCarsCount = value;
					OnPropertyChanged();
					OnPropertyChanged(nameof(NoSwitchCarsPercentage));
				}
			}
		}

		private int noSwitchGoatsCount;
		public int NoSwitchGoatsCount
		{
			get => noSwitchGoatsCount;
			set
			{
				if (noSwitchGoatsCount != value)
				{
					noSwitchGoatsCount = value;
					OnPropertyChanged();
					OnPropertyChanged(nameof(NoSwitchCarsPercentage));
				}
			}
		}

		private int switchCarsCount;
		public int SwitchCarsCount
		{
			get => switchCarsCount;
			set
			{
				if (switchCarsCount != value)
				{
					switchCarsCount = value;
					OnPropertyChanged();
					OnPropertyChanged(nameof(SwitchCarsPercentage));
				}
			}
		}

		private int switchGoatsCount;
		public int SwitchGoatsCount
		{
			get => switchGoatsCount;
			set
			{
				if (switchGoatsCount != value)
				{
					switchGoatsCount = value;
					OnPropertyChanged();
					OnPropertyChanged(nameof(SwitchCarsPercentage));
				}
			}
		}

		private int currentGoats;
		public int CurrentGoats
		{
			get => currentGoats;
			set
			{
				currentGoats = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(CurrentWin));
			}
		}

		private int currentCars;
		public int CurrentCars
		{
			get => currentCars; 
			set
			{
				currentCars = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(CurrentWin));
			}
		}

		public int CurrentWin
		{
			get
			{
				if (CurrentCars + CurrentGoats == 0)
					return 0;
				return Convert.ToInt32((double)CurrentCars / (CurrentCars + CurrentGoats) * 100);
			}
		}

		private readonly IMontyHall _montyHall;
		public ICommand StartSimulation { get; }

		private int? attemptsCount;
		public int? AttemptsCount
		{
			get => attemptsCount;
			set
			{
				attemptsCount = value;
				OnPropertyChanged();
			}
		}

		private bool switchDoor;
		public bool SwitchDoor
		{
			get => switchDoor;
			set
			{
				switchDoor = value;
				OnPropertyChanged();
			}
		}
		public int? NoSwitchCarsPercentage
		{
			get
			{
				if (NoSwitchCarsCount + NoSwitchGoatsCount == 0)
					return 0;
				return Convert.ToInt32((double)NoSwitchCarsCount / (NoSwitchCarsCount + NoSwitchGoatsCount) * 100);
			}
		}
		public double? SwitchCarsPercentage
		{
			get
			{
				if (SwitchCarsCount + SwitchGoatsCount == 0)
					return 0;
				return Convert.ToInt32((double)SwitchCarsCount / (SwitchCarsCount + SwitchGoatsCount) * 100);
			}
		}
		public MontyHallViewModel(IMontyHall montyHall = null)
		{
			_montyHall = montyHall;
			montyHall.CarsChanged += MontyHall_CarsChanged;
			montyHall.GoatsChanged += MontyHall_GoatsChanged;
			StartSimulation = new Command(async () =>
			{
				montyHall.Simulate(SwitchDoor, AttemptsCount ?? 0);
			});
		}
		private void MontyHall_GoatsChanged(object sender, GoatsChangedEventArgs e)
		{
			CurrentGoats = e.NewGoatWinValue;
			switch (SwitchDoor)
			{
				case false: NoSwitchGoatsCount += e.NewGoatWinValue; break;
				case true: SwitchGoatsCount += e.NewGoatWinValue; break;
			}
		}
		private void MontyHall_CarsChanged(object sender, CarsChangedEventArgs e)
		{
			CurrentCars = e.NewCarWinValue;
			switch (SwitchDoor)
			{
				case false: NoSwitchCarsCount += e.NewCarWinValue; break;
				case true: SwitchCarsCount += e.NewCarWinValue; break;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string name = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
