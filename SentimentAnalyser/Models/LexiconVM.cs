using Microsoft.AspNetCore.Mvc.Rendering;
using SentimentAnalyser.Entities;
using System.Collections.Generic;

namespace SentimentAnalyser.Models
{
    public class LexiconVM
    {
        public IList<Word> Words { get; set; }
        public List<SelectListItem> FiltersList { get; set; }
        public int SelectedFilterId { get; set; }
        public double Score { get; set; }
        public bool Calculated { get; set; }
        public string FileValidationMessage { get; set; }

    }
}
