using MontyHallLib;

namespace MontyHallTest
{
	public class MontyHallTests
	{
		private MontyHall _montyHall { get; set; }
		public MontyHallTests()
		{
			_montyHall = new MontyHall();
		}

		[Theory]
		[InlineData(100)]
		[InlineData(1000)]
		[InlineData(10000)]
		public void WinRateShouldBeThirdWhenKeepingDoor(int attempts)
		{
			//act
			_montyHall.Simulate(false, attempts);
			var winRate = WinRate(_montyHall.Wins, attempts);

			//assert winning is about 3rd of the times
			Assert.InRange(winRate, 3, 4);
		}

		private int WinRate(int wins, int total)
		{
			var rate = ((double)_montyHall.Wins / total);
			return (int)(rate * 10);
		}

		[Theory]
		[InlineData(50)]
		[InlineData(100)]
		[InlineData(1000)]
		[InlineData(10000)]
		public void WinRateShouldBeTwoThirdsWhenSwitchingDoor(int attempts)
		{
			//act
			_montyHall.Simulate(true, attempts);
			var winRate = WinRate(_montyHall.Wins, attempts);

			//assert winning is about 2/3rd of the times
			Assert.InRange(winRate, 6, 7);
		}

		[Fact]
		public void RaiseEvent()
		{
			//act
			_montyHall.Simulate(true, 100);
			var carsEvt = Assert.RaisesAny<CarsChangedEventArgs>(
				h => _montyHall.CarsChanged += h,
				h => _montyHall.CarsChanged -= h,
				() => _montyHall.Simulate(true, 100));
			Assert.NotNull(carsEvt);

			_montyHall.Simulate(true, 100);
			var goatsEvt = Assert.RaisesAny<GoatsChangedEventArgs>(
				h => _montyHall.GoatsChanged += h,
				h => _montyHall.GoatsChanged -= h,
				() => _montyHall.Simulate(true, 100));
			Assert.NotNull(goatsEvt);
		}
	}
}