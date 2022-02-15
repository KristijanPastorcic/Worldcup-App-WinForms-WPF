using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FavoritePlayersWPF.Models
{
//    naziv, FIFA kod, broj
//odigranih/pobjeda/poraza/neodlučenih, golova zabijenih/primljenih/razlika.
    public class TeamStatisticsClass
    {
        public string Name { get; set; }
        public string FifaCode { get; set; }
        public int NumberOfGamesPlayed { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
        public float? Goals { get; set; }
        public float? GoalsTaken { get; set; }
        public float? diff { get; set; }
    }
}
