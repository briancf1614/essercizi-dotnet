using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Singleton.Singletons
{
    #region Interfaces for Smart Home Hub
    // 1. Solo para mirar (Tablet del Salón)
    public interface IHomeViewer
    {
        double GetTemperature();
        bool IsLightOn();
        Guid GetInstanceId(); // Para verificar que es el mismo objeto
    }

    // 2. Para controlar (Panel del Dueño)
    public interface IHomeController
    {
        void SetTemperature(double temp);
        void TurnOnLights();
        void TurnOffLights();
    }

    // 3. Seguridad (Sistema de Alarma)
    public interface ISecuritySystem
    {
        void TriggerAlarm();
    }
    #endregion

    public class SmartHomeHubSingleton : IHomeViewer, IHomeController, ISecuritySystem
    {
        private readonly ILogger _logger;
        private readonly string _homeName;

        private double _temperature;
        private bool _lightsOn;
        private readonly Guid _instanceId;

        public SmartHomeHubSingleton(ILogger<SmartHomeHubSingleton> logger, string homeName)
        {
            _logger = logger;
            _homeName = homeName;
            _instanceId = Guid.NewGuid();
            _logger.LogInformation($"🏗️  SmartHomeHub '{_homeName}' INICIADO. ID: {_instanceId}");
        }

        public Guid GetInstanceId() => _instanceId;

        public double GetTemperature() => _temperature;

        public bool IsLightOn() => _lightsOn;

        public void SetTemperature(double temp)
        {
            _temperature = temp;
            _logger.LogInformation($"🌡️  Temperatura cambiada a {_temperature}°C");
        }

        public void TriggerAlarm()
        {
            _lightsOn = true;
            _logger.LogInformation("💡 Luces ENCENDIDAS");
        }

        public void TurnOffLights()
        {
            _lightsOn = false;
            _logger.LogInformation("🌑 Luces APAGADAS");
        }

        public void TurnOnLights()
        {
            _logger.LogCritical("🚨 ¡ALARMA! ¡INTRUSO DETECTADO EN VILLA STARK!");
        }
    }
}
