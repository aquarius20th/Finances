using System.Collections.Generic;
using Caliburn.Micro;

namespace UI.ViewModels
{
	public interface IShell {}

	public class Shell : Conductor<IScreen>.Collection.OneActive, IShell
	{
		public Shell(IEnumerable<IScreen> screens)
		{
			DisplayName = "Finances";

			Items.AddRange(screens);
		}
	}
}