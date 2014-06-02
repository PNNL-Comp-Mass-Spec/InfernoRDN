using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DAnTE.ExtraControls
{
	public class ucStatusBarProgressPanel : StatusBarPanel
	{
		private bool isAdded = false;

		private ProgressBar progressBar = new ProgressBar();
		[Category("Progress")]
		public ProgressBar ProgressBar
		{
			get { return progressBar; }
		}

		public ucStatusBarProgressPanel() : base()
		{
			// Just to be safe
			this.Style = System.Windows.Forms.StatusBarPanelStyle.OwnerDraw;
		}

		public void ParentDrawItemHandler(object sender, StatusBarDrawItemEventArgs sbdevent)
		{
			// Only add this once to the parent's control container
			if (isAdded == false)
			{
				this.Parent.Controls.Add(this.progressBar);
				this.isAdded = true;
			}

			// Get the bounds of this panel and copy to the progress bar's bounds
			if (sbdevent.Panel == this)
				progressBar.Bounds = sbdevent.Bounds;
		}
	}
}
