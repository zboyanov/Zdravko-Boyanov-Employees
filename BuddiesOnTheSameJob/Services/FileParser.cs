using BuddiesOnTheSameJob.Interfaces;
using BuddiesOnTheSameJob.Models;
using System.Globalization;
using System.IO;

namespace BuddiesOnTheSameJob.Services
{
    public sealed class FileParser : IFileParser
    {
        private readonly string[] _dateFormats = new[]
        {
            "yyyy-MM-dd",
            "dd/MM/yyyy",
            "MM/dd/yyyy",
            "dd-MM-yyyy",
            "M/d/yyyy",
            "d.M.yyyy",
            "yyyy/MM/dd"
        };

        public List<WorkLog> Parse(string filePath)
        {
            var workLogs = new List<WorkLog>();

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var employeeProperties = line.Split(',');

                if (employeeProperties.Length < 4)
                    continue;

                try
                {
                    int empId = int.Parse(employeeProperties[0].Trim());
                    int projectId = int.Parse(employeeProperties[1].Trim());
                    DateTime dateFrom = ParseDate(employeeProperties[2].Trim());
                    DateTime dateTo = string.IsNullOrWhiteSpace(employeeProperties[3])
                        ? DateTime.Today
                        : ParseDate(employeeProperties[3].Trim());

                    workLogs.Add(new WorkLog
                    {
                        EmpId = empId,
                        ProjectId = projectId,
                        DateFrom = dateFrom,
                        DateTo = dateTo
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Грешка при обработка на ред: {line} => {ex.Message}");
                }
            }

            return workLogs;
        }

        private DateTime ParseDate(string input)
        {
            if (input.ToLower() is "null")
            {
                return DateTime.Today;
            }

            if (DateTime.TryParseExact(input, _dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                return parsedDate;
            }

            throw new FormatException($"Непознат формат на дата: {input}");
        }
    }
}
