using Singleton.Singletons;
using System;
using System.Collections.Generic;
using System.Text;

namespace Singleton
{
    public class PannelloControllo
    {
        private readonly IHomeController _controller;

        public PannelloControllo(IHomeController controller)
        {
            _controller = controller;
        }

        public void ModoFiesta()
        {
            Console.WriteLine("[Panel] Activando Modo Fiesta...");
            _controller.TurnOnLights();
            _controller.SetTemperature(24.5);
        }
    }
}
