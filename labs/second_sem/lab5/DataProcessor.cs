using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    class DataProcessor
    {
        private List<Course> courses;

        private HashSet<string> instructors;
        private Dictionary<string, List<Course>> subjectDictionary;
        private HashSet<string> subjects;
        private Dictionary<float, List<Course>> unitDictionary;
        private SortedSet<float> units;
        private Dictionary<string, float> sumDictionary;

        private const int pageSize = 10;

        public DataProcessor()
        {
            this.courses = new List<Course>();
            Initialize();
        }
        private void Initialize()
        {
            this.instructors = new HashSet<string>();
            this.subjectDictionary = new Dictionary<string, List<Course>>();
            this.subjects = new HashSet<string>();
            this.unitDictionary = new Dictionary<float, List<Course>>();
            this.units = new SortedSet<float>();
            this.sumDictionary = new Dictionary<string, float>();
        }
        public List<Course> Courses
        {
            get
            {
                return courses;
            }
            set
            {
                this.courses = value;
                Initialize();

                foreach (Course course in this.courses)
                {
                    if (course.instructor != null)
                    {
                        instructors.Add(course.instructor);
                    }
                    if (course.subject != null)
                    {
                        subjects.Add(course.subject);
                        if (subjectDictionary.TryGetValue(course.subject, out List<Course> list1))
                        {
                            list1.Add(course);
                            subjectDictionary.Remove(course.subject);
                            subjectDictionary.Add(course.subject, list1);
                        }
                        else
                        {
                            List<Course> newList1 = new List<Course>();
                            newList1.Add(course);
                            subjectDictionary.Add(course.subject, newList1);
                        }
                    }
                    units.Add(course.units);
                    if (unitDictionary.TryGetValue(course.units, out List<Course> list2))
                    {
                        list2.Add(course);
                        unitDictionary.Remove(course.units);
                        unitDictionary.Add(course.units, list2);
                    }
                    else
                    {
                        List<Course> newList2 = new List<Course>();
                        newList2.Add(course);
                        unitDictionary.Add(course.units, newList2);
                    }

                    float sum = sumDictionary.GetValueOrDefault(course.subject);
                    sum += course.units;
                    sumDictionary.Remove(course.subject);
                    sumDictionary.Add(course.subject, sum);
                }
            }
        }
        public List<Course> GetPage(int n)
        {
            int pageCount = GetPageCount();
            if (n < 1 || n > pageCount)
            {
                throw new Exception("Wrong page");
            }
            List<Course> page = new List<Course>();

            for (int i = (n-1) * pageSize; i < Math.Min(n * pageSize, courses.Count); i++)
            {
                page.Add(courses[i]);
            }
            return page;
        }
        public int GetPageCount()
        {
            return (int)Math.Ceiling(courses.Count / (double)pageSize);
        }
        public List<Course> GetExport(int n)
        {
            List<Course> export = new List<Course>();

            float[] unitsArray = new float[units.Count];
            units.CopyTo(unitsArray);
            for (int i = unitsArray.Length - 1; i >= 0; i--)
            {
                unitDictionary.TryGetValue(unitsArray[i], out List<Course> listPerUnit);
                for (int j = 0; j < listPerUnit.Count; j++)
                {
                    if (export.Count >= n)
                    {
                        return export;
                    }
                    export.Add(listPerUnit[j]);
                }
            }
            return export;
        }
        public string[] GetUniqueSubjects()
        {
            string[] subjects = new string[this.subjects.Count];
            this.subjects.CopyTo(subjects);
            return subjects;
        }
        public string[] GetTitlesBySubject(string subject)
        {
            if (subjectDictionary.TryGetValue(subject, out List<Course> list))
            {
                string[] titles = new string[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    titles[i] = list[i].title;
                }
                return titles;
            }
            return new string[0];
        }
        public string[] GetUniqueInstructors()
        {
            string[] instructors = new string[this.instructors.Count];
            this.instructors.CopyTo(instructors);
            return instructors;
        }
        public float[] GetSums()
        {
            string[] subjects = GetUniqueSubjects();
            float[] sums = new float[subjects.Length];
            for (int i = 0; i < subjects.Length; i++)
            {
                sums[i] = sumDictionary.GetValueOrDefault<string, float>(subjects[i]);
            }
            return sums;
        }
    }
}
