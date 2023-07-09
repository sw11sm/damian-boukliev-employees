using Models;
using System;
using System.Drawing;
using System.Globalization;
using System.Reflection.Metadata;

namespace DataProcessor;

public class FileImporter
{
    // TODO - Document this method
    // - returns a list of errors (if any) which occured while reading and/or processing data from given file   
    public static List<string> Import(string fileName, out List<EmployeeWorkData> employeesWorkData)
    {
        const string ERROR_PASSING_LINE = "Error parsing line";
        employeesWorkData = new List<EmployeeWorkData>();
        var errorMessages = new List<string>();
        try
        {
            int empId;
            int projId;
            DateOnly dateFrom;
            DateOnly dateTo;

            // TODO - add all expected date formats here
            string[] dateFormats = { "yyyy-MM-dd", "M/dd/yyyy", "MM/dd/yyyy" };

            // read file content
            string fileContent = File.ReadAllText(fileName);

            // process one line at a time...
            foreach (var line in SplitTextToLines(fileContent))
            {
                var lineItems = line.Split(',');

                if(lineItems.Length == 4) 
                {
                    // parse empId, projId, dateFrom
                    if (Int32.TryParse(lineItems[0].Trim(), out empId) &&
                         Int32.TryParse(lineItems[1].Trim(), out projId) &&
                         DateOnly.TryParseExact(lineItems[2].Trim(), dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateFrom)
                    )
                    {
                        lineItems[3] = lineItems[3].Trim();
                        if (lineItems[3] == "NULL" || string.IsNullOrEmpty(lineItems[3]))
                        {
                            // default to today's date
                            dateTo = DateOnly.FromDateTime(DateTime.Now);
                        }
                        else
                        {
                            // parse dateTo
                            if (!DateOnly.TryParseExact(lineItems[3], dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTo))
                            {
                                // invalid dateTo 
                                errorMessages.Add($"Error parsing '{lineItems[3]}' in line '{line}'");
                            }
                        }

                        // construct the object and add it to the list
                        employeesWorkData.Add(new EmployeeWorkData()
                        {
                            EmpID = empId,
                            ProjectID = projId,
                            DateFrom = dateFrom,
                            DateTo = dateTo
                        });

                    }
                    else
                    {
                        // invalid data
                        errorMessages.Add($"{ERROR_PASSING_LINE} '{line}'");
                    }
                }
                else
                {
                    // invalid data
                    errorMessages.Add($"{ERROR_PASSING_LINE} '{line}'");
                }
            }
        }
        catch (Exception ex )
        {
            errorMessages.Add($"Exception in method 'Method' : {ex.Message} ");
        }

        return errorMessages;
    }

    private static IEnumerable<string> SplitTextToLines(string text)
    {
        if (text == null)
        {
            yield break;
        }

        using (var reader = new StringReader(text))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }
    }
}