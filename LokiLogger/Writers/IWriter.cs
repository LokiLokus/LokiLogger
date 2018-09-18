using System.Threading.Tasks;

namespace LokiLogger.Writers {
	public interface IWriter {
		void WriteLog(Model.Log log);
		Task Stop();
	}
}