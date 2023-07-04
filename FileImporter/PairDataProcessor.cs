using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessor
{
    public class PairDataProcessor
    {
        public static EmployeePairData GetTopPairData(List<EmployeeWorkData> empData, out List<EmployeePairData> topPairData)
        {
            EmployeePairData finalResult = new EmployeePairData();
            topPairData = new List<EmployeePairData>();
            try
            {
                // NOTE 1
                // the overlapping period (if any) for 2 employees on the same project 
                // - starts from the later of the their DateFrom dates (see CommonPeriodFrom below)
                // - ends at the earliest of the their DateTo dates (see CommonPeriodTo below)
                // 
                // if there is no overlapping period then CommonPeriodTo < CommonPeriodFrom

                // NOTE 2
                // order the IDS of the employee pair on the same project in ascending order ( Employee1_ID < Employee2_ID)
                // so that we can eliminate duplicates using DistinctBy

                // N.B. we are assuming that an it is possible that a pair of employee
                // can work on the same project in more than 1 periods 
                // 
                var employeesSharingProject =
                    from z in empData
                    join z1 in empData on z.ProjectID equals z1.ProjectID
                    where z.EmpID != z1.EmpID
                    select new
                    {
                        ProjectId = z.ProjectID,
                        Employee1_ID = z.EmpID < z1.EmpID ? z.EmpID : z1.EmpID,
                        Employee2_ID = z.EmpID < z1.EmpID ? z1.EmpID : z.EmpID,
                        //Employee1From = z.EmpID < z1.EmpID ? z.DateFrom : z1.DateFrom,
                        //Employee2From = z.EmpID < z1.EmpID ? z1.DateFrom : z.DateFrom,
                        //Employee1To = z.EmpID < z1.EmpID ? z.DateTo : z1.DateTo,
                        //Employee2To = z.EmpID < z1.EmpID ? z1.DateTo : z.DateTo,
                        CommonPeriodFrom = z.DateFrom <= z1.DateFrom ? z1.DateFrom : z.DateFrom,
                        CommonPeriodTo = z.DateTo <= z1.DateTo ? z.DateTo : z1.DateTo,
                    };


                var distinctEmployeesPairs = employeesSharingProject
                    .DistinctBy(e => new { e.ProjectId, e.Employee1_ID, e.Employee2_ID, e.CommonPeriodFrom, e.CommonPeriodTo })
                    .Select(e =>
                    new EmployeePairData()
                    {
                        ProjectID = e.ProjectId,
                        EmployeeID1 = e.Employee1_ID,
                        EmployeeID2 = e.Employee2_ID,
                        DaysWorked = ((DateOnly)e.CommonPeriodTo).DayNumber - e.CommonPeriodFrom.DayNumber + 1
                    })
                    .Where(epd => epd.DaysWorked > 0);

                finalResult = distinctEmployeesPairs
                .GroupBy(pair => new { pair.EmployeeID1, pair.EmployeeID2 })
                .Select(g => new EmployeePairData
                {
                    EmployeeID1 = g.Key.EmployeeID1,
                    EmployeeID2 = g.Key.EmployeeID2,
                    DaysWorked = g.Sum(h => h.DaysWorked)
                })
                .OrderByDescending(r => r.DaysWorked)
                .First();


                // get data for the selected pair in order to disply it
                topPairData = distinctEmployeesPairs
                    .Where(p => p.EmployeeID1 == finalResult.EmployeeID1 && p.EmployeeID2 == finalResult.EmployeeID2)
                    .OrderByDescending(r => r.DaysWorked)
                    .ToList();

                //var grouped_teachers = teachers
                //    .GroupBy(t => new { t.subject, t.age })
                //    .Select(g => new
                //    {
                //        subject = g.Key.subject,
                //        age = g.Key.age,
                //        count = g.Count(), //number of teachers in the group
                //        names = string.Join(", ", g.Select(t => t.name)) // teacher names
                //    });

            }
            catch (Exception ex)
            {
                //errorMessages.Add($"Exception in method 'Method' : {ex.Message} ");
            }

            return finalResult;
        }
    }
}
