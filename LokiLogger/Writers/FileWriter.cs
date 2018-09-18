using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LokiLogger.Writers {
	public class FileWriter :IWriter {
		private string _filePath;
		private StreamWriter _fileStream;

		public FileWriter(string options)
		{
			_filePath = options;
			_fileStream = new StreamWriter(_filePath ,true, Encoding.UTF8) {AutoFlush = true};
		}
		
		public void WriteLog(Model.Log log)
		{
			lock (_fileStream)
			{
				_fileStream.WriteLine(log.ToString());
			}
		}

		public async Task Stop()
		{
			lock (_fileStream)
			{
				_fileStream.Close();				
			}
			await Task.Delay(0);
		}
	}
}