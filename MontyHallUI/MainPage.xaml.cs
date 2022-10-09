using MontyHallLib;
using MontyHallUI.ViewModels;

namespace MontyHallUI;

public partial class MainPage : ContentPage
{
	public MontyHallViewModel montyHallViewModel { get; set; }
	public MainPage(MontyHallViewModel montyHallViewModel = null)
	{
		InitializeComponent();
		this.montyHallViewModel = montyHallViewModel;
		BindingContext = montyHallViewModel;
	}
}

