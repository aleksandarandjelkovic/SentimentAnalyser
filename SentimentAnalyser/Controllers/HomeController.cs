using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SentimentAnalyser.Entities;
using SentimentAnalyser.Models;
using SentimentAnalyser.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentAnalyser.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            IList<Word> result = await ServiceProxy.GetFilteredWords();

            var vm = new LexiconVM()
            {
                Words = result,
                FiltersList = GetFilterList(),
            };

            return View(vm);
        }

        public async Task<int> AddEditWord(int wordId, string wordDesc, double sentimentScore)
        {
            Word word = new Word()
            {
                WordId = wordId,
                WordDesc = wordDesc,
                SentimentScore = sentimentScore
            };

            return await ServiceProxy.AddEditWord(word);
        }

        public async Task<IActionResult> FilterWords(int filterId)
        {
            IList<Word> result = await ServiceProxy.GetFilteredWords(filterId);

            var vm = new LexiconVM()
            {
                Words = result,
                FiltersList = GetFilterList(),
                SelectedFilterId = filterId
            };

            return View("Index", vm);
        }

        public async void DeleteWord(int wordId)
        {
            if (wordId != 0)
            {
                await ServiceProxy.DeleteWord(wordId);
            }
        }

        public List<SelectListItem> GetFilterList()
        {
            return new()
            {
                new SelectListItem { Selected = true, Text = "All", Value = "0" },
                new SelectListItem { Selected = false, Text = "Positive", Value = "1" },
                new SelectListItem { Selected = false, Text = "Neutral", Value = "2" },
                new SelectListItem { Selected = false, Text = "Negative", Value = "3" }
            };
        }

        public async Task<IActionResult> Calculate(string text)
        {
            IList<Word> words = await ServiceProxy.GetFilteredWords();

            var vm = new LexiconVM()
            {
                Words = words,
                FiltersList = GetFilterList(),
                Calculated = true
            };

            if (!string.IsNullOrEmpty(text))
            {
                var stringList = ConvertStringToList(text);
                var result = CalculateRate(stringList).Result;
                vm.Score = Math.Round(result, 2);
                vm.Calculated = true;
            }
            else
            {
                vm.FileValidationMessage = "Please insert text.";
            }

            return View("Index", vm);
        }

        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            IList<Word> words = await ServiceProxy.GetFilteredWords();
            var vm = new LexiconVM()
            {
                Words = words,
                FiltersList = GetFilterList(),
                Calculated = true
            };

            if (files.Count == 0)
            {
                vm.FileValidationMessage = "Please choose file.";
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var sb = new StringBuilder();
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        while (reader.Peek() >= 0)
                            sb.Append(reader.ReadLine());
                    }

                    var stringList = ConvertStringToList(sb.ToString());
                    var result = CalculateRate(stringList).Result;

                    vm.Score = Math.Round(result, 2);
                    vm.Calculated = true;

                    return View("Index", vm);
                }
                else
                {
                    vm.FileValidationMessage = "Uploaded file is empty.";
                }
            }

            return View("Index", vm);
        }

        private async Task<double> CalculateRate(IEnumerable<string> listToBeCalculated)
        {
            var wordsList = await ServiceProxy.GetFilteredWords();

            return wordsList.Join(listToBeCalculated,
            wl => wl.WordDesc.ToLower(),
            j => j.ToLower(),
              (word, join) => new { join, word.SentimentScore }
            ).Select(x => x.SentimentScore).Sum();
        }

        private IEnumerable<string> ConvertStringToList(string str)
        {
            return new string(str.Where(c => !char.IsPunctuation(c)).ToArray()).Split(' ');
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorVM { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
