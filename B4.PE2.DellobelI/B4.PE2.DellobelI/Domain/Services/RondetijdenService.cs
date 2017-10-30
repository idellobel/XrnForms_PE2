using B4.PE2.DellobelI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B4.PE2.DellobelI.Domain.Services
{
    public class RondetijdenService
    {
        public static int ronde = 0;
        private static List<Lap> Rondetijdenlist = new List<Lap>
        {

        new Lap
            {
                Volgenummer = new int(),
                DisplayName = ""

            }
        };

        public async Task<IEnumerable<Lap>> SaveRondeToLijst(string DisplayName)
        {
            await Task.Delay(millisecondsDelay: 0);
            Lap savedRonde;
            if (DisplayName != null)
            {
                savedRonde = new Lap();
                savedRonde.DisplayName = DisplayName;
                savedRonde.Volgenummer = new int();
                Rondetijdenlist.Add(savedRonde);

            }
            return Rondetijdenlist;

        }
    }

}
