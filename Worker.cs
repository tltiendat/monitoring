using Microsoft.VisualBasic;
using WUApiLib;
using System.Threading.Tasks;


namespace Eps.ServerMonitoring
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            GetUpdates();
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}
        }

        private static void GetUpdates()
        {
            try
            {
                IUpdateHistoryEntryCollection updateHistory = GetUpdateHistory();

                Console.WriteLine("Update History:");
                foreach (IUpdateHistoryEntry entry in updateHistory)
                {
                    Console.WriteLine($"Title: {entry.Title}");
                    Console.WriteLine($"Description: {entry.Description}");
                    Console.WriteLine($"Result: {entry.ResultCode}");
                    Console.WriteLine($"Date: {entry.Date}");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadLine();
        }

        static IUpdateHistoryEntryCollection GetUpdateHistory()
        {
            UpdateSession updateSession = new UpdateSession();
            IUpdateSearcher updateSearcher = updateSession.CreateUpdateSearcher();
            IUpdateHistoryEntryCollection updateHistory = updateSearcher.QueryHistory(0, updateSearcher.GetTotalHistoryCount());
            return updateHistory;
        }
    }
}
