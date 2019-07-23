using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Vue24Hour.Domain.Model;
using Vue24Hour.Domain.Repository;

namespace Vue24Hour.Services
{
    internal class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private IGameRepository _gameRepository;
        private Timer _timer;
        private const int TickInSeconds = 5;

        public TimedHostedService(ILogger<TimedHostedService> logger, IGameRepository repository)
        {
            _logger = logger;
            _gameRepository = repository;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(TickInSeconds));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            //_logger.LogInformation("Timed Background Service is working.");
            var allRunningGames = _gameRepository.GetAllGames().Where(g => g.GameState == GameState.Running);
            //_logger.LogInformation("There are " + allRunningGames.Count() + " running games.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
