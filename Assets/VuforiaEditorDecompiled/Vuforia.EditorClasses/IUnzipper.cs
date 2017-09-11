using System;
using System.IO;

namespace Vuforia.EditorClasses
{
	public interface IUnzipper
	{
		System.IO.Stream UnzipFile(string path, string fileNameinZip);
	}
}
