using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTableProjectApi.Models
{
    public class ActualModel
    {
        public ActualModel(Actual_timetable productModel)
        {
            Id_lesson = productModel.Id_lesson;
            Lesson = productModel.Lesson;
            Classroom = productModel.Classroom;
            Subgroup = productModel.Subgroup;
            Count = productModel.Count;
        }

        public int Id_lesson { get; set; }
        public string Lesson { get; set; }
        public string Classroom { get; set; }
        public string Subgroup { get; set; }
        public string Count { get; set; }

    }
}
