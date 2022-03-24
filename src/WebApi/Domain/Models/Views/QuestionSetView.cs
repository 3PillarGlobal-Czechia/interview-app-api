using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Views
{
    public class QuestionSetView
    {
        public string Title { get; set; }

        public double? Difficulty { get; set; }

        public IEnumerable<string> Categories { get; set; }
    }
}
