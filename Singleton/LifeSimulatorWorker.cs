using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Singleton.Singletons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Singleton
{
    public class LifeSimulatorWorker : BackgroundService
    {
        public readonly IHomeController _controller;
        public readonly IHomeViewer _viewer;
        public readonly ILogger<LifeSimulatorWorker> _logger;

        public LifeSimulatorWorker(IHomeController controller, IHomeViewer  viewer, ILogger<LifeSimulatorWorker> logger)
        {
            _controller = controller;
            _viewer = viewer;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("🤖 Simulatore di Vita: ATTIVATO.");

            var random = new Random();

            while (!stoppingToken.IsCancellationRequested)
            {
                // 1. Leggiamo lo stato attuale
                double tempAttuale = _viewer.GetTemperature();

                // 2. Simuliamo un cambiamento (es. riscaldamento)
                double nuovaTemp = tempAttuale + (random.NextDouble() - 0.5); // Sale o scende un po'
                _controller.SetTemperature(Math.Round(nuovaTemp, 1));

                // 3. A volte accendiamo/spegniamo le luci a caso
                if (random.Next(0, 10) > 7)
                {
                    if (_viewer.IsLightOn()) _controller.TurnOffLights();
                    else _controller.TurnOnLights();
                }

                await Task.Delay(3000, stoppingToken); // Aspetta 3 secondi
            }
        }
    }
}
