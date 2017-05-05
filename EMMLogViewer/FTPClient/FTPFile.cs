using System;

namespace EMMLogViewer.FTPClient
{
	public class FTPFile
	{
		private String name;

		public FTPFile()
		{
		}

		public String Name {
			get {
				return this.name;
			}
			set {
				this.name = value;
			}
		}
	}
}
