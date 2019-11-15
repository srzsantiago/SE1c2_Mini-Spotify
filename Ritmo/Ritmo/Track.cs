using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    class Track
    {
        private string Name;
        private Uri AudioFile { get; set; } = new Uri("TestFiles/Powerup1.wav", UriKind.RelativeOrAbsolute);


    }
}