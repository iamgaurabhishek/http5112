using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPassionProjectW2024n01605783.Models.ViewModel
{
    public class DetailsExercise
    {
        public ExerciseDto SelectedExercise { get; set; }
        public List<BlogDto> BlogsForExercise { get; set; }

    }
}