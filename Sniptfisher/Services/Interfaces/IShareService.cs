using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sniptfisher.Services.Interfaces
{
    public interface IShareService
    {
        /// <summary>
        /// Ejecuta el servicio para compartir.
        /// </summary>
        /// <param name="title">El título compartido.</param>
        /// <param name="message">El mensaje comparido.</param>
        /// <param name="link">La URL del vínculo compartido.</param>
        /// <param name="image">La URL de la imágen compartida.</param>
        void Share(string title, string message, string link = "", string image = "");
    }
}
