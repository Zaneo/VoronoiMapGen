using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace VoronoiMapGen.Generation
{
    interface IMapGenerationStep {
        void BeginTask();
        Canvas GetThumbNail();
    }
}
