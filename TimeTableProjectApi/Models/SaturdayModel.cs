using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTableProjectApi.Models
{
    public class SaturdayModel
    {
        public SaturdayModel(Saturday productModel)
        {
            Id_lesson = productModel.Id_lesson;
            Lesson = productModel.Lesson;
            Classroom = productModel.Classroom;
            Teacher = productModel.Teacher;
        }

        public int Id_lesson { get; set; }
        public string Lesson { get; set; }
        public string Classroom { get; set; }
        public string Teacher { get; set; }
    }
}