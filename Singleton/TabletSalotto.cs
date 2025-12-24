using Singleton.Singletons;

namespace Singleton
{
    public class TabletSalotto
    {
        private readonly IHomeViewer _viewer;

        public TabletSalotto(IHomeViewer viewer)
        {
            _viewer = viewer;
        }

        public void MostrarEstado()
        {
            Console.WriteLine($"[Tablet] Temp: {_viewer.GetTemperature()}°C | Luces: {(_viewer.IsLightOn() ? "ON" : "OFF")}");
            Console.WriteLine($"[Tablet] Conectado al Hub ID: {_viewer.GetInstanceId()}");
        }
    }
}
