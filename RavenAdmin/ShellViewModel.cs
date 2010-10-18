namespace RavenAdmin
{
	using System.ComponentModel.Composition;
	using Framework;

	[Export(typeof (IShell))]
	public class ShellViewModel : Screen, IShell
	{
		object body;

		public ShellViewModel()
		{
			Body = new SummaryViewModel();
		}

		public object Body
		{
			get { return body; }
			set
			{
				body = value;
				NotifyOfPropertyChange(() => Body);
			}
		}

		public void NavigateToCollections()
		{
			NavigateTo(new CollectionsViewModel());
		}

		public void NavigateTo(object vm)
		{
			Body = vm;
		}
	}
}